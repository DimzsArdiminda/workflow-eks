using System.Text.Json;
using System.Text.Json.Serialization;

namespace workflowEkstensi.server.TraceServer.Models
{
    public class TraceEvent
    {
        [JsonPropertyName("protocolVersion")]
        public string ProtocolVersion { get; set; } = "1.0";

        [JsonPropertyName("traceId")]
        public string TraceId { get; set; } = string.Empty;

        [JsonPropertyName("spanId")]
        public string SpanId { get; set; } = string.Empty;

        [JsonPropertyName("parentSpanId")]
        public string? ParentSpanId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("serviceName")]
        public string? ServiceName { get; set; }

        [JsonPropertyName("environment")]
        public string? Environment { get; set; }

        [JsonPropertyName("payload")]
        public Dictionary<string, object?>? Payload { get; set; }
    }

    public static class TraceEventTypes
    {
        public const string RequestStart = "request.start";
        public const string RequestEnd = "request.end";
        public const string FunctionEnter = "function.enter";
        public const string FunctionExit = "function.exit";
        public const string Exception = "exception";
        public const string Log = "log";
        public const string Sql = "sql";
        public const string Http = "http";

        public static readonly HashSet<string> All = new()
        {
            RequestStart,
            RequestEnd,
            FunctionEnter,
            FunctionExit,
            Exception,
            Log,
            Sql,
            Http
        };
    }
}