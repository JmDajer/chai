﻿using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class TagType : DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    public ICollection<Tag>? Tags { get; set; }
}