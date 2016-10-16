using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using NashNovoaltaisk03.ViewModel;
using NashNovoaltaisk03.Model;

namespace NashNovoaltaisk03.Data
{
    public class NewsData
    {
        PageModel _model = new PageModel();
        Lazy<ContentViewModel> _contentPage;
        public ContentViewModel ContentPage
        {
            get
            {
                return _contentPage.Value;
            }
            set
            {
                _contentPage = new Lazy<ContentViewModel>(() =>
                    _model.GetPageContent(PageUri).GetAwaiter().GetResult());
            }
        }
        public int Id { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Header { get; set; }
        public string ImgSrc { private get; set; }
        public string PageUri { get; set; }
        public string Content { get; set; }
        private string _pubDate;
        public string PubDate
        {
            get
            {
                try
                {
                    return _pubDate.Split('T')
                              .Aggregate((i, j)
                              => new string(j.TakeWhile(c => c != '+').ToArray()) + "\t"
                              + string.Join(".", i.Split('-').Reverse()));
                }
                catch
                {
                    return "";
                }
            }
            set { _pubDate = value; }
        }

        public BitmapImage Image => new BitmapImage(new Uri(ImgSrc));
    }
}
