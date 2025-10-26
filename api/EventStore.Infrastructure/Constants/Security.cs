namespace EventStore.Infrastructure.Constants
{
    public static class Security
    {
        public static class Claims
        {
            public const string Identifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            public const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        }

        public static class Azure
        {
            public const string TenantId = "Azure:TenantId";
            public const string ClientId = "Azure:ClientId";
            public const string ClientSecret = "Azure:ClientSecret";
        }
    }
}