using System.Windows;

namespace RecipeApp3
{
    public partial class InputWindow : Window
    {
        public string Input { get; private set; }

        public InputWindow(string prompt)
        {
            InitializeComponent();
            PromptText.Text = prompt;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Input = InputBox.Text;
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
