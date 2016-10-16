using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using NashNovoaltaisk03.Pages;

namespace NashNovoaltaisk03
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            PageFrameSplitViewContent.Navigate(typeof(HomePage));
            TitleTextBlock.Text = "Home";
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HomeListBoxItem.IsSelected)
            {
                PageFrameSplitViewContent.Navigate(typeof(HomePage));
                TitleTextBlock.Text = "Home";
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen ^= true;
        }
    }
}