using System;

namespace InforceUrlShortener.Services
{
    public static class IdentifierProvider
    {
        public static string GetGuid() => Guid.NewGuid().ToString();

    }
}
