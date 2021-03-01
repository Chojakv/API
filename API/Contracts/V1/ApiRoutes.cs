namespace API.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public class Categories
        {
            public const string GetAll = Base + "/categories";

            public const string Get = Base + "/categories/{categoryId}";
            
            public const string Create = Base + "/categories";
            
            public const string Delete = Base + "/categories/{categoryId}";
        }
    }
}