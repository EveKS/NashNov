using System;
using Windows.UI.Xaml.Media.Imaging;

namespace NashNovoaltaisk03.Models
{
    public class NewsData
    {
       /* private readonly PageModel _model = new PageModel();
        * private Lazy<ContentViewModel> _contentPage;
        * public ContentViewModel ContentPage { get { return _contentPage.Value; } set { _contentPage = new Lazy<ContentViewModel>(() => _model.GetPageContent(PageUri).GetAwaiter().GetResult()); } } */

        public int Id { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Header { get; set; }
        public Uri ImgSrc { private get; set; }
        public string PageUri { get; set; }
        public string Content { get; set; }
        public DateTime PubDate { get; set; }
        public BitmapImage Image => new BitmapImage(ImgSrc);
    }
}