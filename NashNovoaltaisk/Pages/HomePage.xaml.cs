using Windows.UI.Xaml.Controls;

namespace NashNovoaltaisk03.Pages
{
    public sealed partial class HomePage
    {
        public HomePage()
        {
            InitializeComponent();
            PageFramePivotViewContent.Navigate(typeof(PagesContent));
        }

        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PageFramePivotViewContent.Navigate(typeof(PagesContent), sender);
        }
    }
}