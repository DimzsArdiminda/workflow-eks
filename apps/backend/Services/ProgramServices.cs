using System;
using System.Collections.Generic;
using workflowEkstensi.server.TraceServer.Models;

namespace workflowEkstensi.backend.Services
{
    public class ProgramServices
    {
        public object protocol()
        {
            var samplePayload = new TraceEvent
            {
                ProtocolVersion = "1.0",
                TraceId = "trace-123456789",
                SpanId = "span-abcdef01",
                ParentSpanId = null,
                Type = TraceEventTypes.RequestStart,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                ServiceName = "laravel-agent",
                Environment = "local",
                Payload = new Dictionary<string, object?>
                {
                    ["method"] = "GET",
                    ["url"] = "/api/users",
                    ["ip"] = "127.0.0.1"
                }
            };

            return new
            {
                protocolVersion = "1.0",
                supportedEvents = TraceEventTypes.All,
                samplePayload = samplePayload
            };
        }
    }
}