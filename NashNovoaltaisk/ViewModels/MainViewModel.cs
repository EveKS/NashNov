using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using NashNovoaltaisk.Models;
using NashNovoaltaisk.Services;

namespace NashNovoaltaisk.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly INewsService _rssService;

        private ObservableCollection<NewsItem> _news;

        public MainViewModel(INewsService rssService)
        {
            _rssService = rssService;
        }

        public ObservableCollection<NewsItem> News
        {
            get { return _news; }
            set
            {
                _news = value;
                NotifyOfPropertyChange();
            }
        }

        protected override async void OnActivate()
        {
            await RefreshData();
        }

        public async Task RefreshData()
        {
            var items = await _rssService.GetNews("http://novoaltaysk.online");
            News = new ObservableCollection<NewsItem>(items);
        }
    }
}