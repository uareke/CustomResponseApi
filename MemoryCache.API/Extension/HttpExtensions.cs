using System.Net;

namespace MemoryCache.API.Extension
{
    public static class HttpExtensions
    {
        public static bool IsSuccess(this HttpStatusCode statusCode) =>
             new HttpResponseMessage(statusCode).IsSuccessStatusCode;
    }
}
