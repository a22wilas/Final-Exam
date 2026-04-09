using System.Text.Json.Serialization;
namespace LogisticApp.Models;

public class Item
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("AuthorityCodeField")]
    public string? AuthorityCodeField { get; set; }

    [JsonPropertyName("AssignmentIdField")]
    public string? AssignmentIdField { get; set; }

    [JsonPropertyName("AssignmentReferenceIdField")]
    public string? AssignmentReferenceIdField { get; set; }

    [JsonPropertyName("Package")]
    public string? Package { get; set; }

    [JsonPropertyName("PickupLocationField")]
    public string? PickupLocationField { get; set; }

    [JsonPropertyName("DeliveryLocationField")]
    public string? DeliveryLocationField { get; set; }

    [JsonPropertyName("PriorityField")]
    public string? PriorityField { get; set; }

    [JsonPropertyName("SentAt")]
    public DateTime? SentAt { get; set; }

    [JsonPropertyName("ReceivedAt")]
    public DateTime? ReceivedAt { get; set; }
}