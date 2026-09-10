using System.Text.Json;
using workflowEkstensi.server.TraceServer.Models;
using workflowEkstensi.server.TraceServer.Validation;
using Xunit;

public class TraceEventProtocolTests
{
    private readonly TraceEventValidator _validator = new();

    // 1. Deserialisasi payload valid
    [Fact]
    public void Test_Valid_Payload_Deserialization()
    {
        string json = """
        {
            "protocolVersion": "1.0",
            "traceId": "trace-123456789",
            "spanId": "span-abcdef01",
            "parentSpanId": null,
            "type": "request.start",
            "timestamp": 1789031358489,
            "serviceName": "laravel-agent",
            "environment": "local"
        }
        """;

        var evt = JsonSerializer.Deserialize<TraceEvent>(json);
        Assert.NotNull(evt);
        Assert.Equal("1.0", evt.ProtocolVersion);

        var result = _validator.Validate(evt);
        Assert.True(result.IsValid);
    }

    // 2. Tolak event tanpa traceId, spanId, type, atau timestamp
    [Theory]
    [InlineData("", "span-1", "request.start", 100)]
    [InlineData("trace-1", "", "request.start", 100)]
    [InlineData("trace-1", "span-1", "", 100)]
    [InlineData("trace-1", "span-1", "request.start", 0)]
    public void Test_Reject_Missing_Required_Fields(string traceId, string spanId, string type, long timestamp)
    {
        var evt = new TraceEvent
        {
            ProtocolVersion = "1.0",
            TraceId = traceId,
            SpanId = spanId,
            Type = type,
            Timestamp = timestamp
        };

        var result = _validator.Validate(evt);
        Assert.False(result.IsValid);
    }

    // 3. Tolak protocolVersion selain "1.0"
    [Fact]
    public void Test_Reject_Unsupported_Protocol_Version()
    {
        var evt = new TraceEvent
        {
            ProtocolVersion = "2.0",
            TraceId = "trace-1",
            SpanId = "span-1",
            Type = "request.start",
            Timestamp = 1000
        };

        var result = _validator.Validate(evt);
        Assert.False(result.IsValid);
    }

    // 4. Pastikan parentSpanId boleh null (Root Span)
    [Fact]
    public void Test_Allow_Null_ParentSpanId()
    {
        var evt = new TraceEvent
        {
            ProtocolVersion = "1.0",
            TraceId = "trace-1",
            SpanId = "span-1",
            ParentSpanId = null,
            Type = "request.start",
            Timestamp = 1000
        };

        var result = _validator.Validate(evt);
        Assert.True(result.IsValid);
    }
}