namespace Travel.BaseHttps.BaseEndpoints;

public abstract record BaseMessage
{
    protected Guid _correlationId = Guid.NewGuid();
    public Guid CorrelationId() => _correlationId;
}

