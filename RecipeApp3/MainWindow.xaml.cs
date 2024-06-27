using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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

       

        private void LoadFoodGroupsAndCalories()
        {
            var foodGroups = recipeManager.GetAllFoodGroups();
            FoodGroupFilterComboBox.ItemsSource = foodGroups;

            var calories = recipeManager.GetAllCalories().Select(c => c.ToString()).ToList();
            CaloriesFilterComboBox.ItemsSource = calories;
        }

        private void DisplayFilteredRecipes(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("No recipes matching the filter criteria.", "Filtered Recipes", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string message = "Filtered Recipes:\n";
            foreach (var recipe in recipes)
            {
                message += $"{recipe.Name}\n";
            }
            MessageBox.Show(message, "Filtered Recipes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FilterByIngredient_Click(object sender, RoutedEventArgs e)
        {
            string ingredient = IngredientFilterTextBox.Text;
            var filteredRecipes = recipeManager.FilterRecipesByIngredient(ingredient);
            DisplayFilteredRecipes(filteredRecipes);
        }

        private void FilterByFoodGroup_Click(object sender, RoutedEventArgs e)
        {
            string foodGroup = FoodGroupFilterComboBox.SelectedItem as string;
            var filteredRecipes = recipeManager.FilterRecipesByFoodGroup(foodGroup);
            DisplayFilteredRecipes(filteredRecipes);
        }

        private void FilterByMaxCalories_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CaloriesFilterComboBox.SelectedItem as string, out int maxCalories))
            {
                MessageBox.Show("Invalid max calories value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var filteredRecipes = recipeManager.FilterRecipesByMaxCalories(maxCalories);
            DisplayFilteredRecipes(filteredRecipes);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
