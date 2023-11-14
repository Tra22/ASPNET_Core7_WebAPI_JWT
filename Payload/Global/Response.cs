
using System.Diagnostics;

namespace ASPNET_Core7_WebAPI_JWT.Payload.Global {
    public class Response<T>
    {
        public string? TraceId {get;set;} = Activity.Current?.Id;
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; } = null;
        public string? Error { get; set; } = null;
        public List<string>? ErrorMessages { get; set; } = null;
    }
}