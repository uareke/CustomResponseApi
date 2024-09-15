using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCache.Domain.DTO
{
    public class CustomResult
    {
        public HttpStatusCode? StatusCode { get; private set; }
        public bool? Success { get; private set; }
        public object? Data { get; private set; }
        public int? TotalRecords { get; private set; } = 0;
        public string? Version { get; private set; }
        public string? Language { get; private set; }
        public IEnumerable<string>? Errors { get; private set; }

        public CustomResult(HttpStatusCode statusCode, bool success, string version, string language)
        {
            StatusCode = statusCode;
            Success = success;
            Version = version;
            Language = language;
        }

        public CustomResult(HttpStatusCode statusCode, bool success, object data, int totalRecords, string version, string language) : this(statusCode, success, version, language)
        {
            Data = data;
            TotalRecords = totalRecords;
            Version = version;
            Language = language;
        }

        public CustomResult(HttpStatusCode statusCode, bool success, IEnumerable<string> errors, string version, string language) : this(statusCode, success, version, language) =>
            Errors = errors;

        public CustomResult(HttpStatusCode statusCode, bool success, object data, IEnumerable<string> errors, string version, string language) : this(statusCode, success, version, language)
        {
            Data = data;
            Errors = errors;
        }
    }
}
