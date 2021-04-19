using System;
using Application.Interfaces;
using Contracts.Contracts.V1;

namespace Infrastructure.Services
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
        
        public Uri GetMessageUri(string messageId)
        {
            return new Uri(_baseUri + ApiRoutes.Categories.Get.Replace("{messageId}", messageId));
        }

    }
}