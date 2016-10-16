using System;
using Windows.UI.Xaml.Navigation;
using NashNovoaltaisk03.ViewModel;

namespace NashNovoaltaisk03.Pages
{
    public sealed partial class PagesContent
    {
        public readonly MainPageViewModel Mpvm = new MainPageViewModel();

        public PagesContent()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var con = 0;
            while (con < 25)
                try
                {
                    await Mpvm.MainPageLoad();
                    con = 100;
                }
                catch (Exception)
                {
                    con++;
                }
        }
    }
}