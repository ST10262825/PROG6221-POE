using System.Windows;

namespace RecipeApp3
{
    public partial class RecipeDetailsWindow : Window
    {
        public RecipeDetailsWindow(Recipe recipe)
        {
            InitializeComponent();

            // Set data context
            DataContext = new
            {
                RecipeName = $"****{recipe.Name.ToUpper()} RECIPE****",
                Ingredients = GetIngredientsText(recipe),
                Steps = GetStepsText(recipe),
                TotalCalories = $"Total Calories: {recipe.CalculateTotalCalories()}"
            };
        }

        private string GetIngredientsText(Recipe recipe)
        {
            string ingredientsText = "INGREDIENTS:\n";
            foreach (var ingredient in recipe.Ingredients)
            {
                var details = ingredient.Value;
                ingredientsText += $"{recipe.ConvertQuantity(details.Quantity, details.Unit)} of {ingredient.Key} ({details.Calories} calories, Food Group: {details.FoodGroup})\n";
            }
            return ingredientsText;
        }

        private string GetStepsText(Recipe recipe)
        {
            string stepsText = "STEPS:\n";
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                stepsText += $"{i + 1}. {recipe.Steps[i]}\n";
            }
            return stepsText;
        }
    }
}
