using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeApp3
{
    /// <summary>
    /// Manages a collection of recipes, including adding new recipes,
    /// displaying recipe details, scaling recipes, and filtering recipes.
    /// </summary>
    public class RecipeManager
    {
        // List to store all recipes
        public List<Recipe> recipes = new List<Recipe>();

        /// <summary>
        /// Prompts the user to enter a new recipe and adds it to the collection.
        /// Ensures the recipe name is unique and valid.
        /// </summary>
        public void EnterNewRecipe()
        {
            while (true)
            {
                var inputWindow = new InputWindow("\nEnter the name of the recipe: ");
                if (inputWindow.ShowDialog() == true)
                {
                    string recipeName = inputWindow.Input;

                    if (recipeName == null || string.IsNullOrWhiteSpace(recipeName))
                    {
                        MessageBox.Show("Error: Recipe name cannot be empty or null. Please enter a valid name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        continue;
                    }

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
                else
                {
                    // Handle Cancel case
                    MessageBox.Show("Operation canceled.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                }
            }
        }

        /// <summary>
        /// Handles the event when a recipe's total calories exceed 300.
        /// Displays a warning message.
        /// </summary>
        /// <param name="recipeName">The name of the recipe.</param>
        /// <param name="totalCalories">The total calories of the recipe.</param>
        public void Recipe_OnHighCalorie(string recipeName, double totalCalories)
        {
            MessageBox.Show($"Warning: The total calories of the recipe '{recipeName}' exceeds 300 calories. Total Calories: {totalCalories}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Displays the details of a selected recipe.
        /// </summary>
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

        /// <summary>
        /// Scales the quantities of a selected recipe.
        /// </summary>
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

        /// <summary>
        /// Resets the quantities of ingredients in a selected recipe to their original values.
        /// </summary>
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

        /// <summary>
        /// Clears the data of a selected recipe from the collection.
        /// </summary>
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

        /// <summary>
        /// Prompts the user to select a recipe from the list and returns the selected recipe.
        /// </summary>
        /// <returns>The selected Recipe object, or null if no selection is made.</returns>
        public Recipe SelectRecipe()
        {
            // Create a list of recipe names and order them alphabetically
            var recipeNames = recipes.Select(r => r.Name).OrderBy(n => n).ToList();

            // Show the select window with the ordered recipe names
            var selectWindow = new SelectWindow("Select a recipe:(Click on a Recipe's name to select it)", recipeNames);

            // Check if a valid selection was made
            if (selectWindow.ShowDialog() == true && selectWindow.SelectedIndex >= 0)
            {
                // Get the selected recipe name based on the selected index
                string selectedRecipeName = recipeNames[selectWindow.SelectedIndex];

                // Find the recipe in the original list by name
                Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name == selectedRecipeName);

                if (selectedRecipe != null)
                {
                    return selectedRecipe;
                }
            }

            // Show an error message if the selection was invalid
            MessageBox.Show("Error: Invalid selection or no recipe selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

        /// <summary>
        /// Filters recipes based on the provided ingredient, food group, and maximum calories.
        /// </summary>
        /// <param name="ingredient">The ingredient to filter by.</param>
        /// <param name="foodGroup">The food group to filter by.</param>
        /// <param name="maxCalories">The maximum calories to filter by.</param>
        /// <returns>A list of filtered recipes.</returns>
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

        /// <summary>
        /// Filters recipes by a specific ingredient.
        /// </summary>
        /// <param name="ingredient">The ingredient to filter by.</param>
        /// <returns>A list of filtered recipes.</returns>
        public List<Recipe> FilterRecipesByIngredient(string ingredient)
        {
            var filteredRecipes = recipes.Where(r => r.Ingredients.ContainsKey(ingredient)).ToList();
            return filteredRecipes;
        }

        /// <summary>
        /// Filters recipes by a specific food group.
        /// </summary>
        /// <param name="foodGroup">The food group to filter by.</param>
        /// <returns>A list of filtered recipes.</returns>
        public List<Recipe> FilterRecipesByFoodGroup(string foodGroup)
        {
            var filteredRecipes = recipes.Where(r => r.Ingredients.Values.Any(i => i.FoodGroup == foodGroup)).ToList();
            return filteredRecipes;
        }

        /// <summary>
        /// Filters recipes by a maximum calorie limit.
        /// </summary>
        /// <param name="maxCalories">The maximum calories to filter by.</param>
        /// <returns>A list of filtered recipes.</returns>
        public List<Recipe> FilterRecipesByMaxCalories(int maxCalories)
        {
            var filteredRecipes = recipes.Where(r => r.CalculateTotalCalories() <= maxCalories).ToList();
            return filteredRecipes;
        }

        /// <summary>
        /// Gets a list of all unique food groups from the recipes.
        /// </summary>
        /// <returns>A list of food groups.</returns>
        public List<string> GetAllFoodGroups()
        {
            return recipes.SelectMany(r => r.Ingredients.Values.Select(i => i.FoodGroup)).Distinct().ToList();
        }

        /// <summary>
        /// Gets a list of all unique calorie values from the recipes.
        /// </summary>
        /// <returns>A list of calorie values.</returns>
        public List<int> GetAllCalories()
        {
            return recipes.Select(r => (int)r.CalculateTotalCalories()).Distinct().ToList();
        }
    }
}
