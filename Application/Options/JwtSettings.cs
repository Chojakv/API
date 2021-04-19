using System;

namespace Application.Options
{
    public class JwtSettings
    {
        public string Key { get; set; }

        public TimeSpan TokenLifetime { get; set; }
    }
}