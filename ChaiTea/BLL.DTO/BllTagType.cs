using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.DTO;

public class BllTagType : DomainEntityId
{
    public string Name { get; set; } = default!;

    public ICollection<BllTag>? Tags { get; set; }
}