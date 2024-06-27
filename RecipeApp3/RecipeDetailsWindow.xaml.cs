using System.Windows;

namespace RecipeApp3
{
    public partial class RecipeDetailsWindow : Window
    {
        public RecipeDetailsWindow(Recipe recipe)
        {
            InitializeComponent();
            DataContext = recipe;

            // Check if total calories are high and set the high calories message
            if (recipe.CalculateTotalCalories() > 300)
            {
                HighCaloriesMessage.Text = "(High Calories)";
            }
        }
    }
}
