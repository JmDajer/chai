using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class Picture : DomainEntityId
{
    public string Url { get; set; } = default!;
    
    public Guid BeverageId { get; set; }
    [JsonIgnore]
    public Beverage Beverage { get; set; } = default!;
}