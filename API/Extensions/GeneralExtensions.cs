using System.Linq;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext.User != null ? httpContext.User.Claims.Single(x => x.Type == "Id").Value : string.Empty;
        }
        
        public static string GetUsername(this HttpContext httpContext)
        {
            return httpContext.User != null ? httpContext.User.Claims.First(x => x.Type == "Username").Value : string.Empty;
        }
        

    }
}