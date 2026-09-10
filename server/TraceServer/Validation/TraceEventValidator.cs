using workflowEkstensi.server.TraceServer.Models;

namespace workflowEkstensi.server.TraceServer.Validation
{
    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; } = new();

        public void AddError(string error) => Errors.Add(error);
    }

    public class TraceEventValidator
    {
        public const string SupportedProtocolVersion = "1.0";

        public ValidationResult Validate(TraceEvent? traceEvent)
        {
            var result = new ValidationResult();

            if (traceEvent == null)
            {
                result.AddError("Event payload cannot be null.");
                return result;
            }

            // 1. Validasi Protocol Version
            if (string.IsNullOrWhiteSpace(traceEvent.ProtocolVersion) || traceEvent.ProtocolVersion != SupportedProtocolVersion)
            {
                result.AddError($"Unsupported or missing protocolVersion '{traceEvent.ProtocolVersion}'. Expected '{SupportedProtocolVersion}'.");
            }

            // 2. Validasi TraceId
            if (string.IsNullOrWhiteSpace(traceEvent.TraceId))
            {
                result.AddError("Field 'traceId' is required and cannot be empty.");
            }

            // 3. Validasi SpanId
            if (string.IsNullOrWhiteSpace(traceEvent.SpanId))
            {
                result.AddError("Field 'spanId' is required and cannot be empty.");
            }

            // 4. Validasi Type
            if (string.IsNullOrWhiteSpace(traceEvent.Type))
            {
                result.AddError("Field 'type' is required and cannot be empty.");
            }
            else if (!TraceEventTypes.All.Contains(traceEvent.Type))
            {
                result.AddError($"Invalid event type '{traceEvent.Type}'.");
            }

            // 5. Validasi Timestamp
            if (traceEvent.Timestamp <= 0)
            {
                result.AddError("Field 'timestamp' must be a valid positive number.");
            }

            // Catatan: parentSpanId boleh null (untuk root span), jadi tidak perlu divalidasi tidak boleh null.

            return result;
        }
    }
}