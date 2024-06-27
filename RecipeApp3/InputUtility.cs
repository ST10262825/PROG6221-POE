using System.Collections.Generic;
using System.Windows;

namespace RecipeApp3
{
    /// <summary>
    /// Utility class providing methods to get validated user input.
    /// </summary>
    public static class InputUtility
    {
        /// <summary>
        /// Prompts the user to enter a positive integer.
        /// </summary>
        /// <param name="prompt">The message to display in the prompt.</param>
        /// <returns>A positive integer entered by the user.</returns>
        public static int GetPositiveIntegerInput(string prompt)
        {
            int result;
            while (true)
            {
                var inputWindow = new InputWindow(prompt);
                if (inputWindow.ShowDialog() == true && int.TryParse(inputWindow.Input, out result) && result > 0)
                {
                    break;
                }
                else
                {
                    MessageBox.Show("Error: Invalid input. Please enter a positive integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return result;
        }

        /// <summary>
        /// Prompts the user to enter a positive double.
        /// </summary>
        /// <param name="prompt">The message to display in the prompt.</param>
        /// <returns>A positive double entered by the user.</returns>
        public static double GetPositiveDoubleInput(string prompt)
        {
            double result;
            while (true)
            {
                var inputWindow = new InputWindow(prompt);
                if (inputWindow.ShowDialog() == true && double.TryParse(inputWindow.Input, out result) && result > 0)
                {
                    break;
                }
                else
                {
                    MessageBox.Show("Error: Invalid input. Please enter a positive number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return result;
        }

        /// <summary>
        /// Prompts the user to enter a non-empty string.
        /// </summary>
        /// <param name="prompt">The message to display in the prompt.</param>
        /// <returns>A non-empty string entered by the user.</returns>
        public static string GetNonEmptyStringInput(string prompt)
        {
            string result;
            while (true)
            {
                var inputWindow = new InputWindow(prompt);
                if (inputWindow.ShowDialog() == true)
                {
                    result = inputWindow.Input;

                    if (!string.IsNullOrEmpty(result))
                    {
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Error: Input cannot be empty. Please enter a valid string.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Operation canceled.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return null;
                }
            }
            return result;
        }

        /// <summary>
        /// Prompts the user to select an option from a list of strings.
        /// </summary>
        /// <param name="prompt">The message to display in the prompt.</param>
        /// <param name="items">The list of options to select from.</param>
        /// <returns>The selected option as a string.</returns>
        public static string GetComboBoxInput(string prompt, List<string> items)
        {
            string result;
            while (true)
            {
                var inputWindow = new InputWindow(prompt, items);
                if (inputWindow.ShowDialog() == true)
                {
                    result = inputWindow.Input;

                    if (!string.IsNullOrEmpty(result))
                    {
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Error: Please select a valid option.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Operation canceled.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return null;
                }
            }
            return result;
        }

        /// <summary>
        /// Prompts the user to select an option from a dictionary of key-value pairs.
        /// </summary>
        /// <param name="prompt">The message to display in the prompt.</param>
        /// <param name="itemsDict">The dictionary of options to select from, where keys are displayed to the user and values are returned.</param>
        /// <returns>The value corresponding to the selected key from the dictionary.</returns>
        public static string GetComboBoxInput(string prompt, Dictionary<string, string> itemsDict)
        {
            string result;
            while (true)
            {
                var inputWindow = new InputWindow(prompt, itemsDict);
                if (inputWindow.ShowDialog() == true)
                {
                    result = inputWindow.Input;

                    if (!string.IsNullOrEmpty(result) && itemsDict.ContainsKey(result))
                    {
                        result = itemsDict[result]; // Return the value corresponding to the selected key
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Error: Please select a valid option.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Operation canceled.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return null;
                }
            }
            return result;
        }
    }
}
