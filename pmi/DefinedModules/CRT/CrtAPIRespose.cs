using System.Text.Json.Serialization;

namespace pmi.DefinedModules.CRT;

public record CertificateEntry(
    [property: JsonPropertyName("issuer_ca_id")] int IssuerCaId,
    [property: JsonPropertyName("issuer_name")] string IssuerName,
    [property: JsonPropertyName("common_name")] string CommonName,
    [property: JsonPropertyName("name_value")] string NameValue,
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("entry_timestamp")] DateTime EntryTimestamp,
    [property: JsonPropertyName("not_before")] DateTime NotBefore,
    [property: JsonPropertyName("not_after")] DateTime NotAfter,
    [property: JsonPropertyName("serial_number")] string SerialNumber,
    [property: JsonPropertyName("result_count")] int ResultCount
);