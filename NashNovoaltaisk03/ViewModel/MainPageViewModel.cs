using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NashNovoaltaisk03.Data;
using System.Collections.ObjectModel;
using NashNovoaltaisk03.Model;

namespace NashNovoaltaisk03.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<NewsData> PageContent { get; set; }
            = new ObservableCollection<NewsData>();
        PageModel Model = new PageModel();

        public async Task MainPageLoad()
        {
            var content = await Model.PageParser("http://novoaltaysk.online");
            //var content = await Model.GetNewsCategoryContent(3, "Город", false);
            foreach (var con in content)
                PageContent.Add(con);
        }
    }
}
