using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GanhoDeCapital.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OperationType
{
    Buy,
    Sell
}
