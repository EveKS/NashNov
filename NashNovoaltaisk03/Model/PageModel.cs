using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NashNovoaltaisk03.Data;
using NashNovoaltaisk03.ViewModel;

namespace NashNovoaltaisk03.Model
{
    public class PageModel
    {
        string big_grid_wrapper = "div[class='td-big-grid-wrapper']";
        string main_content_wrap = "div[class='td-main-content-wrap']";
        string infoSelect = "div[class='td-module-thumb']";
        string categorySelect = "a[class='td-post-category']";
        string meta_info_container = "div[class='td-meta-info-container']";

        string _mainPage = @"http://novoaltaysk.online/";

        /// <summary>
        /// Настройки запроса к http://novoaltaysk.online/
        /// </summary>
        /// <param name="page">Сраница, к которой идет запрос, null указание на http://novoaltaysk.online/ </param>
        /// <param name="key">Категория, к которой идет запрос</param>
        /// <param name="isMain">Главные новости или нет</param>
        /// <returns></returns>
        public async Task<IEnumerable<NewsData>> GetNewsCategoryContent(int? page, string key, bool isMain)
            => await PageNewsParser
                (
                    page == null ? _mainPage : $"{_mainPage}/category/{_category[key]}/page/{page}/",
                    $"{(!isMain ? main_content_wrap : big_grid_wrapper)} {infoSelect}",
                    $"{(!isMain ? main_content_wrap : big_grid_wrapper)} {categorySelect}",
                    $"{(!isMain ? main_content_wrap : big_grid_wrapper)} {meta_info_container}"
                );

        public IEnumerable<string> PageCategory => _category.Select(cat => cat.Key);

        Dictionary<string, string> _category = new Dictionary<string, string>
        {
            ["Город"] =                     @"gorod",
            ["В крае"] =                    @"v-krae",
            ["В мире"] =                    @"v-mire",
            ["Политика"] =                  @"politika",
            ["Культура"] =                  @"kultura",
            ["Общество"] =                  @"obshhestvo",
            ["Образование"] =               @"obrazovanie",
            ["Спорт"] =                     @"sport",
            ["Криминал"] =                  @"kriminal",
            ["Происшествия"] =              @"proisshestviya",
            ["Строительство"] =             @"stroitelstvo",
            ["Новости городов и районов"] = @"novosti-gorodov-i-rajonov",
            ["Новости районов"] =           @"novosti-gorodov-i-rajonov/novosti-rajonov",
            ["На заметку"] =                @"na-zametku",
            ["Туризм"] =                    @"turizm",
            ["В стране и мире"] =           @"v-strane-i-mire",
        };

        private async Task<string> GetHttp(string requestUri)
        {
            using (var client = new HttpClient())
                return await client.GetStringAsync(requestUri).ConfigureAwait(false);
        }

        private Task<IEnumerable<NewsData>> PageNewsParser
            (string pageUri, string infoSelect, string categorySelect, string metaSelect)
        =>
        Enumerable.Range(0, 1)
                .Select(url =>
                    GetHttp(pageUri))

                .Select(async content =>
                {
                    var parser = new HtmlParser();
                    var document = parser.Parse(await content.ConfigureAwait(false));

                    var info = document.QuerySelectorAll(infoSelect);
                    var category = document.QuerySelectorAll(categorySelect);
                    var meta = document.QuerySelectorAll(metaSelect);
                    return
                        info.Select((element, index) =>
                            new NewsData
                            {
                                Id = index,
                                Header = element.QuerySelector("a")
                                                .GetAttribute("title"),
                                ImgSrc = element.QuerySelector("img")
                                                .GetAttribute("src"),
                                PageUri = element.QuerySelector("a")
                                                .GetAttribute("href"),
                                Category = category.Select(cat => cat.TextContent)
                                                .ElementAt(index),
                                //Author = meta.Select(met => met.QuerySelector("meta[itemprop='author']")
                                //                .GetAttribute("content")).ElementAt(index),
                                //PubDate = meta.Select(met => met.QuerySelector("meta[itemprop='datePublished']")
                                //                .GetAttribute("content")).ElementAt(index),
                            })
                        ;
                }).First();

        public Task<IEnumerable<NewsData>> PageParser
            (string pageUri)
            =>
            Enumerable.Range(0, 1)
                .Select(url =>
                    GetHttp(pageUri))

                .Select(async content =>
                {
                    var parser = new HtmlParser();
                    var document = parser.Parse(await content.ConfigureAwait(false));
                    var infoSelect = "div[class='td_module_10 td_module_wrap td-animation-stack'] div[class='item-details']";
                    var categorySelect = "div[class='td_module_10 td_module_wrap td-animation-stack'] div[class='td-module-thumb']";
                    var metaSelect = "div[class='td_module_10 td_module_wrap td-animation-stack']";

                    var info = document.QuerySelectorAll(infoSelect);
                    var category = document.QuerySelectorAll(categorySelect);
                    var meta = document.QuerySelectorAll(metaSelect);
                    return
                        info.Select((element, index) =>
                        new NewsData
                        {
                            Id = index,
                            Category = element.QuerySelector("div[class='td-module-meta-info'] a")
                                .TextContent,
                            Content = element.QuerySelector("div[class='td-excerpt']")
                                .TextContent.Trim(),
                            PubDate = element.QuerySelector("time")
                                .GetAttribute("datetime"),
                            Author = meta.Select(met => met.QuerySelector("meta[itemprop='author']")
                                 .GetAttribute("content")).ElementAt(index),
                            Header = category.Select(cat => cat.QuerySelector("a")
                                .GetAttribute("title")).ElementAt(index),
                            ImgSrc = category.Select(cat => cat.QuerySelector("img")
                                .GetAttribute("src")).ElementAt(index),
                            PageUri = category.Select(cat => cat.QuerySelector("a")
                                .GetAttribute("href")).ElementAt(index),
                        })
                        ;
                }).First();

        public Task<ContentViewModel> GetPageContent(string pageUri)
        {
            return
                Enumerable.Range(0, 1)
                .Select(url =>
                    GetHttp(pageUri))

                    .Select(async content =>
                    {
                        var parser = new HtmlParser();
                        var document = parser.Parse(await content.ConfigureAwait(false));

                        var data = document.QuerySelectorAll
                        ("div[class='td-post-date'] time");
                        var pageContent = document.QuerySelectorAll
                        ("div[class='td-post-content']");
                        return
                        new ContentViewModel
                        {
                            ContentDataList = pageContent.Select(pcon =>
                            pcon.InnerHtml.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).TakeWhile(con => !con.Contains("<!--"))
                            .Select(con =>
                            {
                                var document2 = parser.Parse(con).All.First();
                                return
                                new ContentData
                                {
                                    Content = document2.TextContent,
                                    ImgSrc = document2.InnerHtml.Contains("img")
                                    && document2.InnerHtml.Contains("src")
                                    ? document2.QuerySelector("img")
                                    .GetAttribute("src") : null
                                };
                            }).Where(d => !string.IsNullOrWhiteSpace(d.Content) || d.ImgSrc != null)).First()
                        };
                    }).First();
        }
    }
}
