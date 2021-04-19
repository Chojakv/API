namespace Domain.Domain
{
    public class PayloadResult<T> : BaseRequestResult
    {
        public T Payload { get; set; }
    }
}