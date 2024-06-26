using System.Collections.Generic;
using System.Windows;

namespace RecipeApp3
{
    public partial class SelectWindow : Window
    {
        public int SelectedIndex { get; private set; }

        public SelectWindow(string prompt, List<string> items)
        {
            InitializeComponent();
            PromptText.Text = prompt;
            ItemsListBox.ItemsSource = items;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedIndex = ItemsListBox.SelectedIndex;
            DialogResult = true;
            Close(); // Ensure the window is closed
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close(); // Ensure the window is closed
        }
    }
}
