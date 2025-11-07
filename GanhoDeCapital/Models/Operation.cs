using System.Text.Json.Serialization;

namespace GanhoDeCapital.Models;
public class Operation
{
    [JsonPropertyName("operation")]
    public OperationType OperationType {  get; set; }

    [JsonPropertyName("unit-cost")]
    public decimal UnitCost { get; set; }

    [JsonPropertyName("quantity")]
    public long Quantity  { get; set; }

    public decimal TotalOperationValue => UnitCost * Quantity;
}
