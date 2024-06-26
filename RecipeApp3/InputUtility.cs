using System.Windows;

namespace RecipeApp3
{
    public static class InputUtility
    {
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
            }
            return result;
        }


    }
}
