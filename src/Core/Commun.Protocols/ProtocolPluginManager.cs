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
                    .Where(t => typeof(IProtocolPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

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
