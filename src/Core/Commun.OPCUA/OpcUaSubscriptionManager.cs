using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Opc.Ua;
using Opc.Ua.Client;

namespace Commun.OPCUA;

public class OpcUaSubscriptionManager : IDisposable
{
    private readonly Session _session;
    private readonly ConcurrentDictionary<string, Subscription> _subscriptions = new();
    private readonly ConcurrentDictionary<string, MonitoredItem> _monitoredItems = new();
    private bool _disposed;

    public event EventHandler<NodeValueChangedEventArgs>? NodeValueChanged;

    public OpcUaSubscriptionManager(Session session)
    {
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public bool Subscribe(string nodeIdString, int publishingInterval = 1000, uint queueSize = 1)
    {
        if (_disposed) return false;

        try
        {
            var nodeId = new NodeId(nodeIdString);
            var subscriptionKey = $"sub_{nodeIdString}";

            var subscription = _subscriptions.GetOrAdd(subscriptionKey, _ =>
            {
                var sub = new Subscription(_session.DefaultSubscription)
                {
                    PublishingEnabled = true,
                    PublishingInterval = publishingInterval,
                    LifetimeCount = 1000,
                    KeepAliveCount = 10,
                    MaxNotificationsPerPublish = 1000,
                    Priority = 0
                };
                _session.AddSubscription(sub);
                sub.Create();
                return sub;
            });

            var monitoredItem = new MonitoredItem(subscription.DefaultItem)
            {
                DisplayName = $"Monitor_{nodeIdString}",
                StartNodeId = nodeId,
                AttributeId = Attributes.Value,
                SamplingInterval = publishingInterval,
                QueueSize = queueSize,
                DiscardOldest = true,
                MonitoringMode = MonitoringMode.Reporting
            };

            monitoredItem.Notification += OnMonitoredItemNotification;
            subscription.AddItem(monitoredItem);
            subscription.ApplyChanges();

            _monitoredItems[nodeIdString] = monitoredItem;
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Subscribe failed for {nodeIdString}: {ex.Message}");
            return false;
        }
    }

    public bool Unsubscribe(string nodeIdString)
    {
        if (_disposed) return false;

        try
        {
            if (!_monitoredItems.TryRemove(nodeIdString, out var monitoredItem))
                return false;

            var subscriptionKey = $"sub_{nodeIdString}";
            if (_subscriptions.TryRemove(subscriptionKey, out var subscription))
            {
                subscription.RemoveItem(monitoredItem);
                monitoredItem.Notification -= OnMonitoredItemNotification;

                if (subscription.MonitoredItemCount == 0)
                {
                    _session.RemoveSubscription(subscription);
                    subscription.Dispose();
                }
                else
                {
                    subscription.ApplyChanges();
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Unsubscribe failed for {nodeIdString}: {ex.Message}");
            return false;
        }
    }

    public bool IsSubscribed(string nodeIdString)
    {
        return _monitoredItems.ContainsKey(nodeIdString);
    }

    public IReadOnlyCollection<string> GetActiveSubscriptions()
    {
        return _monitoredItems.Keys.ToList().AsReadOnly();
    }

    public int SubscriptionCount => _monitoredItems.Count;

    private void OnMonitoredItemNotification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
    {
        try
        {
            if (e.NotificationValue is DataValue dataValue)
            {
                var nodeIdString = monitoredItem.StartNodeId.ToString();
                NodeValueChanged?.Invoke(this, new NodeValueChangedEventArgs
                {
                    NodeId = nodeIdString,
                    Value = dataValue.Value,
                    StatusCode = dataValue.StatusCode,
                    SourceTimestamp = dataValue.SourceTimestamp,
                    ServerTimestamp = dataValue.ServerTimestamp
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Notification handler error: {ex.Message}");
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        foreach (var item in _monitoredItems.Values)
        {
            try { item.Notification -= OnMonitoredItemNotification; } catch { }
        }

        foreach (var sub in _subscriptions.Values)
        {
            try
            {
                _session.RemoveSubscription(sub);
                sub.Dispose();
            }
            catch { }
        }

        _monitoredItems.Clear();
        _subscriptions.Clear();
    }
}

public class NodeValueChangedEventArgs : EventArgs
{
    public string NodeId { get; set; } = string.Empty;
    public object? Value { get; set; }
    public StatusCode StatusCode { get; set; }
    public DateTime SourceTimestamp { get; set; }
    public DateTime ServerTimestamp { get; set; }
}
