using System.Text.Json.Serialization;

namespace CreateTransportAssignment.Models;

public record Message(
    int Id,

    [property: JsonPropertyName("authorityCodeField")]
    string AuthorityCodeField,

    [property: JsonPropertyName("assignmentIdField")]
    string AssignmentIdField,

    [property: JsonPropertyName("assignmentReferenceIdField")]
    string AssignmentReferenceIdField,

    [property: JsonPropertyName("package")]
    string Package,

    [property: JsonPropertyName("pickupLocationField")]
    string PickupLocationField,

    [property: JsonPropertyName("deliveryLocationField")]
    string DeliveryLocationField,

    [property: JsonPropertyName("priorityField")]
    string PriorityField
);