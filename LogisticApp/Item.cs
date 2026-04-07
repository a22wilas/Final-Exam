namespace LogisticApp.Models;
public class Item
{
    public int Id { get; set; }
    public string? AuthorityCodeField { get; set; }
    public string? AssignmentIdField { get; set; }
    public string? AssignmentReferenceIdField { get; set; }
    public string? Package { get; set; }
    public string? PickupLocationField { get; set; }
    public string? DeliveryLocationField { get; set; }
    public string? PriorityField { get; set; }
}