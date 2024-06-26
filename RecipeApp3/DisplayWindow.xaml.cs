using System.Collections.Generic;
using System.Windows;

namespace RecipeApp3
{
    public partial class DisplayWindow : Window
    {
        public DisplayWindow(string title, List<string> items)
        {
            InitializeComponent();
            Title = title;
            DisplayText.Text = string.Join("\n", items);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
