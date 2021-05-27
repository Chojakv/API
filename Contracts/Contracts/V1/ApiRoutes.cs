namespace Contracts.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
        
            public const string Register = Base + "/identity/register";

            public const string Logout = Base + "/logout";
        }

        public static class Message
        {
            public const string Create = Base + "/messages";
            
            public const string Get = Base + "/messages/{messageId}";
        }
        
        public static class Users
        {
            public const string Get = Base + "/users/{username}";

            public const string Update = Base + "/users/{username}";

            public const string UploadAvatar = Base + "/users/{username}/avatar";

            public static class Ads
            {
                public const string GetAll = Base + "/users/{username}/ads";
            }
            public static class Messages
            {
                public const string GetAllSent = Base + "/users/{username}/messagesSent";
        
                public const string GetAllReceived = Base + "/users/{username}/messagesReceived";
                
                public const string NewMessagesCount = Base + "/users/{username}/newMessages";
            }
        }
        
        public static class Ads
        {
            public const string GetAll = Base + "/ads";
            
            public const string Get = Base + "/ads/{adId}";

            public const string UploadPhoto = Base + "/ads/{adId}/photos";
            
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