using Domain.Base;

namespace DAL.DTO;

public class DalTagType : DomainEntityId
{
    public string Name { get; set; } = default!;

    public ICollection<DalTag>? Tags { get; set; }
}