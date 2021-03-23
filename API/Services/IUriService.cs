using System;

namespace API.Services
{
    public interface IUriService
    {
        Uri GetAdUri(string adId);

        Uri GetCategoryUri(string categoryId);
    }
}