using System;

namespace API.Interfaces
{
    public interface IUriService
    {
        Uri GetAdUri(string adId);

        Uri GetCategoryUri(string categoryId);
        
        Uri GetMessageUri(string messageId);
    }
}