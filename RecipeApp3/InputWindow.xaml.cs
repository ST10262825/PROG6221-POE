using System.Printing;
using System.Windows;

namespace RecipeApp3
{
    public partial class InputWindow : Window
    {
        public string Input { get; private set; }
        private bool isComboBoxInput;

        public InputWindow(string prompt, List<string> comboBoxItems = null)
        {
            InitializeComponent();
            PromptText.Text = prompt;

            if (comboBoxItems != null)
            {
                ComboBoxInput.ItemsSource = comboBoxItems;
                ComboBoxInput.Visibility = Visibility.Visible;
                isComboBoxInput = true;
            }
            else
            {
                InputBox.Visibility = Visibility.Visible;
                isComboBoxInput = false;
            }
        }

        public InputWindow(string prompt, Dictionary<string, string> comboBoxItemsDict)
        {
            InitializeComponent();
            PromptText.Text = prompt;

            if (comboBoxItemsDict != null)
            {
                ComboBoxInput.ItemsSource = comboBoxItemsDict.Keys.ToList();
                ComboBoxInput.Visibility = Visibility.Visible;
                isComboBoxInput = true;
            }
            else
            {
                InputBox.Visibility = Visibility.Visible;
                isComboBoxInput = false;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (isComboBoxInput)
            {
                Input = ComboBoxInput.SelectedItem?.ToString();
            }
            else
            {
                Input = InputBox.Text;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}