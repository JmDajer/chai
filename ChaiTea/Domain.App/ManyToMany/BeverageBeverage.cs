using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.App.ManyToMany
{
    public class BeverageBeverage
    {
        public Guid BeverageBeverageId { get; set; }

        [ForeignKey("SubBeverageId")]
        public Guid SubBeverageId { get; set; }
        public Beverage? SubBeverage { get; set; }

        [ForeignKey("ParentBeverageId")]
        public Guid ParentBeverageId { get; set; }
        public Beverage? ParentBeverage { get; set; }
    }
}