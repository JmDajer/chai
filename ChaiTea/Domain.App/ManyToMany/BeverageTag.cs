using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.App.ManyToMany
{
    public class BeverageTag
    {
        public Guid BeverageTagId { get; set; }

        [ForeignKey("BeverageId")]
        public Guid BeverageId { get; set; }
        public Beverage? Beverage { get; set; }

        [ForeignKey("TagId")]
        public Guid TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}