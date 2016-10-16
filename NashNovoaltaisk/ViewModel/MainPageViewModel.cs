using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NashNovoaltaisk03.Models;
using NashNovoaltaisk03.Utilities;

namespace NashNovoaltaisk03.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<NewsData> PageContent { get; } = new ObservableCollection<NewsData>();

        public async Task MainPageLoad()
        {
            var data = await Parser.ParsePageAsync("http://novoaltaysk.online");
            foreach (var item in data)
                PageContent.Add(item);
        }
    }
}