using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp3
{
    public partial class MainWindow : Window
    {
        public RecipeManager recipeManager = new RecipeManager();

        public MainWindow()
        {
            InitializeComponent();
            LoadFoodGroupsAndCalories();

        }

        private void EnterNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            recipeManager.EnterNewRecipe();
            LoadFoodGroupsAndCalories();
        }

        private void DisplayAllRecipes_Click(object sender, RoutedEventArgs e)
        {
            recipeManager.DisplayAllRecipes();
        }

        

        private void DisplayRecipeDetails_Click(object sender, RoutedEventArgs e)
        {
           
                recipeManager.DisplayRecipeDetails();
            
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            recipeManager.ScaleRecipe();
        }

        private void ResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            recipeManager.ResetQuantities();
        }

        private void ClearRecipeData_Click(object sender, RoutedEventArgs e)
        {
            recipeManager.ClearRecipeData();
            LoadFoodGroupsAndCalories();
        }

        private void ApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            string ingredient = IngredientFilterTextBox.Text;
            string foodGroup = FoodGroupFilterComboBox.SelectedItem as string;
            if (!int.TryParse(CaloriesFilterComboBox.SelectedItem as string, out int maxCalories))
            {
                maxCalories = int.MaxValue;
            }

            var filteredRecipes = recipeManager.FilterRecipes(ingredient, foodGroup, maxCalories);
            DisplayFilteredRecipes(filteredRecipes);
        }

        private void LoadFoodGroupsAndCalories()
        {
            var foodGroups = recipeManager.GetAllFoodGroups();
            FoodGroupFilterComboBox.ItemsSource = foodGroups;

            var calories = recipeManager.GetAllCalories().Select(c => c.ToString()).ToList();
            CaloriesFilterComboBox.ItemsSource = calories;
        }

        private void DisplayFilteredRecipes(List<Recipe> recipes)
        {
            string message = "Filtered Recipes:\n";
            foreach (var recipe in recipes)
            {
                message += $"{recipe.Name}\n";
            }
            MessageBox.Show(message, "Filtered Recipes", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
