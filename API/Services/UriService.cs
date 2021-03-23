using System;
using API.Contracts.V1;

namespace API.Services
{
    public class UriService : IUriService
    {

        private readonly string _baseUri;
    

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAdUri(string adId)
        {
            return new Uri(_baseUri + ApiRoutes.Ads.Get.Replace("{adId}", adId));
        }

        public Uri GetCategoryUri(string categoryId)
        {
            return new Uri(_baseUri + ApiRoutes.Categories.Get.Replace("{categoryId}", categoryId));
        }
    }
}