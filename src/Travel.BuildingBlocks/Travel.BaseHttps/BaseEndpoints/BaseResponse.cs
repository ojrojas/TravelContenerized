namespace Travel.BaseHttps.BaseEndpoints;

public record BaseResponse : BaseMessage
{
    public BaseResponse(Guid correlationId) : base()
    {
        _correlationId = correlationId;
    }
}