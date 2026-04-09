using System.Text.Json.Serialization;

namespace CreateTransportAssignment.Models;

public record Message(
    int Id,

    [property: JsonPropertyName("AuthorityCodeField")]
    string AuthorityCodeField,

    [property: JsonPropertyName("AssignmentIdField")]
    string AssignmentIdField,

    [property: JsonPropertyName("AssignmentReferenceIdField")]
    string AssignmentReferenceIdField,

    [property: JsonPropertyName("Package")]
    string Package,

    [property: JsonPropertyName("PickupLocationField")]
    string PickupLocationField,

    [property: JsonPropertyName("DeliveryLocationField")]
    string DeliveryLocationField,

    [property: JsonPropertyName("PriorityField")]
    string PriorityField,

    DateTime? SentAt,
    DateTime? ReceivedAt 
);