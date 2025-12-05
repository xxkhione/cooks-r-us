namespace CooksRUs.Models
{
    public class Recipe
    {
        public string RecipeName { get; set; }
        public string Image { get; set; }
        public string Directions { get; set; }
        public List<Ingredient> IngredientList { get; set; }
    }

    public class Ingredient
    {
        public string IngredientName { get; set; }
        public string Measurement { get; set; }
    }
}
