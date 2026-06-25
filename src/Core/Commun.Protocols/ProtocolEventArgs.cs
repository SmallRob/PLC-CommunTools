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
