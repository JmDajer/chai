using System.Text.Json.Serialization;
using Domain.Base;

namespace BLL.DTO;

public class BllPicture : DomainEntityId
{
    public string Url { get; set; } = default!;

    public Guid BeverageId { get; set; }
    [JsonIgnore]
    public BllBeverage Beverage { get; set; } = default!;
}