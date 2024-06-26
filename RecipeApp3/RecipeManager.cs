using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeApp3
{
    public class RecipeManager
    {
        public List<Recipe> recipes = new List<Recipe>();

        public void EnterNewRecipe()
        {
            while (true)
            {
                string recipeName = InputUtility.GetNonEmptyStringInput("\nEnter the name of the recipe: ");

                if (recipeName.Any(char.IsDigit))
                {
                    MessageBox.Show("Error: Recipe name cannot contain numbers. Please enter a valid name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    continue;
                }

                if (recipes.Any(r => r.Name == recipeName))
                {
                    MessageBox.Show("Error: Recipe with the same name already exists. Please enter a unique name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Recipe recipe = new Recipe();
                    recipe.Name = recipeName;
                    recipe.OnHighCalorie += Recipe_OnHighCalorie;
                    recipe.EnterRecipeDetails();
                    recipes.Add(recipe);
                    break;
                }
            }
        }

        public void Recipe_OnHighCalorie(string recipeName, double totalCalories)
        {
            MessageBox.Show($"Warning: The total calories of the recipe '{recipeName}' exceeds 300 calories. Total Calories: {totalCalories}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void DisplayAllRecipes()
        {
            DisplayRecipes(recipes);
        }

        public void DisplayRecipes(List<Recipe> recipeList)
        {
            if (recipeList.Count == 0)
            {
                MessageBox.Show("Error: No recipes available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<string> recipeNames = recipeList.Select(r => r.Name).OrderBy(n => n).ToList();
            var displayWindow = new DisplayWindow("List of Recipes", recipeNames);
            displayWindow.ShowDialog();
        }

        public void DisplayRecipeDetails()
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("Error: No recipes available. Please enter a new recipe first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Recipe selectedRecipe = SelectRecipe();
            if (selectedRecipe != null)
            {
                selectedRecipe.OpenRecipeDetailsWindow();
            }
        }

        public void ScaleRecipe()
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("Error: No recipes available. Please enter a new recipe first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Recipe selectedRecipe = SelectRecipe();
            if (selectedRecipe != null)
            {
                selectedRecipe.ScaleRecipe();
            }
        }

        public void ResetQuantities()
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("Error: No recipes available. Please enter a new recipe first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Recipe selectedRecipe = SelectRecipe();
            if (selectedRecipe != null)
            {
                selectedRecipe.ResetQuantities();
            }
        }

        public void ClearRecipeData()
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("Error: No recipes available to clear.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Recipe selectedRecipe = SelectRecipe();
            if (selectedRecipe != null)
            {
                MessageBoxResult confirmation = MessageBox.Show($"Are you sure you want to clear data for '{selectedRecipe.Name}'?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmation == MessageBoxResult.Yes)
                {
                    recipes.Remove(selectedRecipe);
                    MessageBox.Show($"Recipe '{selectedRecipe.Name}' data cleared successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public Recipe SelectRecipe()
        {
            var recipeNames = recipes.Select(r => r.Name).ToList();
            var selectWindow = new SelectWindow("Select a recipe:", recipeNames);
            if (selectWindow.ShowDialog() == true && selectWindow.SelectedIndex >= 0)
            {
                return recipes[selectWindow.SelectedIndex];
            }

            MessageBox.Show("Error: Invalid selection or no recipe selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

        public List<Recipe> FilterRecipes(string ingredient, string foodGroup, int maxCalories)
        {
            var filteredRecipes = recipes.AsEnumerable();

            if (!string.IsNullOrEmpty(ingredient))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.ContainsKey(ingredient));
            }

            if (!string.IsNullOrEmpty(foodGroup))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Values.Any(i => i.FoodGroup == foodGroup));
            }

            if (maxCalories != int.MaxValue)
            {
                filteredRecipes = filteredRecipes.Where(r => r.CalculateTotalCalories() <= maxCalories);
            }

            return filteredRecipes.ToList();
        }

        public List<string> GetAllFoodGroups()
        {
            return recipes.SelectMany(r => r.Ingredients.Values.Select(i => i.FoodGroup)).Distinct().ToList();
        }

        public List<int> GetAllCalories()
        {
            return recipes.Select(r => (int)r.CalculateTotalCalories()).Distinct().ToList();
        }
    }
}
