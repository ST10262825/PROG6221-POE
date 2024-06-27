using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeApp3
{
    
    // Represents a recipe with a name, ingredients, and preparation steps.
   public class Recipe
    {
        
        // Gets or sets the name of the recipe.
        public string Name { get; set; }

        
        // Dictionary storing the ingredients with their details such as quantity, unit, calories, and food group.
         public Dictionary<string, (double Quantity, string Unit, double Calories, string FoodGroup)> Ingredients { get; private set; } = new Dictionary<string, (double, string, double, string)>();

       
        // List of steps to prepare the recipe.
        public List<string> Steps { get; set; } = new List<string>();

        /// <summary>
        /// Dictionary to store the original quantities and calories of ingredients for reset purposes.
        /// </summary>
        private Dictionary<string, (double Quantity, double Calories)> originalIngredients = new Dictionary<string, (double, double)>();

        /// Delegate for the high-calorie alert event.
        public delegate void HighCalorieAlert(string recipeName, double totalCalories);

        
        // Event triggered when the total calories of a recipe exceed a certain threshold.
       public event HighCalorieAlert OnHighCalorie;

        // Constants for unit measurements
        public const string Teaspoon = "Teaspoon";
        public const string Tablespoon = "Tablespoon";
        public const string Cup = "cup";
       

        /// <summary>
        /// Gets the formatted text of ingredients with converted quantities.
        /// </summary>
        public string IngredientsText
        {
            get
            {
                return string.Join("\n", Ingredients.Select(i =>
                {
                    string convertedQuantity = ConvertQuantity(i.Value.Quantity, i.Value.Unit);
                    return $"{convertedQuantity} of {i.Key} ({i.Value.Calories} calories, Food Group: {i.Value.FoodGroup})";
                }));
            }
        }

        // Gets the total calories text.
        public string TotalCaloriesText
        {
            get
            {
                return $"Total Calories: {CalculateTotalCalories()}";
            }
        }

       
        // Gets the high calories message if the total calories exceed 300.
        public string HighCaloriesMessage
        {
            get
            {
                return CalculateTotalCalories() > 300 ? "(High Calories)" : string.Empty;
            }
        }

        /// <summary>
        /// Opens the recipe details window and triggers the high-calorie alert if applicable.
        /// </summary>
        public void OpenRecipeDetailsWindow()
        {
            double totalCalories = CalculateTotalCalories();
            if (totalCalories > 300)
            {
                OnHighCalorie?.Invoke(Name, totalCalories);
            }
            var detailsWindow = new RecipeDetailsWindow(this);
            detailsWindow.ShowDialog();
        }

       
        /// Calculates the total calories of the recipe.
        /// <returns>Total calories of the recipe.</returns>
        /// // Adapted from: [Stack Overflow], [C# calorie counter],
        // [https://stackoverflow.com/questions/58226276/c-sharp-calorie-counter]
        public double CalculateTotalCalories()
        {
            return Ingredients.Sum(i => i.Value.Calories);
        }


        // Adapted from: [Stack Overflow], [How do i display user input captured from a list in C#],
        // [https://stackoverflow.com/questions/76301825/how-do-i-display-user-input-captured-from-a-list-in-c-sharp]
        // Prompts the user to enter the details of the recipe including ingredients and steps.
        public void EnterRecipeDetails()
        {
            int ingredientCount = InputUtility.GetPositiveIntegerInput("\nEnter the number of ingredients: ");

            Dictionary<string, string> unitOptions = new Dictionary<string, string> // Dictionary to get unit options
            {
                { "Teaspoons", Teaspoon },
                { "Tablespoons", Tablespoon },
                { "cup", Cup },
                
            };

            List<string> foodGroupOptions = new List<string>
            {
                "Starchy foods",
                "Vegetables and fruits",
                "Dry beans, peas, lentils and soya",
                "Chicken, fish, meat and eggs",
                "Milk and dairy products",
                "Fats and oil",
                "Water"
            };

            for (int i = 0; i < ingredientCount; i++)
            {
                string ingredientName = InputUtility.GetNonEmptyStringInput($"\nEnter the name of ingredient {i + 1}: ");
                string unit = InputUtility.GetComboBoxInput($"\nSelect the unit of measurement for {ingredientName}:", unitOptions);
                double quantity = InputUtility.GetPositiveDoubleInput($"\nEnter the quantity of {ingredientName} (in positive numbers): ");
                double calories = InputUtility.GetPositiveDoubleInput($"\nEnter the number of calories for {quantity} {unit} of {ingredientName}: ");
                string foodGroup = InputUtility.GetComboBoxInput($"\nSelect the food group for {ingredientName}:", foodGroupOptions);

                Ingredients[ingredientName] = (quantity, unit, calories, foodGroup);
                originalIngredients[ingredientName] = (quantity, calories);
            }

            int stepCount = InputUtility.GetPositiveIntegerInput("\nEnter the number of steps: ");
            for (int i = 0; i < stepCount; i++)
            {
                string step = InputUtility.GetNonEmptyStringInput($"\nEnter step {i + 1}: ");
                Steps.Add(step);
            }

            MessageBox.Show("Recipe details entered successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        

        /// <summary>
        /// Scales the quantities of all ingredients in the recipe by a specified factor.
        /// </summary>
        public void ScaleRecipe()
        {
            double factor = InputUtility.GetPositiveDoubleInput("\nEnter the scaling factor (0.5 for half, 2 for double, 3 for triple): ");

            foreach (var ingredient in Ingredients.Keys.ToList())
            {
                var details = Ingredients[ingredient];
                Ingredients[ingredient] = (details.Quantity * factor, details.Unit, details.Calories * factor, details.FoodGroup);
            }

            MessageBox.Show("Recipe scaled successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        // Resets the quantities of ingredients to their original values.
        // Adapted from: [Stack Overflow], [How would I make a Yes/No prompt in Console using C#?],
        // [https://stackoverflow.com/questions/37359161/how-would-i-make-a-yes-no-prompt-in-console-using-c]
        public void ResetQuantities()
        {
            var result = MessageBox.Show($"Are you sure you want to reset all quantities for {Name}? (Yes/No)", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                foreach (var ingredient in originalIngredients)
                {
                    Ingredients[ingredient.Key] = (ingredient.Value.Quantity, Ingredients[ingredient.Key].Unit, ingredient.Value.Calories, Ingredients[ingredient.Key].FoodGroup);
                }
                MessageBox.Show("Quantities reset to original values.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (result == MessageBoxResult.No)
            {
                MessageBox.Show("Operation canceled.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter Yes or No.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Converts the quantity of an ingredient to a different unit if applicable.

        private string ConvertQuantity(double quantity, string unit)
        {
            if (unit == Tablespoon)
            {
                double cups = quantity / 8; // 1 cup = 8 tbsp
                if (cups >= 1)
                {
                    return $"{cups} cup{(cups > 1 ? "s" : "")}";
                }
            }
            else if (unit == Teaspoon)
            {
                double tbsp = quantity / 2; // 1 tbsp = 2 tsp
                if (tbsp >= 1)
                {
                    double cups = tbsp / 8; // 1 cup = 8 tbsp
                    if (cups >= 1)
                    {
                        return $"{cups} cup{(cups > 1 ? "s" : "")}";
                    }
                    return $"{tbsp} tablespoon{(tbsp > 1 ? "s" : "")}";
                }

            }
            else if (unit == Cup)
            {
                return $"{quantity} cup{(quantity > 1 ? "s" : "")}";
            }

            return $"{quantity} {unit}";
        }
        /// <summary>
        /// Represents an ingredient with its details such as quantity, unit, calories, and food group.
        /// </summary>
        public class Ingredient
        {
            // Gets or sets the quantity of the ingredient.
             public double Quantity { get; set; }
           // Gets or sets the unit of measurement for the quantity.
            public string Unit { get; set; }

            // Gets or sets the number of calories in the ingredient.
             public double Calories { get; set; }

            // Gets or sets the food group of the ingredient.
            public string FoodGroup { get; set; }
        }
    }
}
