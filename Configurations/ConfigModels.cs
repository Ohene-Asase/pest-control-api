using System;
using PestControl.Services;

namespace PestControl.Configurations
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

    public class RecaptchaConfig
    {
        public string EndPoint { get; set; }
        public string Secret { get; set; }
    }
}
