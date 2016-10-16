using NashNovoaltaisk03.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace NashNovoaltaisk03.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class PagesContent : Page
    {
        public MainPageViewModel MPVM = new MainPageViewModel();
        public PagesContent()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var con = 0;
            while (con < 25)
                try
                {
                    await MPVM.MainPageLoad();
                    con = 100;
                }
                catch (Exception ex)
                {
                    string exept = ex.ToString();
                    con++;
                }
            base.OnNavigatedTo(e);
        }
    }
}
