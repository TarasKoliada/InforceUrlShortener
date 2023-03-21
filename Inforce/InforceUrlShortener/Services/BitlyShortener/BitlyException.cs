using System;
using System.Net;

namespace InforceUrlShortener.Services.BitlyShortener
{
    public class BitlyException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public BitlyException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
