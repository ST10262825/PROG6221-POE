using System.Collections.Generic;
using System.Windows;

namespace RecipeApp3
{
    public partial class ComboBoxWindow : Window
    {
        public string Title { get; set; }
        public List<string> Items { get; set; }
        public string SelectedItem { get; set; }

        public ComboBoxWindow(string title, List<string> items)
        {
            InitializeComponent();
            DataContext = this;

            Title = title;
            Items = items;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
