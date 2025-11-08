using System.Text.Json.Serialization;

namespace GanhoDeCapital.Models;
public class Tax
{
    [JsonPropertyName("tax")]
    public decimal TaxAmout { get; set; }

    public Tax(decimal taxAmount)
    {
        TaxAmout = taxAmount;
    }
}
