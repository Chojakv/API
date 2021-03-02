namespace API.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        
        public static class Ads
        {
            public const string GetAll = Base + "/ads";
            
            public const string Get = Base + "/ads/{adId}";
            
            public const string GetByCategory = Base + "/ads/{categoryId}/ads";

            public const string Create = Base + "/ads";
            
            public const string Update = Base + "/ads/{adId}";
            
            public const string Delete = Base + "/ads/{adId}";
        }
        
        public static class Categories
        {
            public const string GetAll = Base + "/categories";

            public const string Get = Base + "/categories/{categoryId}";
            
            public const string Create = Base + "/categories";
            
            public const string Delete = Base + "/categories/{categoryId}";
            
        }
    }
}