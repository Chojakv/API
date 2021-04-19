using System.Collections.Generic;

namespace Domain.Domain
{
    public class BaseRequestResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}