using System.Collections.Generic;

namespace Contracts.Contracts.Validation
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new();
    }
}