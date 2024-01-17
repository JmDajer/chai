using System.Text.Json.Serialization;
using Domain.Base;

namespace DAL.DTO;

public class DalPicture : DomainEntityId
{
    public string Url { get; set; } = default!;

    public Guid BeverageId { get; set; }
    [JsonIgnore]
    public DalBeverage Beverage { get; set; } = default!;
}