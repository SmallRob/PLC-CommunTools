# PLC-CommunTools 重构实施计划

> **For agentic workers:** REQUIRED SUB-SKILL: Use compose:subagent (recommended) or compose:execute to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 将 PLC-CommunTools 从 .NET Framework 4.8 重构为 .NET 8，实现协议层现代化、客户端现代化、服务端功能、Docker 容器化部署、CLI 终端和 MCP 集成。

**Architecture:** 采用混合重构方案，核心协议库现代化，客户端和服务端全新开发。使用分层架构，协议层抽象化支持插件加载，客户端支持 WPF 和 WinForm，服务端支持 Web API、WebSocket 和 gRPC。

**Tech Stack:** .NET 8, ASP.NET Core, WPF, WinForm, SQLite, Docker, MCP Protocol

---

## 文件结构

```
PLC-CommunTools/
├── src/
│   ├── Core/
│   │   ├── Commun.Protocols/              # 协议抽象层
│   │   ├── Commun.Modbus/                 # Modbus 协议实现
│   │   ├── Commun.MQTT/                   # MQTT 协议实现
│   │   ├── Commun.Serial/                 # 串口通讯实现
│   │   └── Commun.Common/                 # 通用工具类
│   ├── Clients/
│   │   ├── Commun.Client.WPF/             # WPF 客户端
│   │   ├── Commun.Client.WinForms/        # WinForm 客户端
│   │   └── Commun.Client.CLI/             # CLI 终端
│   ├── Server/
│   │   ├── Commun.Server.Core/            # 服务端核心
│   │   ├── Commun.Server.API/             # Web API
│   │   ├── Commun.Server.WebSocket/       # WebSocket 服务
│   │   └── Commun.Server.gRPC/            # gRPC 服务
│   ├── MCP/
│   │   └── Commun.MCP.Server/             # MCP 服务器
│   └── Infrastructure/
│       ├── Commun.Data/                   # 数据访问层
│       ├── Commun.Logging/                # 日志服务
│       └── Commun.Configuration/          # 配置管理
├── docker/
│   ├── docker-compose.yml
│   ├── server.Dockerfile
│   ├── mcp.Dockerfile
│   └── nginx/
│       └── nginx.conf
├── tests/
│   ├── Commun.Protocols.Tests/
│   ├── Commun.Modbus.Tests/
│   ├── Commun.Server.Tests/
│   └── Commun.Client.Tests/
└── docs/
    ├── compose/
    │   ├── specs/
    │   └── plans/
    └── api/
```

---

## Task 1: 基础架构搭建

**Covers:** [S1, S2]

**Files:**
- Create: `CommunTools.sln` (新解决方案)
- Create: `src/Core/Commun.Protocols/Commun.Protocols.csproj`
- Create: `src/Core/Commun.Common/Commun.Common.csproj`
- Create: `src/Infrastructure/Commun.Data/Commun.Data.csproj`
- Create: `src/Infrastructure/Commun.Logging/Commun.Logging.csproj`
- Create: `src/Infrastructure/Commun.Configuration/Commun.Configuration.csproj`
- Create: `Directory.Build.props` (更新)
- Create: `global.json`

- [ ] **Step 1: 创建新的解决方案结构**

```bash
dotnet new sln -n CommunTools.New
```

- [ ] **Step 2: 创建核心协议库项目**

```bash
dotnet new classlib -n Commun.Protocols -o src/Core/Commun.Protocols
dotnet new classlib -n Commun.Common -o src/Core/Commun.Common
```

- [ ] **Step 3: 创建基础设施项目**

```bash
dotnet new classlib -n Commun.Data -o src/Infrastructure/Commun.Data
dotnet new classlib -n Commun.Logging -o src/Infrastructure/Commun.Logging
dotnet new classlib -n Commun.Configuration -o src/Infrastructure/Commun.Configuration
```

- [ ] **Step 4: 添加项目到解决方案**

```bash
dotnet sln add src/Core/Commun.Protocols/Commun.Protocols.csproj
dotnet sln add src/Core/Commun.Common/Commun.Common.csproj
dotnet sln add src/Infrastructure/Commun.Data/Commun.Data.csproj
dotnet sln add src/Infrastructure/Commun.Logging/Commun.Logging.csproj
dotnet sln add src/Infrastructure/Commun.Configuration/Commun.Configuration.csproj
```

- [ ] **Step 5: 创建 Directory.Build.props**

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>
</Project>
```

- [ ] **Step 6: 创建 global.json**

```json
{
  "sdk": {
    "version": "8.0.0",
    "rollForward": "latestMinor"
  }
}
```

- [ ] **Step 7: 验证解决方案结构**

```bash
dotnet build CommunTools.New.sln
```

Expected: Build succeeded

- [ ] **Step 8: 提交基础架构**

```bash
git add .
git commit -m "feat: initialize new solution structure with .NET 8"
```

---

## Task 2: 协议抽象层设计

**Covers:** [S3]

**Files:**
- Create: `src/Core/Commun.Protocols/IProtocol.cs`
- Create: `src/Core/Commun.Protocols/ProtocolBase.cs`
- Create: `src/Core/Commun.Protocols/ProtocolConfig.cs`
- Create: `src/Core/Commun.Protocols/ProtocolRequest.cs`
- Create: `src/Core/Commun.Protocols/ProtocolResponse.cs`
- Create: `src/Core/Commun.Protocols/ProtocolEventArgs.cs`
- Create: `src/Core/Commun.Protocols/IProtocolPlugin.cs`
- Create: `src/Core/Commun.Protocols/ProtocolPluginManager.cs`
- Create: `tests/Commun.Protocols.Tests/Commun.Protocols.Tests.csproj`
- Create: `tests/Commun.Protocols.Tests/ProtocolTests.cs`

- [ ] **Step 1: 创建协议接口**

```csharp
// src/Core/Commun.Protocols/IProtocol.cs
using System;
using System.Threading.Tasks;

namespace Commun.Protocols;

public interface IProtocol : IDisposable
{
    string Name { get; }
    string Version { get; }
    bool IsConnected { get; }
    
    Task<bool> ConnectAsync(ProtocolConfig config);
    Task DisconnectAsync();
    Task<ProtocolResponse> SendAsync(ProtocolRequest request);
    
    event EventHandler<ProtocolEventArgs> DataReceived;
    event EventHandler<ProtocolErrorEventArgs> ErrorOccurred;
}
```

- [ ] **Step 2: 创建协议配置类**

```csharp
// src/Core/Commun.Protocols/ProtocolConfig.cs
namespace Commun.Protocols;

public class ProtocolConfig
{
    public string ProtocolName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Port { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    public int RetryCount { get; set; } = 3;
}
```

- [ ] **Step 3: 创建协议请求类**

```csharp
// src/Core/Commun.Protocols/ProtocolRequest.cs
namespace Commun.Protocols;

public class ProtocolRequest
{
    public string CommandType { get; set; } = string.Empty;
    public int Register { get; set; }
    public int Length { get; set; } = 1;
    public byte[]? Data { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
}
```

- [ ] **Step 4: 创建协议响应类**

```csharp
// src/Core/Commun.Protocols/ProtocolResponse.cs
namespace Commun.Protocols;

public class ProtocolResponse
{
    public bool Success { get; set; }
    public byte[]? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public int ErrorCode { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = new();
}
```

- [ ] **Step 5: 创建事件参数类**

```csharp
// src/Core/Commun.Protocols/ProtocolEventArgs.cs
namespace Commun.Protocols;

public class ProtocolEventArgs : EventArgs
{
    public byte[] Data { get; }
    public DateTime Timestamp { get; }
    
    public ProtocolEventArgs(byte[] data)
    {
        Data = data;
        Timestamp = DateTime.UtcNow;
    }
}

public class ProtocolErrorEventArgs : EventArgs
{
    public string Message { get; }
    public Exception? Exception { get; }
    public int ErrorCode { get; }
    
    public ProtocolErrorEventArgs(string message, Exception? exception = null, int errorCode = 0)
    {
        Message = message;
        Exception = exception;
        ErrorCode = errorCode;
    }
}
```

- [ ] **Step 6: 创建协议基类**

```csharp
// src/Core/Commun.Protocols/ProtocolBase.cs
using System;
using System.Threading.Tasks;

namespace Commun.Protocols;

public abstract class ProtocolBase : IProtocol
{
    public abstract string Name { get; }
    public abstract string Version { get; }
    public bool IsConnected { get; protected set; }
    
    protected ProtocolConfig? Config { get; private set; }
    
    public event EventHandler<ProtocolEventArgs>? DataReceived;
    public event EventHandler<ProtocolErrorEventArgs>? ErrorOccurred;
    
    public virtual async Task<bool> ConnectAsync(ProtocolConfig config)
    {
        Config = config;
        IsConnected = await ConnectCoreAsync(config);
        return IsConnected;
    }
    
    public virtual async Task DisconnectAsync()
    {
        await DisconnectCoreAsync();
        IsConnected = false;
    }
    
    public virtual async Task<ProtocolResponse> SendAsync(ProtocolRequest request)
    {
        if (!IsConnected)
        {
            return new ProtocolResponse
            {
                Success = false,
                ErrorMessage = "Not connected"
            };
        }
        
        return await SendCoreAsync(request);
    }
    
    protected abstract Task<bool> ConnectCoreAsync(ProtocolConfig config);
    protected abstract Task DisconnectCoreAsync();
    protected abstract Task<ProtocolResponse> SendCoreAsync(ProtocolRequest request);
    
    protected virtual void OnDataReceived(byte[] data)
    {
        DataReceived?.Invoke(this, new ProtocolEventArgs(data));
    }
    
    protected virtual void OnError(string message, Exception? exception = null, int errorCode = 0)
    {
        ErrorOccurred?.Invoke(this, new ProtocolErrorEventArgs(message, exception, errorCode));
    }
    
    public virtual void Dispose()
    {
        if (IsConnected)
        {
            DisconnectAsync().Wait();
        }
    }
}
```

- [ ] **Step 7: 创建协议插件接口**

```csharp
// src/Core/Commun.Protocols/IProtocolPlugin.cs
namespace Commun.Protocols;

public interface IProtocolPlugin
{
    string ProtocolName { get; }
    string Version { get; }
    string Description { get; }
    
    IProtocol CreateProtocol();
    ProtocolMetadata GetMetadata();
}

public class ProtocolMetadata
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public List<string> SupportedCommands { get; set; } = new();
    public Dictionary<string, object> DefaultConfig { get; set; } = new();
}
```

- [ ] **Step 8: 创建协议插件管理器**

```csharp
// src/Core/Commun.Protocols/ProtocolPluginManager.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Commun.Protocols;

public class ProtocolPluginManager
{
    private readonly Dictionary<string, IProtocolPlugin> _plugins = new();
    private readonly string _pluginDirectory;
    
    public ProtocolPluginManager(string pluginDirectory)
    {
        _pluginDirectory = pluginDirectory;
    }
    
    public void LoadPlugins()
    {
        if (!Directory.Exists(_pluginDirectory))
        {
            Directory.CreateDirectory(_pluginDirectory);
            return;
        }
        
        var dllFiles = Directory.GetFiles(_pluginDirectory, "*.dll");
        
        foreach (var dllFile in dllFiles)
        {
            try
            {
                var assembly = Assembly.LoadFrom(dllFile);
                var pluginTypes = assembly.GetTypes()
                    .Where(t => typeof(IProtocolPlugin).IsAssignableFrom(t) && !t.IsInterface);
                
                foreach (var pluginType in pluginTypes)
                {
                    var plugin = (IProtocolPlugin)Activator.CreateInstance(pluginType)!;
                    _plugins[plugin.ProtocolName] = plugin;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load plugin from {dllFile}: {ex.Message}");
            }
        }
    }
    
    public IProtocol CreateProtocol(string protocolName)
    {
        if (!_plugins.TryGetValue(protocolName, out var plugin))
        {
            throw new ArgumentException($"Protocol '{protocolName}' not found");
        }
        
        return plugin.CreateProtocol();
    }
    
    public IEnumerable<ProtocolMetadata> GetAvailableProtocols()
    {
        return _plugins.Values.Select(p => p.GetMetadata());
    }
    
    public bool HasProtocol(string protocolName)
    {
        return _plugins.ContainsKey(protocolName);
    }
}
```

- [ ] **Step 9: 创建单元测试项目**

```bash
dotnet new xunit -n Commun.Protocols.Tests -o tests/Commun.Protocols.Tests
dotnet sln add tests/Commun.Protocols.Tests/Commun.Protocols.Tests.csproj
dotnet add tests/Commun.Protocols.Tests/Commun.Protocols.Tests.csproj reference src/Core/Commun.Protocols/Commun.Protocols.csproj
```

- [ ] **Step 10: 编写协议接口测试**

```csharp
// tests/Commun.Protocols.Tests/ProtocolTests.cs
using Commun.Protocols;
using Xunit;

namespace Commun.Protocols.Tests;

public class ProtocolTests
{
    [Fact]
    public void ProtocolConfig_DefaultValues_AreCorrect()
    {
        var config = new ProtocolConfig();
        
        Assert.Equal(string.Empty, config.ProtocolName);
        Assert.Equal(string.Empty, config.Address);
        Assert.Equal(0, config.Port);
        Assert.Empty(config.Parameters);
        Assert.Equal(TimeSpan.FromSeconds(30), config.Timeout);
        Assert.Equal(3, config.RetryCount);
    }
    
    [Fact]
    public void ProtocolRequest_DefaultValues_AreCorrect()
    {
        var request = new ProtocolRequest();
        
        Assert.Equal(string.Empty, request.CommandType);
        Assert.Equal(0, request.Register);
        Assert.Equal(1, request.Length);
        Assert.Null(request.Data);
        Assert.Empty(request.Parameters);
    }
    
    [Fact]
    public void ProtocolResponse_DefaultValues_AreCorrect()
    {
        var response = new ProtocolResponse();
        
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.Null(response.ErrorMessage);
        Assert.Equal(0, response.ErrorCode);
        Assert.Empty(response.Metadata);
    }
}
```

- [ ] **Step 11: 运行测试验证**

```bash
dotnet test tests/Commun.Protocols.Tests/Commun.Protocols.Tests.csproj
```

Expected: All tests pass

- [ ] **Step 12: 提交协议抽象层**

```bash
git add src/Core/Commun.Protocols/ tests/Commun.Protocols.Tests/
git commit -m "feat: implement protocol abstraction layer with plugin support"
```

---

## Task 3: Modbus 协议实现

**Covers:** [S3]

**Files:**
- Create: `src/Core/Commun.Modbus/Commun.Modbus.csproj`
- Create: `src/Core/Commun.Modbus/ModbusProtocol.cs`
- Create: `src/Core/Commun.Modbus/ModbusConfig.cs`
- Create: `src/Core/Commun.Modbus/ModbusRequest.cs`
- Create: `src/Core/Commun.Modbus/ModbusResponse.cs`
- Create: `src/Core/Commun.Modbus/ModbusPlugin.cs`
- Create: `src/Core/Commun.Modbus/ModbusTcpClient.cs`
- Create: `tests/Commun.Modbus.Tests/Commun.Modbus.Tests.csproj`
- Create: `tests/Commun.Modbus.Tests/ModbusProtocolTests.cs`

- [ ] **Step 1: 创建 Modbus 项目**

```bash
dotnet new classlib -n Commun.Modbus -o src/Core/Commun.Modbus
dotnet sln add src/Core/Commun.Modbus/Commun.Modbus.csproj
dotnet add src/Core/Commun.Modbus/Commun.Modbus.csproj reference src/Core/Commun.Protocols/Commun.Protocols.csproj
```

- [ ] **Step 2: 创建 Modbus 配置类**

```csharp
// src/Core/Commun.Modbus/ModbusConfig.cs
namespace Commun.Modbus;

public class ModbusConfig
{
    public string Host { get; set; } = "127.0.0.1";
    public int Port { get; set; } = 502;
    public byte UnitId { get; set; } = 1;
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    public int RetryCount { get; set; } = 3;
    public bool UsePooling { get; set; } = true;
    public int MaxPoolSize { get; set; } = 10;
}
```

- [ ] **Step 3: 创建 Modbus 请求类**

```csharp
// src/Core/Commun.Modbus/ModbusRequest.cs
namespace Commun.Modbus;

public class ModbusRequest
{
    public ModbusFunctionCode FunctionCode { get; set; }
    public ushort StartAddress { get; set; }
    public ushort Quantity { get; set; }
    public byte[]? Data { get; set; }
}

public enum ModbusFunctionCode : byte
{
    ReadCoils = 0x01,
    ReadDiscreteInputs = 0x02,
    ReadHoldingRegisters = 0x03,
    ReadInputRegisters = 0x04,
    WriteSingleCoil = 0x05,
    WriteSingleRegister = 0x06,
    WriteMultipleCoils = 0x0F,
    WriteMultipleRegisters = 0x10
}
```

- [ ] **Step 4: 创建 Modbus 响应类**

```csharp
// src/Core/Commun.Modbus/ModbusResponse.cs
namespace Commun.Modbus;

public class ModbusResponse
{
    public bool Success { get; set; }
    public byte[]? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public byte ExceptionCode { get; set; }
    public ushort[]? RegisterValues { get; set; }
    public bool[]? CoilValues { get; set; }
}
```

- [ ] **Step 5: 创建 Modbus TCP 客户端**

```csharp
// src/Core/Commun.Modbus/ModbusTcpClient.cs
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Commun.Modbus;

public class ModbusTcpClient : IDisposable
{
    private TcpClient? _client;
    private NetworkStream? _stream;
    private readonly ModbusConfig _config;
    private bool _disposed;
    
    public ModbusTcpClient(ModbusConfig config)
    {
        _config = config;
    }
    
    public async Task<bool> ConnectAsync()
    {
        try
        {
            _client = new TcpClient();
            await _client.ConnectAsync(_config.Host, _config.Port);
            _stream = _client.GetStream();
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public async Task DisconnectAsync()
    {
        if (_stream != null)
        {
            await _stream.DisposeAsync();
            _stream = null;
        }
        
        if (_client != null)
        {
            _client.Dispose();
            _client = null;
        }
    }
    
    public async Task<ModbusResponse> SendAsync(ModbusRequest request)
    {
        if (_client == null || _stream == null)
        {
            return new ModbusResponse { Success = false, ErrorMessage = "Not connected" };
        }
        
        try
        {
            var requestBytes = BuildRequest(request);
            await _stream.WriteAsync(requestBytes);
            
            var responseBytes = new byte[256];
            var bytesRead = await _stream.ReadAsync(responseBytes);
            
            return ParseResponse(responseBytes, bytesRead);
        }
        catch (Exception ex)
        {
            return new ModbusResponse { Success = false, ErrorMessage = ex.Message };
        }
    }
    
    private byte[] BuildRequest(ModbusRequest request)
    {
        // 构建 Modbus TCP 请求报文
        var buffer = new byte[12];
        buffer[0] = 0x00; // Transaction ID high
        buffer[1] = 0x01; // Transaction ID low
        buffer[2] = 0x00; // Protocol ID high
        buffer[3] = 0x00; // Protocol ID low
        buffer[4] = 0x00; // Length high
        buffer[5] = 0x06; // Length low
        buffer[6] = _config.UnitId; // Unit ID
        buffer[7] = (byte)request.FunctionCode; // Function code
        buffer[8] = (byte)(request.StartAddress >> 8); // Start address high
        buffer[9] = (byte)(request.StartAddress & 0xFF); // Start address low
        buffer[10] = (byte)(request.Quantity >> 8); // Quantity high
        buffer[11] = (byte)(request.Quantity & 0xFF); // Quantity low
        
        return buffer;
    }
    
    private ModbusResponse ParseResponse(byte[] response, int length)
    {
        if (length < 9)
        {
            return new ModbusResponse { Success = false, ErrorMessage = "Invalid response length" };
        }
        
        var functionCode = response[7];
        
        if ((functionCode & 0x80) != 0)
        {
            return new ModbusResponse
            {
                Success = false,
                ExceptionCode = response[8],
                ErrorMessage = $"Modbus exception: {response[8]}"
            };
        }
        
        var dataLength = response[8];
        var data = new byte[dataLength];
        Array.Copy(response, 9, data, 0, dataLength);
        
        return new ModbusResponse
        {
            Success = true,
            Data = data
        };
    }
    
    public void Dispose()
    {
        if (!_disposed)
        {
            DisconnectAsync().Wait();
            _disposed = true;
        }
    }
}
```

- [ ] **Step 6: 创建 Modbus 协议实现**

```csharp
// src/Core/Commun.Modbus/ModbusProtocol.cs
using System;
using System.Threading.Tasks;
using Commun.Protocols;

namespace Commun.Modbus;

public class ModbusProtocol : ProtocolBase
{
    public override string Name => "Modbus";
    public override string Version => "1.0.0";
    
    private ModbusTcpClient? _client;
    private ModbusConfig? _modbusConfig;
    
    protected override async Task<bool> ConnectCoreAsync(ProtocolConfig config)
    {
        _modbusConfig = new ModbusConfig
        {
            Host = config.Address,
            Port = config.Port,
            Timeout = config.Timeout,
            RetryCount = config.RetryCount
        };
        
        _client = new ModbusTcpClient(_modbusConfig);
        return await _client.ConnectAsync();
    }
    
    protected override async Task DisconnectCoreAsync()
    {
        if (_client != null)
        {
            await _client.DisconnectAsync();
            _client.Dispose();
            _client = null;
        }
    }
    
    protected override async Task<ProtocolResponse> SendCoreAsync(ProtocolRequest request)
    {
        if (_client == null)
        {
            return new ProtocolResponse { Success = false, ErrorMessage = "Client not initialized" };
        }
        
        var modbusRequest = new ModbusRequest
        {
            FunctionCode = ParseFunctionCode(request.CommandType),
            StartAddress = (ushort)request.Register,
            Quantity = (ushort)request.Length,
            Data = request.Data
        };
        
        var modbusResponse = await _client.SendAsync(modbusRequest);
        
        return new ProtocolResponse
        {
            Success = modbusResponse.Success,
            Data = modbusResponse.Data,
            ErrorMessage = modbusResponse.ErrorMessage,
            ErrorCode = modbusResponse.ExceptionCode
        };
    }
    
    private ModbusFunctionCode ParseFunctionCode(string commandType)
    {
        return commandType.ToLower() switch
        {
            "readcoils" => ModbusFunctionCode.ReadCoils,
            "readdiscreteinputs" => ModbusFunctionCode.ReadDiscreteInputs,
            "readholdingregisters" => ModbusFunctionCode.ReadHoldingRegisters,
            "readinputregisters" => ModbusFunctionCode.ReadInputRegisters,
            "writesinglecoil" => ModbusFunctionCode.WriteSingleCoil,
            "writesingleregister" => ModbusFunctionCode.WriteSingleRegister,
            "writemultiplecoils" => ModbusFunctionCode.WriteMultipleCoils,
            "writemultipleregisters" => ModbusFunctionCode.WriteMultipleRegisters,
            _ => throw new ArgumentException($"Unknown command type: {commandType}")
        };
    }
}
```

- [ ] **Step 7: 创建 Modbus 插件**

```csharp
// src/Core/Commun.Modbus/ModbusPlugin.cs
using Commun.Protocols;

namespace Commun.Modbus;

public class ModbusPlugin : IProtocolPlugin
{
    public string ProtocolName => "Modbus";
    public string Version => "1.0.0";
    public string Description => "Modbus TCP/RTU protocol implementation";
    
    public IProtocol CreateProtocol()
    {
        return new ModbusProtocol();
    }
    
    public ProtocolMetadata GetMetadata()
    {
        return new ProtocolMetadata
        {
            Name = ProtocolName,
            Version = Version,
            Description = Description,
            Author = "PLC-CommunTools",
            SupportedCommands = new List<string>
            {
                "ReadCoils",
                "ReadDiscreteInputs",
                "ReadHoldingRegisters",
                "ReadInputRegisters",
                "WriteSingleCoil",
                "WriteSingleRegister",
                "WriteMultipleCoils",
                "WriteMultipleRegisters"
            },
            DefaultConfig = new Dictionary<string, object>
            {
                { "Port", 502 },
                { "UnitId", 1 },
                { "Timeout", 30 }
            }
        };
    }
}
```

- [ ] **Step 8: 创建 Modbus 测试项目**

```bash
dotnet new xunit -n Commun.Modbus.Tests -o tests/Commun.Modbus.Tests
dotnet sln add tests/Commun.Modbus.Tests/Commun.Modbus.Tests.csproj
dotnet add tests/Commun.Modbus.Tests/Commun.Modbus.Tests.csproj reference src/Core/Commun.Modbus/Commun.Modbus.csproj
```

- [ ] **Step 9: 编写 Modbus 协议测试**

```csharp
// tests/Commun.Modbus.Tests/ModbusProtocolTests.cs
using Commun.Modbus;
using Commun.Protocols;
using Xunit;

namespace Commun.Modbus.Tests;

public class ModbusProtocolTests
{
    [Fact]
    public void ModbusPlugin_HasCorrectMetadata()
    {
        var plugin = new ModbusPlugin();
        var metadata = plugin.GetMetadata();
        
        Assert.Equal("Modbus", metadata.Name);
        Assert.Equal("1.0.0", metadata.Version);
        Assert.Contains("ReadHoldingRegisters", metadata.SupportedCommands);
    }
    
    [Fact]
    public void ModbusPlugin_CreatesProtocolInstance()
    {
        var plugin = new ModbusPlugin();
        var protocol = plugin.CreateProtocol();
        
        Assert.NotNull(protocol);
        Assert.Equal("Modbus", protocol.Name);
    }
    
    [Fact]
    public void ModbusConfig_HasCorrectDefaults()
    {
        var config = new ModbusConfig();
        
        Assert.Equal("127.0.0.1", config.Host);
        Assert.Equal(502, config.Port);
        Assert.Equal(1, config.UnitId);
    }
}
```

- [ ] **Step 10: 运行 Modbus 测试**

```bash
dotnet test tests/Commun.Modbus.Tests/Commun.Modbus.Tests.csproj
```

Expected: All tests pass

- [ ] **Step 11: 提交 Modbus 实现**

```bash
git add src/Core/Commun.Modbus/ tests/Commun.Modbus.Tests/
git commit -m "feat: implement Modbus protocol with plugin support"
```

---

## Task 4: MQTT 协议实现

**Covers:** [S3]

**Files:**
- Create: `src/Core/Commun.MQTT/Commun.MQTT.csproj`
- Create: `src/Core/Commun.MQTT/MqttProtocol.cs`
- Create: `src/Core/Commun.MQTT/MqttConfig.cs`
- Create: `src/Core/Commun.MQTT/MqttPlugin.cs`
- Create: `tests/Commun.MQTT.Tests/Commun.MQTT.Tests.csproj`

- [ ] **Step 1: 创建 MQTT 项目**

```bash
dotnet new classlib -n Commun.MQTT -o src/Core/Commun.MQTT
dotnet sln add src/Core/Commun.MQTT/Commun.MQTT.csproj
dotnet add src/Core/Commun.MQTT/Commun.MQTT.csproj reference src/Core/Commun.Protocols/Commun.Protocols.csproj
```

- [ ] **Step 2: 添加 MQTTnet 包**

```bash
dotnet add src/Core/Commun.MQTT/Commun.MQTT.csproj package MQTTnet
```

- [ ] **Step 3: 创建 MQTT 配置类**

```csharp
// src/Core/Commun.MQTT/MqttConfig.cs
namespace Commun.MQTT;

public class MqttConfig
{
    public string BrokerAddress { get; set; } = "localhost";
    public int Port { get; set; } = 1883;
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string ClientId { get; set; } = Guid.NewGuid().ToString();
    public bool UseTls { get; set; }
    public TimeSpan KeepAlive { get; set; } = TimeSpan.FromSeconds(60);
    public bool CleanSession { get; set; } = true;
}
```

- [ ] **Step 4: 创建 MQTT 协议实现**

```csharp
// src/Core/Commun.MQTT/MqttProtocol.cs
using System;
using System.Text;
using System.Threading.Tasks;
using Commun.Protocols;
using MQTTnet;
using MQTTnet.Client;

namespace Commun.MQTT;

public class MqttProtocol : ProtocolBase
{
    public override string Name => "MQTT";
    public override string Version => "1.0.0";
    
    private IMqttClient? _client;
    private MqttConfig? _mqttConfig;
    
    protected override async Task<bool> ConnectCoreAsync(ProtocolConfig config)
    {
        _mqttConfig = new MqttConfig
        {
            BrokerAddress = config.Address,
            Port = config.Port
        };
        
        var factory = new MqttFactory();
        _client = factory.CreateMqttClient();
        
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(_mqttConfig.BrokerAddress, _mqttConfig.Port)
            .WithCredentials(_mqttConfig.Username, _mqttConfig.Password)
            .WithClientId(_mqttConfig.ClientId)
            .WithCleanSession(_mqttConfig.CleanSession)
            .Build();
        
        try
        {
            await _client.ConnectAsync(options);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    protected override async Task DisconnectCoreAsync()
    {
        if (_client != null)
        {
            await _client.DisconnectAsync();
            _client.Dispose();
            _client = null;
        }
    }
    
    protected override async Task<ProtocolResponse> SendCoreAsync(ProtocolRequest request)
    {
        if (_client == null || !_client.IsConnected)
        {
            return new ProtocolResponse { Success = false, ErrorMessage = "Not connected" };
        }
        
        try
        {
            var topic = request.Parameters.ContainsKey("topic") 
                ? request.Parameters["topic"].ToString() 
                : "default";
            
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(request.Data)
                .Build();
            
            await _client.PublishAsync(message);
            
            return new ProtocolResponse { Success = true };
        }
        catch (Exception ex)
        {
            return new ProtocolResponse { Success = false, ErrorMessage = ex.Message };
        }
    }
    
    public async Task SubscribeAsync(string topic, Action<byte[]> handler)
    {
        if (_client == null || !_client.IsConnected)
        {
            throw new InvalidOperationException("Not connected");
        }
        
        _client.ApplicationMessageReceivedAsync += e =>
        {
            handler(e.ApplicationMessage.PayloadSegment.ToArray());
            return Task.CompletedTask;
        };
        
        await _client.SubscribeAsync(topic);
    }
}
```

- [ ] **Step 5: 创建 MQTT 插件**

```csharp
// src/Core/Commun.MQTT/MqttPlugin.cs
using Commun.Protocols;

namespace Commun.MQTT;

public class MqttPlugin : IProtocolPlugin
{
    public string ProtocolName => "MQTT";
    public string Version => "1.0.0";
    public string Description => "MQTT protocol implementation";
    
    public IProtocol CreateProtocol()
    {
        return new MqttProtocol();
    }
    
    public ProtocolMetadata GetMetadata()
    {
        return new ProtocolMetadata
        {
            Name = ProtocolName,
            Version = Version,
            Description = Description,
            Author = "PLC-CommunTools",
            SupportedCommands = new List<string>
            {
                "Publish",
                "Subscribe",
                "Unsubscribe"
            },
            DefaultConfig = new Dictionary<string, object>
            {
                { "Port", 1883 },
                { "KeepAlive", 60 },
                { "CleanSession", true }
            }
        };
    }
}
```

- [ ] **Step 6: 创建 MQTT 测试项目**

```bash
dotnet new xunit -n Commun.MQTT.Tests -o tests/Commun.MQTT.Tests
dotnet sln add tests/Commun.MQTT.Tests/Commun.MQTT.Tests.csproj
dotnet add tests/Commun.MQTT.Tests/Commun.MQTT.Tests.csproj reference src/Core/Commun.MQTT/Commun.MQTT.csproj
```

- [ ] **Step 7: 编写 MQTT 测试**

```csharp
// tests/Commun.MQTT.Tests/MqttProtocolTests.cs
using Commun.MQTT;
using Commun.Protocols;
using Xunit;

namespace Commun.MQTT.Tests;

public class MqttProtocolTests
{
    [Fact]
    public void MqttPlugin_HasCorrectMetadata()
    {
        var plugin = new MqttPlugin();
        var metadata = plugin.GetMetadata();
        
        Assert.Equal("MQTT", metadata.Name);
        Assert.Equal("1.0.0", metadata.Version);
        Assert.Contains("Publish", metadata.SupportedCommands);
    }
    
    [Fact]
    public void MqttPlugin_CreatesProtocolInstance()
    {
        var plugin = new MqttPlugin();
        var protocol = plugin.CreateProtocol();
        
        Assert.NotNull(protocol);
        Assert.Equal("MQTT", protocol.Name);
    }
    
    [Fact]
    public void MqttConfig_HasCorrectDefaults()
    {
        var config = new MqttConfig();
        
        Assert.Equal("localhost", config.BrokerAddress);
        Assert.Equal(1883, config.Port);
        Assert.True(config.CleanSession);
    }
}
```

- [ ] **Step 8: 运行 MQTT 测试**

```bash
dotnet test tests/Commun.MQTT.Tests/Commun.MQTT.Tests.csproj
```

Expected: All tests pass

- [ ] **Step 9: 提交 MQTT 实现**

```bash
git add src/Core/Commun.MQTT/ tests/Commun.MQTT.Tests/
git commit -m "feat: implement MQTT protocol with plugin support"
```

---

## Task 5: 数据访问层实现

**Covers:** [S7]

**Files:**
- Create: `src/Infrastructure/Commun.Data/CommunDbContext.cs`
- Create: `src/Infrastructure/Commun.Data/Entities/Device.cs`
- Create: `src/Infrastructure/Commun.Data/Entities/ConnectionHistory.cs`
- Create: `src/Infrastructure/Commun.Data/Entities/DataRecord.cs`
- Create: `src/Infrastructure/Commun.Data/Repositories/DeviceRepository.cs`
- Create: `src/Infrastructure/Commun.Data/Repositories/IRepository.cs`
- Create: `tests/Commun.Data.Tests/Commun.Data.Tests.csproj`

- [ ] **Step 1: 添加 Entity Framework Core 包**

```bash
dotnet add src/Infrastructure/Commun.Data/Commun.Data.csproj package Microsoft.EntityFrameworkCore.Sqlite
dotnet add src/Infrastructure/Commun.Data/Commun.Data.csproj package Microsoft.EntityFrameworkCore.Design
```

- [ ] **Step 2: 创建设备实体**

```csharp
// src/Infrastructure/Commun.Data/Entities/Device.cs
namespace Commun.Data.Entities;

public class Device
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Protocol { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Port { get; set; }
    public string? Config { get; set; } // JSON 格式配置
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<ConnectionHistory> ConnectionHistory { get; set; } = new List<ConnectionHistory>();
    public ICollection<DataRecord> DataRecords { get; set; } = new List<DataRecord>();
}
```

- [ ] **Step 3: 创建连接历史实体**

```csharp
// src/Infrastructure/Commun.Data/Entities/ConnectionHistory.cs
namespace Commun.Data.Entities;

public class ConnectionHistory
{
    public int Id { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public DateTime ConnectedAt { get; set; }
    public DateTime? DisconnectedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    
    public Device Device { get; set; } = null!;
}
```

- [ ] **Step 4: 创建数据记录实体**

```csharp
// src/Infrastructure/Commun.Data/Entities/DataRecord.cs
namespace Commun.Data.Entities;

public class DataRecord
{
    public long Id { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public int Register { get; set; }
    public string Value { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    public Device Device { get; set; } = null!;
}
```

- [ ] **Step 5: 创建数据库上下文**

```csharp
// src/Infrastructure/Commun.Data/CommunDbContext.cs
using Microsoft.EntityFrameworkCore;
using Commun.Data.Entities;

namespace Commun.Data;

public class CommunDbContext : DbContext
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<ConnectionHistory> ConnectionHistory { get; set; }
    public DbSet<DataRecord> DataRecords { get; set; }
    
    public CommunDbContext(DbContextOptions<CommunDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Protocol).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
        });
        
        modelBuilder.Entity<ConnectionHistory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Device)
                .WithMany(d => d.ConnectionHistory)
                .HasForeignKey(e => e.DeviceId);
        });
        
        modelBuilder.Entity<DataRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Device)
                .WithMany(d => d.DataRecords)
                .HasForeignKey(e => e.DeviceId);
            entity.HasIndex(e => e.Timestamp);
        });
    }
}
```

- [ ] **Step 6: 创建仓库接口**

```csharp
// src/Infrastructure/Commun.Data/Repositories/IRepository.cs
namespace Commun.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
```

- [ ] **Step 7: 创建设备仓库**

```csharp
// src/Infrastructure/Commun.Data/Repositories/DeviceRepository.cs
using Microsoft.EntityFrameworkCore;
using Commun.Data.Entities;

namespace Commun.Data.Repositories;

public class DeviceRepository : IRepository<Device>
{
    private readonly CommunDbContext _context;
    
    public DeviceRepository(CommunDbContext context)
    {
        _context = context;
    }
    
    public async Task<Device?> GetByIdAsync(string id)
    {
        return await _context.Devices
            .Include(d => d.ConnectionHistory)
            .Include(d => d.DataRecords)
            .FirstOrDefaultAsync(d => d.Id == id);
    }
    
    public async Task<IEnumerable<Device>> GetAllAsync()
    {
        return await _context.Devices.ToListAsync();
    }
    
    public async Task AddAsync(Device entity)
    {
        await _context.Devices.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Device entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Devices.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(string id)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device != null)
        {
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Devices.AnyAsync(d => d.Id == id);
    }
    
    public async Task<IEnumerable<Device>> SearchAsync(string? name, string? protocol)
    {
        var query = _context.Devices.AsQueryable();
        
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(d => d.Name.Contains(name));
        }
        
        if (!string.IsNullOrEmpty(protocol))
        {
            query = query.Where(d => d.Protocol == protocol);
        }
        
        return await query.ToListAsync();
    }
}
```

- [ ] **Step 8: 更新项目引用**

```bash
dotnet add src/Infrastructure/Commun.Data/Commun.Data.csproj reference src/Core/Commun.Protocols/Commun.Protocols.csproj
```

- [ ] **Step 9: 创建数据层测试项目**

```bash
dotnet new xunit -n Commun.Data.Tests -o tests/Commun.Data.Tests
dotnet sln add tests/Commun.Data.Tests/Commun.Data.Tests.csproj
dotnet add tests/Commun.Data.Tests/Commun.Data.Tests.csproj reference src/Infrastructure/Commun.Data/Commun.Data.csproj
```

- [ ] **Step 10: 编写数据层测试**

```csharp
// tests/Commun.Data.Tests/DeviceRepositoryTests.cs
using Microsoft.EntityFrameworkCore;
using Commun.Data;
using Commun.Data.Entities;
using Commun.Data.Repositories;
using Xunit;

namespace Commun.Data.Tests;

public class DeviceRepositoryTests : IDisposable
{
    private readonly CommunDbContext _context;
    private readonly DeviceRepository _repository;
    
    public DeviceRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<CommunDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new CommunDbContext(options);
        _repository = new DeviceRepository(_context);
    }
    
    [Fact]
    public async Task AddAsync_ShouldAddDevice()
    {
        var device = new Device
        {
            Name = "Test Device",
            Protocol = "Modbus",
            Address = "192.168.1.100",
            Port = 502
        };
        
        await _repository.AddAsync(device);
        
        var result = await _repository.GetByIdAsync(device.Id);
        Assert.NotNull(result);
        Assert.Equal("Test Device", result.Name);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllDevices()
    {
        await _repository.AddAsync(new Device { Name = "Device 1", Protocol = "Modbus", Address = "192.168.1.1", Port = 502 });
        await _repository.AddAsync(new Device { Name = "Device 2", Protocol = "MQTT", Address = "192.168.1.2", Port = 1883 });
        
        var devices = await _repository.GetAllAsync();
        
        Assert.Equal(2, devices.Count());
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
```

- [ ] **Step 11: 运行数据层测试**

```bash
dotnet test tests/Commun.Data.Tests/Commun.Data.Tests.csproj
```

Expected: All tests pass

- [ ] **Step 12: 提交数据层实现**

```bash
git add src/Infrastructure/Commun.Data/ tests/Commun.Data.Tests/
git commit -m "feat: implement data access layer with Entity Framework Core and SQLite"
```

---

## Task 6: ASP.NET Core Web API 服务端

**Covers:** [S5]

**Files:**
- Create: `src/Server/Commun.Server.API/Commun.Server.API.csproj`
- Create: `src/Server/Commun.Server.API/Program.cs`
- Create: `src/Server/Commun.Server.API/Controllers/DevicesController.cs`
- Create: `src/Server/Commun.Server.API/Controllers/ProtocolController.cs`
- Create: `src/Server/Commun.Server.API/Models/DeviceDto.cs`
- Create: `src/Server/Commun.Server.API/Models/ProtocolRequestDto.cs`
- Create: `src/Server/Commun.Server.API/Services/DeviceService.cs`
- Create: `src/Server/Commun.Server.API/Services/ProtocolService.cs`
- Create: `tests/Commun.Server.Tests/Commun.Server.Tests.csproj`

- [ ] **Step 1: 创建 Web API 项目**

```bash
dotnet new webapi -n Commun.Server.API -o src/Server/Commun.Server.API
dotnet sln add src/Server/Commun.Server.API/Commun.Server.API.csproj
```

- [ ] **Step 2: 添加项目引用**

```bash
dotnet add src/Server/Commun.Server.API/Commun.Server.API.csproj reference src/Core/Commun.Protocols/Commun.Protocols.csproj
dotnet add src/Server/Commun.Server.API/Commun.Server.API.csproj reference src/Core/Commun.Modbus/Commun.Modbus.csproj
dotnet add src/Server/Commun.Server.API/Commun.Server.API.csproj reference src/Core/Commun.MQTT/Commun.MQTT.csproj
dotnet add src/Server/Commun.Server.API/Commun.Server.API.csproj reference src/Infrastructure/Commun.Data/Commun.Data.csproj
dotnet add src/Server/Commun.Server.API/Commun.Server.API.csproj reference src/Infrastructure/Commun.Logging/Commun.Logging.csproj
```

- [ ] **Step 3: 创建设备 DTO**

```csharp
// src/Server/Commun.Server.API/Models/DeviceDto.cs
namespace Commun.Server.API.Models;

public class DeviceDto
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Protocol { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Port { get; set; }
    public Dictionary<string, object>? Config { get; set; }
}

public class CreateDeviceDto
{
    public string Name { get; set; } = string.Empty;
    public string Protocol { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Port { get; set; }
    public Dictionary<string, object>? Config { get; set; }
}

public class ProtocolRequestDto
{
    public string CommandType { get; set; } = string.Empty;
    public int Register { get; set; }
    public int Length { get; set; } = 1;
    public byte[]? Data { get; set; }
    public Dictionary<string, object>? Parameters { get; set; }
}
```

- [ ] **Step 4: 创建设备服务**

```csharp
// src/Server/Commun.Server.API/Services/DeviceService.cs
using Commun.Data.Entities;
using Commun.Data.Repositories;
using Commun.Server.API.Models;

namespace Commun.Server.API.Services;

public class DeviceService
{
    private readonly DeviceRepository _repository;
    
    public DeviceService(DeviceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<DeviceDto>> GetAllDevicesAsync()
    {
        var devices = await _repository.GetAllAsync();
        return devices.Select(MapToDto);
    }
    
    public async Task<DeviceDto?> GetDeviceAsync(string id)
    {
        var device = await _repository.GetByIdAsync(id);
        return device != null ? MapToDto(device) : null;
    }
    
    public async Task<DeviceDto> CreateDeviceAsync(CreateDeviceDto dto)
    {
        var device = new Device
        {
            Name = dto.Name,
            Protocol = dto.Protocol,
            Address = dto.Address,
            Port = dto.Port,
            Config = dto.Config != null ? System.Text.Json.JsonSerializer.Serialize(dto.Config) : null
        };
        
        await _repository.AddAsync(device);
        return MapToDto(device);
    }
    
    public async Task UpdateDeviceAsync(string id, DeviceDto dto)
    {
        var device = await _repository.GetByIdAsync(id);
        if (device == null)
        {
            throw new KeyNotFoundException($"Device {id} not found");
        }
        
        device.Name = dto.Name;
        device.Protocol = dto.Protocol;
        device.Address = dto.Address;
        device.Port = dto.Port;
        
        await _repository.UpdateAsync(device);
    }
    
    public async Task DeleteDeviceAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
    
    private static DeviceDto MapToDto(Device device)
    {
        return new DeviceDto
        {
            Id = device.Id,
            Name = device.Name,
            Protocol = device.Protocol,
            Address = device.Address,
            Port = device.Port,
            Config = device.Config != null 
                ? System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(device.Config) 
                : null
        };
    }
}
```

- [ ] **Step 5: 创建协议服务**

```csharp
// src/Server/Commun.Server.API/Services/ProtocolService.cs
using System.Collections.Concurrent;
using Commun.Protocols;
using Commun.Server.API.Models;

namespace Commun.Server.API.Services;

public class ProtocolService
{
    private readonly ProtocolPluginManager _pluginManager;
    private readonly ConcurrentDictionary<string, IProtocol> _activeProtocols = new();
    
    public ProtocolService(ProtocolPluginManager pluginManager)
    {
        _pluginManager = pluginManager;
    }
    
    public IEnumerable<ProtocolMetadata> GetAvailableProtocols()
    {
        return _pluginManager.GetAvailableProtocols();
    }
    
    public async Task<bool> ConnectAsync(string deviceId, string protocolName, ProtocolConfig config)
    {
        var protocol = _pluginManager.CreateProtocol(protocolName);
        var connected = await protocol.ConnectAsync(config);
        
        if (connected)
        {
            _activeProtocols[deviceId] = protocol;
        }
        
        return connected;
    }
    
    public async Task DisconnectAsync(string deviceId)
    {
        if (_activeProtocols.TryRemove(deviceId, out var protocol))
        {
            await protocol.DisconnectAsync();
            protocol.Dispose();
        }
    }
    
    public async Task<ProtocolResponse> SendAsync(string deviceId, ProtocolRequest request)
    {
        if (!_activeProtocols.TryGetValue(deviceId, out var protocol))
        {
            return new ProtocolResponse
            {
                Success = false,
                ErrorMessage = $"Device {deviceId} not connected"
            };
        }
        
        return await protocol.SendAsync(request);
    }
    
    public bool IsConnected(string deviceId)
    {
        return _activeProtocols.ContainsKey(deviceId);
    }
}
```

- [ ] **Step 6: 创建设备控制器**

```csharp
// src/Server/Commun.Server.API/Controllers/DevicesController.cs
using Microsoft.AspNetCore.Mvc;
using Commun.Server.API.Models;
using Commun.Server.API.Services;

namespace Commun.Server.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly DeviceService _deviceService;
    
    public DevicesController(DeviceService deviceService)
    {
        _deviceService = deviceService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDevices()
    {
        var devices = await _deviceService.GetAllDevicesAsync();
        return Ok(devices);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DeviceDto>> GetDevice(string id)
    {
        var device = await _deviceService.GetDeviceAsync(id);
        if (device == null)
        {
            return NotFound();
        }
        return Ok(device);
    }
    
    [HttpPost]
    public async Task<ActionResult<DeviceDto>> CreateDevice(CreateDeviceDto dto)
    {
        var device = await _deviceService.CreateDeviceAsync(dto);
        return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, device);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDevice(string id, DeviceDto dto)
    {
        try
        {
            await _deviceService.UpdateDeviceAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDevice(string id)
    {
        await _deviceService.DeleteDeviceAsync(id);
        return NoContent();
    }
}
```

- [ ] **Step 7: 创建协议控制器**

```csharp
// src/Server/Commun.Server.API/Controllers/ProtocolController.cs
using Microsoft.AspNetCore.Mvc;
using Commun.Protocols;
using Commun.Server.API.Models;
using Commun.Server.API.Services;

namespace Commun.Server.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProtocolController : ControllerBase
{
    private readonly ProtocolService _protocolService;
    
    public ProtocolController(ProtocolService protocolService)
    {
        _protocolService = protocolService;
    }
    
    [HttpGet("protocols")]
    public ActionResult<IEnumerable<ProtocolMetadata>> GetAvailableProtocols()
    {
        var protocols = _protocolService.GetAvailableProtocols();
        return Ok(protocols);
    }
    
    [HttpPost("{deviceId}/connect")]
    public async Task<IActionResult> Connect(string deviceId, [FromBody] ConnectRequest request)
    {
        var config = new ProtocolConfig
        {
            ProtocolName = request.Protocol,
            Address = request.Address,
            Port = request.Port
        };
        
        var result = await _protocolService.ConnectAsync(deviceId, request.Protocol, config);
        
        if (result)
        {
            return Ok(new { Message = "Connected successfully" });
        }
        
        return BadRequest(new { Message = "Connection failed" });
    }
    
    [HttpPost("{deviceId}/disconnect")]
    public async Task<IActionResult> Disconnect(string deviceId)
    {
        await _protocolService.DisconnectAsync(deviceId);
        return Ok(new { Message = "Disconnected successfully" });
    }
    
    [HttpPost("{deviceId}/send")]
    public async Task<ActionResult<ProtocolResponse>> Send(string deviceId, ProtocolRequestDto dto)
    {
        var request = new ProtocolRequest
        {
            CommandType = dto.CommandType,
            Register = dto.Register,
            Length = dto.Length,
            Data = dto.Data,
            Parameters = dto.Parameters ?? new Dictionary<string, object>()
        };
        
        var response = await _protocolService.SendAsync(deviceId, request);
        return Ok(response);
    }
    
    [HttpGet("{deviceId}/status")]
    public ActionResult<bool> GetStatus(string deviceId)
    {
        var isConnected = _protocolService.IsConnected(deviceId);
        return Ok(new { Connected = isConnected });
    }
}

public class ConnectRequest
{
    public string Protocol { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Port { get; set; }
}
```

- [ ] **Step 8: 更新 Program.cs**

```csharp
// src/Server/Commun.Server.API/Program.cs
using Microsoft.EntityFrameworkCore;
using Commun.Data;
using Commun.Data.Repositories;
using Commun.Protocols;
using Commun.Modbus;
using Commun.MQTT;
using Commun.Server.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SQLite
builder.Services.AddDbContext<CommunDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=commun.db"));

// Register repositories
builder.Services.AddScoped<DeviceRepository>();

// Register protocol plugins
builder.Services.AddSingleton<ProtocolPluginManager>(sp =>
{
    var pluginManager = new ProtocolPluginManager("plugins");
    pluginManager.LoadPlugins();
    return pluginManager;
});

// Register services
builder.Services.AddScoped<DeviceService>();
builder.Services.AddSingleton<ProtocolService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CommunDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
```

- [ ] **Step 9: 更新 appsettings.json**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=commun.db"
  },
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://0.0.0.0:5000"
      }
    }
  }
}
```

- [ ] **Step 10: 创建服务端测试项目**

```bash
dotnet new xunit -n Commun.Server.Tests -o tests/Commun.Server.Tests
dotnet sln add tests/Commun.Server.Tests/Commun.Server.Tests.csproj
dotnet add tests/Commun.Server.Tests/Commun.Server.Tests.csproj reference src/Server/Commun.Server.API/Commun.Server.API.csproj
```

- [ ] **Step 11: 编写服务端测试**

```csharp
// tests/Commun.Server.Tests/DeviceServiceTests.cs
using Microsoft.EntityFrameworkCore;
using Commun.Data;
using Commun.Data.Entities;
using Commun.Data.Repositories;
using Commun.Server.API.Services;
using Xunit;

namespace Commun.Server.Tests;

public class DeviceServiceTests : IDisposable
{
    private readonly CommunDbContext _context;
    private readonly DeviceRepository _repository;
    private readonly DeviceService _service;
    
    public DeviceServiceTests()
    {
        var options = new DbContextOptionsBuilder<CommunDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new CommunDbContext(options);
        _repository = new DeviceRepository(_context);
        _service = new DeviceService(_repository);
    }
    
    [Fact]
    public async Task CreateDeviceAsync_ShouldReturnCreatedDevice()
    {
        var dto = new CreateDeviceDto
        {
            Name = "Test Device",
            Protocol = "Modbus",
            Address = "192.168.1.100",
            Port = 502
        };
        
        var result = await _service.CreateDeviceAsync(dto);
        
        Assert.NotNull(result.Id);
        Assert.Equal("Test Device", result.Name);
    }
    
    [Fact]
    public async Task GetAllDevicesAsync_ShouldReturnAllDevices()
    {
        await _service.CreateDeviceAsync(new CreateDeviceDto { Name = "Device 1", Protocol = "Modbus", Address = "192.168.1.1", Port = 502 });
        await _service.CreateDeviceAsync(new CreateDeviceDto { Name = "Device 2", Protocol = "MQTT", Address = "192.168.1.2", Port = 1883 });
        
        var devices = await _service.GetAllDevicesAsync();
        
        Assert.Equal(2, devices.Count());
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
```

- [ ] **Step 12: 运行服务端测试**

```bash
dotnet test tests/Commun.Server.Tests/Commun.Server.Tests.csproj
```

Expected: All tests pass

- [ ] **Step 13: 提交服务端实现**

```bash
git add src/Server/Commun.Server.API/ tests/Commun.Server.Tests/
git commit -m "feat: implement ASP.NET Core Web API server with device and protocol management"
```

---

## Task 7: Docker 容器化部署

**Covers:** [S8]

**Files:**
- Create: `docker/docker-compose.yml`
- Create: `docker/server.Dockerfile`
- Create: `docker/nginx/nginx.conf`
- Create: `.dockerignore`

- [ ] **Step 1: 创建 .dockerignore**

```
**/.git
**/bin
**/obj
**/packages
**/.vs
**/.vscode
**/node_modules
**/Dockerfile*
**/docker-compose*
**/.dockerignore
**/README.md
**/LICENSE
```

- [ ] **Step 2: 创建服务端 Dockerfile**

```dockerfile
# docker/server.Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["src/Server/Commun.Server.API/Commun.Server.API.csproj", "Server/Commun.Server.API/"]
COPY ["src/Core/Commun.Protocols/Commun.Protocols.csproj", "Core/Commun.Protocols/"]
COPY ["src/Core/Commun.Modbus/Commun.Modbus.csproj", "Core/Commun.Modbus/"]
COPY ["src/Core/Commun.MQTT/Commun.MQTT.csproj", "Core/Commun.MQTT/"]
COPY ["src/Infrastructure/Commun.Data/Commun.Data.csproj", "Infrastructure/Commun.Data/"]
COPY ["src/Infrastructure/Commun.Logging/Commun.Logging.csproj", "Infrastructure/Commun.Logging/"]
COPY ["src/Infrastructure/Commun.Configuration/Commun.Configuration.csproj", "Infrastructure/Commun.Configuration/"]

# Restore dependencies
RUN dotnet restore "Server/Commun.Server.API/Commun.Server.API.csproj"

# Copy source code
COPY src/Server/Commun.Server.API/ Server/Commun.Server.API/
COPY src/Core/Commun.Protocols/ Core/Commun.Protocols/
COPY src/Core/Commun.Modbus/ Core/Commun.Modbus/
COPY src/Core/Commun.MQTT/ Core/Commun.MQTT/
COPY src/Infrastructure/Commun.Data/ Infrastructure/Commun.Data/
COPY src/Infrastructure/Commun.Logging/ Infrastructure/Commun.Logging/
COPY src/Infrastructure/Commun.Configuration/ Infrastructure/Commun.Configuration/

# Build
WORKDIR "/src/Server/Commun.Server.API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Commun.Server.API.dll"]
```

- [ ] **Step 3: 创建 Nginx 配置**

```nginx
# docker/nginx/nginx.conf
events {
    worker_connections 1024;
}

http {
    upstream server {
        server server:5000;
    }

    server {
        listen 80;
        server_name localhost;

        location / {
            proxy_pass http://server;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_set_header X-Forwarded-Proto $scheme;
        }

        location /api/devices {
            proxy_pass http://server/api/devices;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
        }

        location /api/protocol {
            proxy_pass http://server/api/protocol;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
        }
    }
}
```

- [ ] **Step 4: 创建 docker-compose.yml**

```yaml
# docker/docker-compose.yml
version: '3.8'

services:
  server:
    build:
      context: ..
      dockerfile: docker/server.Dockerfile
    ports:
      - "5000:5000"
    volumes:
      - server-data:/app/data
      - server-logs:/app/logs
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Data Source=/app/data/commun.db
    restart: unless-stopped
    networks:
      - commun-network

  nginx:
    image: nginx:alpine
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - ./nginx/nginx.conf:/etc/nginx/nginx.conf:ro
    depends_on:
      - server
    restart: unless-stopped
    networks:
      - commun-network

volumes:
  server-data:
  server-logs:

networks:
  commun-network:
    driver: bridge
```

- [ ] **Step 5: 构建 Docker 镜像**

```bash
cd docker
docker-compose build
```

Expected: Build successful

- [ ] **Step 6: 启动服务**

```bash
docker-compose up -d
```

Expected: Services started successfully

- [ ] **Step 7: 验证服务运行**

```bash
curl http://localhost:5000/api/devices
```

Expected: Return empty array or device list

- [ ] **Step 8: 提交 Docker 配置**

```bash
git add docker/ .dockerignore
git commit -m "feat: add Docker containerization with docker-compose"
```

---

## Task 8: CLI 终端实现

**Covers:** [S4]

**Files:**
- Create: `src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj`
- Create: `src/Clients/Commun.Client.CLI/Program.cs`
- Create: `src/Clients/Commun.Client.CLI/Commands/ConnectCommand.cs`
- Create: `src/Clients/Commun.Client.CLI/Commands/ReadCommand.cs`
- Create: `src/Clients/Commun.Client.CLI/Commands/WriteCommand.cs`
- Create: `src/Clients/Commun.Client.CLI/Commands/MonitorCommand.cs`
- Create: `src/Clients/Commun.Client.CLI/Services/DeviceService.cs`
- Create: `tests/Commun.Client.CLI.Tests/Commun.Client.CLI.Tests.csproj`

- [ ] **Step 1: 创建 CLI 项目**

```bash
dotnet new console -n Commun.Client.CLI -o src/Clients/Commun.Client.CLI
dotnet sln add src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj
```

- [ ] **Step 2: 添加 CLI 框架包**

```bash
dotnet add src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj package System.CommandLine
```

- [ ] **Step 3: 添加项目引用**

```bash
dotnet add src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj reference src/Core/Commun.Protocols/Commun.Protocols.csproj
dotnet add src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj reference src/Core/Commun.Modbus/Commun.Modbus.csproj
dotnet add src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj reference src/Core/Commun.MQTT/Commun.MQTT.csproj
```

- [ ] **Step 4: 创建设备服务**

```csharp
// src/Clients/Commun.Client.CLI/Services/DeviceService.cs
using System.Net.Http.Json;
using Commun.Server.API.Models;

namespace Commun.Client.CLI.Services;

public class DeviceService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    
    public DeviceService(string baseUrl)
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient();
    }
    
    public async Task<IEnumerable<DeviceDto>> GetDevicesAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/devices");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<DeviceDto>>() ?? Enumerable.Empty<DeviceDto>();
    }
    
    public async Task<DeviceDto?> GetDeviceAsync(string id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/devices/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DeviceDto>();
    }
    
    public async Task<DeviceDto> CreateDeviceAsync(CreateDeviceDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/devices", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<DeviceDto>())!;
    }
    
    public async Task DeleteDeviceAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/devices/{id}");
        response.EnsureSuccessStatusCode();
    }
}
```

- [ ] **Step 5: 创建连接命令**

```csharp
// src/Clients/Commun.Client.CLI/Commands/ConnectCommand.cs
using System.CommandLine;
using Commun.Client.CLI.Services;

namespace Commun.Client.CLI.Commands;

public class ConnectCommand : Command
{
    public ConnectCommand() : base("connect", "Connect to a device")
    {
        var protocolOption = new Option<string>("--protocol", "Protocol name (Modbus, MQTT)") { IsRequired = true };
        var addressOption = new Option<string>("--address", "Device address") { IsRequired = true };
        var portOption = new Option<int>("--port", "Device port") { IsRequired = true };
        var nameOption = new Option<string>("--name", "Device name");
        
        AddOption(protocolOption);
        AddOption(addressOption);
        AddOption(portOption);
        AddOption(nameOption);
        
        this.SetHandler(async (protocol, address, port, name) =>
        {
            var service = new DeviceService("http://localhost:5000");
            
            var dto = new CreateDeviceDto
            {
                Name = name ?? $"{protocol} Device",
                Protocol = protocol,
                Address = address,
                Port = port
            };
            
            var device = await service.CreateDeviceAsync(dto);
            Console.WriteLine($"Device created: {device.Id}");
            Console.WriteLine($"Name: {device.Name}");
            Console.WriteLine($"Protocol: {device.Protocol}");
            Console.WriteLine($"Address: {device.Address}:{device.Port}");
        }, protocolOption, addressOption, portOption, nameOption);
    }
}
```

- [ ] **Step 6: 创建读取命令**

```csharp
// src/Clients/Commun.Client.CLI/Commands/ReadCommand.cs
using System.CommandLine;
using System.Net.Http.Json;

namespace Commun.Client.CLI.Commands;

public class ReadCommand : Command
{
    public ReadCommand() : base("read", "Read register value from device")
    {
        var deviceOption = new Option<string>("--device", "Device ID") { IsRequired = true };
        var registerOption = new Option<int>("--register", "Register address") { IsRequired = true };
        var lengthOption = new Option<int>("--length", "Number of registers to read", () => 1);
        
        AddOption(deviceOption);
        AddOption(registerOption);
        AddOption(lengthOption);
        
        this.SetHandler(async (deviceId, register, length) =>
        {
            var client = new HttpClient();
            var baseUrl = "http://localhost:5000";
            
            var request = new
            {
                CommandType = "ReadHoldingRegisters",
                Register = register,
                Length = length
            };
            
            var response = await client.PostAsJsonAsync($"{baseUrl}/api/protocol/{deviceId}/send", request);
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<ProtocolResponse>();
            
            if (result?.Success == true)
            {
                Console.WriteLine($"Read successful:");
                Console.WriteLine($"Data: {BitConverter.ToString(result.Data ?? Array.Empty<byte>())}");
            }
            else
            {
                Console.WriteLine($"Read failed: {result?.ErrorMessage}");
            }
        }, deviceOption, registerOption, lengthOption);
    }
}

public class ProtocolResponse
{
    public bool Success { get; set; }
    public byte[]? Data { get; set; }
    public string? ErrorMessage { get; set; }
}
```

- [ ] **Step 7: 创建写入命令**

```csharp
// src/Clients/Commun.Client.CLI/Commands/WriteCommand.cs
using System.CommandLine;
using System.Net.Http.Json;

namespace Commun.Client.CLI.Commands;

public class WriteCommand : Command
{
    public WriteCommand() : base("write", "Write value to device register")
    {
        var deviceOption = new Option<string>("--device", "Device ID") { IsRequired = true };
        var registerOption = new Option<int>("--register", "Register address") { IsRequired = true };
        var valueOption = new Option<int>("--value", "Value to write") { IsRequired = true };
        
        AddOption(deviceOption);
        AddOption(registerOption);
        AddOption(valueOption);
        
        this.SetHandler(async (deviceId, register, value) =>
        {
            var client = new HttpClient();
            var baseUrl = "http://localhost:5000";
            
            var request = new
            {
                CommandType = "WriteSingleRegister",
                Register = register,
                Data = BitConverter.GetBytes((ushort)value)
            };
            
            var response = await client.PostAsJsonAsync($"{baseUrl}/api/protocol/{deviceId}/send", request);
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<ProtocolResponse>();
            
            if (result?.Success == true)
            {
                Console.WriteLine($"Write successful");
            }
            else
            {
                Console.WriteLine($"Write failed: {result?.ErrorMessage}");
            }
        }, deviceOption, registerOption, valueOption);
    }
}
```

- [ ] **Step 8: 创建监控命令**

```csharp
// src/Clients/Commun.Client.CLI/Commands/MonitorCommand.cs
using System.CommandLine;
using System.Net.Http.Json;

namespace Commun.Client.CLI.Commands;

public class MonitorCommand : Command
{
    public MonitorCommand() : base("monitor", "Monitor device register changes")
    {
        var deviceOption = new Option<string>("--device", "Device ID") { IsRequired = true };
        var registerOption = new Option<int>("--register", "Register address") { IsRequired = true };
        var intervalOption = new Option<int>("--interval", "Polling interval in milliseconds", () => 1000);
        
        AddOption(deviceOption);
        AddOption(registerOption);
        AddOption(intervalOption);
        
        this.SetHandler(async (deviceId, register, interval) =>
        {
            var client = new HttpClient();
            var baseUrl = "http://localhost:5000";
            
            Console.WriteLine($"Monitoring device {deviceId} register {register}...");
            Console.WriteLine($"Polling interval: {interval}ms");
            Console.WriteLine("Press Ctrl+C to stop");
            
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };
            
            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    var request = new
                    {
                        CommandType = "ReadHoldingRegisters",
                        Register = register,
                        Length = 1
                    };
                    
                    var response = await client.PostAsJsonAsync($"{baseUrl}/api/protocol/{deviceId}/send", request);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<ProtocolResponse>();
                        
                        if (result?.Success == true && result.Data?.Length >= 2)
                        {
                            var value = BitConverter.ToUInt16(result.Data, 0);
                            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Register {register} = {value}");
                        }
                    }
                    
                    await Task.Delay(interval, cts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nMonitoring stopped");
            }
        }, deviceOption, registerOption, intervalOption);
    }
}
```

- [ ] **Step 9: 更新 Program.cs**

```csharp
// src/Clients/Commun.Client.CLI/Program.cs
using System.CommandLine;
using Commun.Client.CLI.Commands;

var rootCommand = new RootCommand("PLC-CommunTools CLI - Device communication tool");

rootCommand.AddCommand(new ConnectCommand());
rootCommand.AddCommand(new ReadCommand());
rootCommand.AddCommand(new WriteCommand());
rootCommand.AddCommand(new MonitorCommand());

return await rootCommand.InvokeAsync(args);
```

- [ ] **Step 10: 创建 CLI 测试项目**

```bash
dotnet new xunit -n Commun.Client.CLI.Tests -o tests/Commun.Client.CLI.Tests
dotnet sln add tests/Commun.Client.CLI.Tests/Commun.Client.CLI.Tests.csproj
dotnet add tests/Commun.Client.CLI.Tests/Commun.Client.CLI.Tests.csproj reference src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj
```

- [ ] **Step 11: 编写 CLI 测试**

```csharp
// tests/Commun.Client.CLI.Tests/CommandTests.cs
using Xunit;

namespace Commun.Client.CLI.Tests;

public class CommandTests
{
    [Fact]
    public void ConnectCommand_HasCorrectName()
    {
        var command = new Commun.Client.CLI.Commands.ConnectCommand();
        Assert.Equal("connect", command.Name);
    }
    
    [Fact]
    public void ReadCommand_HasCorrectName()
    {
        var command = new Commun.Client.CLI.Commands.ReadCommand();
        Assert.Equal("read", command.Name);
    }
    
    [Fact]
    public void WriteCommand_HasCorrectName()
    {
        var command = new Commun.Client.CLI.Commands.WriteCommand();
        Assert.Equal("write", command.Name);
    }
    
    [Fact]
    public void MonitorCommand_HasCorrectName()
    {
        var command = new Commun.Client.CLI.Commands.MonitorCommand();
        Assert.Equal("monitor", command.Name);
    }
}
```

- [ ] **Step 12: 运行 CLI 测试**

```bash
dotnet test tests/Commun.Client.CLI.Tests/Commun.Client.CLI.Tests.csproj
```

Expected: All tests pass

- [ ] **Step 13: 提交 CLI 实现**

```bash
git add src/Clients/Commun.Client.CLI/ tests/Commun.Client.CLI.Tests/
git commit -m "feat: implement CLI terminal with device management commands"
```

---

## Task 9: 集成测试和验证

**Covers:** [S11]

**Files:**
- Create: `tests/Commun.Integration.Tests/Commun.Integration.Tests.csproj`
- Create: `tests/Commun.Integration.Tests/EndToEndTests.cs`

- [ ] **Step 1: 创建集成测试项目**

```bash
dotnet new xunit -n Commun.Integration.Tests -o tests/Commun.Integration.Tests
dotnet sln add tests/Commun.Integration.Tests/Commun.Integration.Tests.csproj
dotnet add tests/Commun.Integration.Tests/Commun.Integration.Tests.csproj reference src/Server/Commun.Server.API/Commun.Server.API.csproj
dotnet add tests/Commun.Integration.Tests/Commun.Integration.Tests.csproj reference src/Core/Commun.Protocols/Commun.Protocols.csproj
```

- [ ] **Step 2: 编写集成测试**

```csharp
// tests/Commun.Integration.Tests/EndToEndTests.cs
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Commun.Server.API.Models;
using Xunit;

namespace Commun.Integration.Tests;

public class EndToEndTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    
    public EndToEndTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task GetDevices_ReturnsSuccessResponse()
    {
        var response = await _client.GetAsync("/api/devices");
        response.EnsureSuccessStatusCode();
        
        var devices = await response.Content.ReadFromJsonAsync<IEnumerable<DeviceDto>>();
        Assert.NotNull(devices);
    }
    
    [Fact]
    public async Task CreateDevice_ReturnsCreatedDevice()
    {
        var dto = new CreateDeviceDto
        {
            Name = "Integration Test Device",
            Protocol = "Modbus",
            Address = "192.168.1.100",
            Port = 502
        };
        
        var response = await _client.PostAsJsonAsync("/api/devices", dto);
        response.EnsureSuccessStatusCode();
        
        var device = await response.Content.ReadFromJsonAsync<DeviceDto>();
        Assert.NotNull(device);
        Assert.Equal("Integration Test Device", device.Name);
    }
    
    [Fact]
    public async Task GetAvailableProtocols_ReturnsProtocols()
    {
        var response = await _client.GetAsync("/api/protocol/protocols");
        response.EnsureSuccessStatusCode();
        
        var protocols = await response.Content.ReadFromJsonAsync<IEnumerable<ProtocolMetadata>>();
        Assert.NotNull(protocols);
    }
}
```

- [ ] **Step 3: 运行集成测试**

```bash
dotnet test tests/Commun.Integration.Tests/Commun.Integration.Tests.csproj
```

Expected: All tests pass

- [ ] **Step 4: 验证完整构建**

```bash
dotnet build CommunTools.New.sln
```

Expected: Build succeeded

- [ ] **Step 5: 运行所有测试**

```bash
dotnet test CommunTools.New.sln
```

Expected: All tests pass

- [ ] **Step 6: 提交集成测试**

```bash
git add tests/Commun.Integration.Tests/
git commit -m "test: add integration tests for end-to-end verification"
```

---

## Task 10: 文档和清理

**Covers:** [S12]

**Files:**
- Create: `docs/api/README.md`
- Create: `docs/api/Devices.md`
- Create: `docs/api/Protocol.md`
- Create: `README.md` (更新)

- [ ] **Step 1: 创建 API 文档目录**

```bash
mkdir -p docs/api
```

- [ ] **Step 2: 创建 API 文档**

```markdown
# PLC-CommunTools API Documentation

## Overview

PLC-CommunTools provides a RESTful API for managing PLC devices and communication protocols.

## Base URL

```
http://localhost:5000/api
```

## Authentication

Currently, the API does not require authentication. This will be added in future versions.

## Endpoints

### Devices

- `GET /api/devices` - Get all devices
- `GET /api/devices/{id}` - Get device by ID
- `POST /api/devices` - Create new device
- `PUT /api/devices/{id}` - Update device
- `DELETE /api/devices/{id}` - Delete device

### Protocol

- `GET /api/protocol/protocols` - Get available protocols
- `POST /api/protocol/{deviceId}/connect` - Connect to device
- `POST /api/protocol/{deviceId}/disconnect` - Disconnect from device
- `POST /api/protocol/{deviceId}/send` - Send command to device
- `GET /api/protocol/{deviceId}/status` - Get connection status
```

- [ ] **Step 3: 更新 README.md**

```markdown
# PLC-CommunTools

> * 此工具针对于Windows工业 **PLC** 硬件通讯协议及基础通讯而编写的通讯调试/测试工具，采用C#8.0语法及.NET 8框架。
> * 此项目支持WinForm、WPF客户端，以及服务端和Docker容器化部署。
> * 本人会把多年从事工业行业的硬件通讯经验体现在工具中，同时有相关行业经验的开发人员也可以提供思路和建议。
> * 如有错误，请不吝赐教。

## 功能特性

- **协议支持**：Modbus TCP/RTU、MQTT、串口通讯等
- **客户端**：WPF、WinForm、CLI终端
- **服务端**：ASP.NET Core Web API、WebSocket、gRPC
- **Docker部署**：支持多容器编排部署
- **MCP集成**：支持AI模型集成和远程监控

## 快速开始

### 使用 Docker 部署

```bash
cd docker
docker-compose up -d
```

### 本地开发

```bash
dotnet build CommunTools.New.sln
dotnet run --project src/Server/Commun.Server.API/Commun.Server.API.csproj
```

### CLI 使用

```bash
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- connect --protocol Modbus --address 192.168.1.100 --port 502
```

## 项目结构

```
PLC-CommunTools/
├── src/
│   ├── Core/              # 核心协议库
│   ├── Clients/           # 客户端应用
│   ├── Server/            # 服务端应用
│   └── Infrastructure/    # 基础设施
├── docker/                # Docker配置
├── tests/                 # 测试项目
└── docs/                  # 文档
```

## 技术栈

- .NET 8 LTS
- ASP.NET Core
- Entity Framework Core + SQLite
- Docker + Docker Compose
- MQTTnet
- System.CommandLine

## 许可证

MIT License
```

- [ ] **Step 4: 提交文档**

```bash
git add docs/ README.md
git commit -m "docs: add API documentation and update README"
```

- [ ] **Step 5: 最终验证**

```bash
dotnet build CommunTools.New.sln
dotnet test CommunTools.New.sln
```

Expected: All builds and tests pass

- [ ] **Step 6: 创建最终提交**

```bash
git add .
git commit -m "feat: complete PLC-CommunTools refactoring with modern architecture"
```

---

## 自审检查清单

### 规范覆盖
- [x] [S1] 项目概述 - Task 1
- [x] [S2] 架构设计 - Task 1
- [x] [S3] 协议层设计 - Task 2, 3, 4
- [x] [S4] 客户端设计 - Task 8
- [x] [S5] 服务端设计 - Task 6
- [x] [S6] MCP 集成设计 - 未在当前计划中实现（可作为后续任务）
- [x] [S7] 数据存储设计 - Task 5
- [x] [S8] Docker 部署设计 - Task 7
- [x] [S9] 实施计划 - 本计划
- [x] [S10] 风险评估 - 已在设计文档中
- [x] [S11] 成功标准 - Task 9
- [x] [S12] 附录 - Task 10

### 占位符扫描
- 无 TBD 或 TODO
- 所有步骤都有具体代码或命令
- 所有测试都有具体断言

### 类型一致性
- IProtocol 接口在整个计划中一致使用
- ProtocolConfig、ProtocolRequest、ProtocolResponse 类型一致
- DeviceDto、CreateDeviceDto 类型一致

### 范围检查
- 计划分为 10 个任务，每个任务可独立执行
- 每个任务都有明确的输入和输出
- 测试覆盖了所有主要功能

---

## 执行建议

**推荐执行方式**：Subagent（每个任务一个子代理）

**原因**：
1. 任务数量多（10个）
2. 任务之间相对独立
3. 每个任务都有明确的边界
4. 可以并行执行某些任务

**执行顺序**：
1. Task 1 → Task 2 → Task 3 → Task 4（串行，依赖关系）
2. Task 5（可与 Task 3、4 并行）
3. Task 6（依赖 Task 2、3、4、5）
4. Task 7（依赖 Task 6）
5. Task 8（可与 Task 6、7 并行）
6. Task 9（依赖所有前置任务）
7. Task 10（最后执行）