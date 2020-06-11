namespace Recipe.Data.Recipes
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public int RecipeVariationId { get; set; }

        public virtual RecipeVariation RecipeVariation { get; set; }
    }
}