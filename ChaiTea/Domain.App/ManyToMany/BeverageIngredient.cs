using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.App.ManyToMany
{
    public class BeverageIngredient
    {
        public Guid BeverageIngredientId { get; set; }

        [ForeignKey("BeverageId")]
        public Guid BeverageId { get; set; }
        public Beverage? Beverage { get; set; }

        [ForeignKey("IngredientId")]
        public Guid IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}