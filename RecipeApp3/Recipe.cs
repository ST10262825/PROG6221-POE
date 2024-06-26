using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeApp3
{
    public class Recipe
    {
        public string Name { get; set; }
        public Dictionary<string, (double Quantity, string Unit, double Calories, string FoodGroup)> Ingredients { get; private set; } = new Dictionary<string, (double, string, double, string)>();
        public List<string> Steps { get; set; } = new List<string>();

        private Dictionary<string, (double Quantity, double Calories)> originalIngredients = new Dictionary<string, (double, double)>();

        public delegate void HighCalorieAlert(string recipeName, double totalCalories);
        public event HighCalorieAlert OnHighCalorie;



        public void EnterRecipeDetails()
        {
            int ingredientCount = InputUtility.GetPositiveIntegerInput("\nEnter the number of ingredients: ");

            Dictionary<string, string> unitOptions = new Dictionary<string, string>
            {
                { "tsp", "teaspoons" },
                { "tbsp", "tablespoons" },
                { "cup", "cups" }
            };

            for (int i = 0; i < ingredientCount; i++)
            {
                string ingredientName = InputUtility.GetNonEmptyStringInput($"\nEnter the name of ingredient {i + 1}: ");

                string unit = string.Empty;
                while (true)
                {
                  
                    var inputWindow = new InputWindow($"\nEnter the unit of measurement for {ingredientName} (tsp, tbsp, cup): ");
                    if (inputWindow.ShowDialog() == true)
                    {
                        string userInput = inputWindow.Input.ToLower();

                        if (unitOptions.ContainsKey(userInput))
                        {
                            unit = unitOptions[userInput];
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Error: Invalid unit. Please choose from tsp, tbsp, or cup.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }

                double quantity = InputUtility.GetPositiveDoubleInput($"\nEnter the quantity of {ingredientName} (in positive numbers): ");
                double calories = InputUtility.GetPositiveDoubleInput($"\nEnter the number of calories for {quantity} {unit} of {ingredientName}: ");
                string foodGroup = InputUtility.GetNonEmptyStringInput($"\nEnter the food group for {ingredientName}: ");

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

        public void DisplayRecipe()
        {
            string message = $"****{Name.ToUpper()} RECIPE****\n\nINGREDIENTS:\n";
            foreach (var ingredient in Ingredients)
            {
                var details = ingredient.Value;
                message += $"{ConvertQuantity(details.Quantity, details.Unit)} of {ingredient.Key} ({details.Calories} calories, Food Group: {details.FoodGroup})\n";
            }

            message += "\nSTEPS:\n";
            for (int i = 0; i < Steps.Count; i++)
            {
                message += $"{i + 1}. {Steps[i]}\n";
            }

            double totalCalories = CalculateTotalCalories();
            message += $"\nTotal Calories: {totalCalories}";

            if (totalCalories > 300)
            {
                OnHighCalorie?.Invoke(Name, totalCalories);
            }

            MessageBox.Show(message, "Recipe Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void OpenRecipeDetailsWindow()
        {
            double totalCalories = CalculateTotalCalories();
            if (totalCalories > 300)
            {
                OnHighCalorie?.Invoke(Name, totalCalories);
            }
            RecipeDetailsWindow detailsWindow = new RecipeDetailsWindow(this);
            detailsWindow.Show();
        }

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

        public double CalculateTotalCalories()
        {
            return Ingredients.Sum(ingredient => ingredient.Value.Calories);
        }

        public string ConvertQuantity(double quantity, string unit)
        {
            if (unit == "tablespoons")
            {
                double cups = quantity / 8; // 1 cup = 8 tbsp
                if (cups >= 1)
                {
                    return $"{cups} cup{(cups > 1 ? "s" : "")}";
                }
            }
            else if (unit == "teaspoons")
            {
                double tbsp = quantity / 2; // 1 tbsp = 2 tsp
                if (tbsp >= 1)
                {
                    double cups = tbsp / 8; // 1 cup = 8 tbsp
                    return $"{tbsp} tbsp{(tbsp > 1 ? "s" : "")}";
                }
            }
            return $"{quantity} {unit}";
        }
    }
}
