namespace API.Domain
{
    public class PayloadResult<T> : BaseRequestResult
    {
        public T Payload { get; set; }
    }
}