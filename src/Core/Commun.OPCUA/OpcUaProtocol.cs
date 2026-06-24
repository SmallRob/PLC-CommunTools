using System;
using System.Threading.Tasks;
using Commun.Protocols;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;

namespace Commun.OPCUA;

public class OpcUaProtocol : ProtocolBase
{
    public override string Name => "OPC UA";
    public override string Version => "1.0.0";

    private OpcUaConfig? _config;
    private ApplicationConfiguration? _appConfig;
    private Session? _session;

    protected override async Task<bool> ConnectCoreAsync(ProtocolConfig config)
    {
        _config = new OpcUaConfig
        {
            EndpointUrl = $"opc.tcp://{config.Address}:{config.Port}",
            OperationTimeout = (int)config.Timeout.TotalMilliseconds
        };

        try
        {
            _appConfig = new ApplicationConfiguration
            {
                ApplicationName = _config.ApplicationName,
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    AutoAcceptUntrustedCertificates = _config.AutoAccept,
                    RejectSHA1SignedCertificates = false
                },
                TransportQuotas = new TransportQuotas
                {
                    OperationTimeout = _config.OperationTimeout
                },
                ClientConfiguration = new ClientConfiguration
                {
                    DefaultSessionTimeout = _config.SessionTimeout
                }
            };

            await _appConfig.Validate(ApplicationType.Client);

            var endpoint = CoreClientUtils.SelectEndpoint(_config.EndpointUrl, _config.UseSecurity);
            var endpointConfig = EndpointConfiguration.Create(_appConfig);
            var configuredEndpoint = new ConfiguredEndpoint(null, endpoint, endpointConfig);

            IUserIdentity userIdentity = _config.Username != null
                ? new UserIdentity(_config.Username, _config.Password ?? string.Empty)
                : new UserIdentity();

            _session = await Session.Create(
                _appConfig,
                configuredEndpoint,
                false,
                _config.ApplicationName,
                (uint)_config.SessionTimeout,
                userIdentity,
                null
            );

            return _session.Connected;
        }
        catch (Exception ex)
        {
            OnError($"Failed to connect to OPC UA server: {ex.Message}", ex);
            return false;
        }
    }

    protected override async Task DisconnectCoreAsync()
    {
        if (_session != null)
        {
            _session.Close();
            _session.Dispose();
            _session = null;
        }
        await Task.CompletedTask;
    }

    protected override async Task<ProtocolResponse> SendCoreAsync(ProtocolRequest request)
    {
        if (_session == null || !_session.Connected)
            return new ProtocolResponse { Success = false, ErrorMessage = "Not connected" };

        try
        {
            var nodeId = new NodeId(request.Register, 2);
            var dataValue = _session.ReadValue(nodeId);

            return new ProtocolResponse
            {
                Success = true,
                Data = ConvertToBytes(dataValue.Value)
            };
        }
        catch (Exception ex)
        {
            return new ProtocolResponse { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ProtocolResponse> ReadNodeAsync(string nodeIdString)
    {
        if (_session == null || !_session.Connected)
            return new ProtocolResponse { Success = false, ErrorMessage = "Not connected" };

        try
        {
            var nodeId = new NodeId(nodeIdString);
            var dataValue = _session.ReadValue(nodeId);

            return new ProtocolResponse
            {
                Success = true,
                Data = ConvertToBytes(dataValue.Value)
            };
        }
        catch (Exception ex)
        {
            return new ProtocolResponse { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ProtocolResponse> WriteNodeAsync(string nodeIdString, object value)
    {
        if (_session == null || !_session.Connected)
            return new ProtocolResponse { Success = false, ErrorMessage = "Not connected" };

        try
        {
            var nodeId = new NodeId(nodeIdString);
            var writeValue = new WriteValue
            {
                NodeId = nodeId,
                AttributeId = Attributes.Value,
                Value = new DataValue(new Variant(value))
            };

            var writeValues = new WriteValueCollection { writeValue };
            var response = _session.Write(null, writeValues, out var results, out var diagnosticInfos);

            if (StatusCode.IsGood(results[0]))
                return new ProtocolResponse { Success = true };
            else
                return new ProtocolResponse { Success = false, ErrorMessage = $"Write failed: {results[0]}" };
        }
        catch (Exception ex)
        {
            return new ProtocolResponse { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ProtocolResponse> BrowseAsync(string nodeIdString)
    {
        if (_session == null || !_session.Connected)
            return new ProtocolResponse { Success = false, ErrorMessage = "Not connected" };

        try
        {
            var nodeId = new NodeId(nodeIdString);
            var browseDescription = new BrowseDescription
            {
                NodeId = nodeId,
                BrowseDirection = BrowseDirection.Forward,
                ReferenceTypeId = ReferenceTypeIds.HierarchicalReferences,
                IncludeSubtypes = true,
                NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.ObjectType | NodeClass.VariableType | NodeClass.ReferenceType | NodeClass.DataType | NodeClass.View),
                ResultMask = (uint)BrowseResultMask.All
            };

            _session.Browse(
                null,
                null,
                100,
                new BrowseDescriptionCollection { browseDescription },
                out var results,
                out var diagnosticInfos
            );

            return new ProtocolResponse { Success = true };
        }
        catch (Exception ex)
        {
            return new ProtocolResponse { Success = false, ErrorMessage = ex.Message };
        }
    }

    private static byte[] ConvertToBytes(object value)
    {
        return value switch
        {
            int i => BitConverter.GetBytes(i),
            uint ui => BitConverter.GetBytes(ui),
            short s => BitConverter.GetBytes(s),
            ushort us => BitConverter.GetBytes(us),
            float f => BitConverter.GetBytes(f),
            double d => BitConverter.GetBytes(d),
            long l => BitConverter.GetBytes(l),
            bool b => BitConverter.GetBytes(b),
            byte by => new[] { by },
            _ => System.Text.Encoding.UTF8.GetBytes(value.ToString() ?? string.Empty)
        };
    }
}
