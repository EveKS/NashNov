using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using NashNovoaltaisk03.Models;

namespace NashNovoaltaisk03.Utilities
{
    public static class Parser
    {
        private const string AnimationStackSelector = "div[class='td_module_10 td_module_wrap td-animation-stack']";
        private const string ItemDetailsSelector = AnimationStackSelector + " " + "div[class='item-details']";
        private const string ModuleThumbSelector = AnimationStackSelector + " " + "div[class='td-module-thumb']";
        private static async Task<string> GetUrlAsync(string url)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url).ConfigureAwait(false);
            }
        }

        public static async Task<IEnumerable<NewsData>> ParsePageAsync(string pageUri)
        {
            var content = await GetUrlAsync(pageUri);
            var parser = new HtmlParser();
            var document = parser.Parse(content);
            var info = document.QuerySelectorAll(ItemDetailsSelector);
            var category = document.QuerySelectorAll(ModuleThumbSelector);
            var meta = document.QuerySelectorAll(AnimationStackSelector);
            return info.Select((element, index) => new NewsData
            {
                Id = index,
                Category = element.QuerySelector("div[class='td-module-meta-info'] a").TextContent,
                Content = element.QuerySelector("div[class='td-excerpt']").TextContent.Trim(),
                PubDate = DateTime.Parse(element.QuerySelector("time").GetAttribute("datetime"), null, DateTimeStyles.RoundtripKind),
                Author = meta.Select(met => met.QuerySelector("meta[itemprop='author']").GetAttribute("content")).ElementAt(index),
                Header = category.Select(cat => cat.QuerySelector("a").GetAttribute("title")).ElementAt(index),
                ImgSrc = new Uri(category.Select(cat => cat.QuerySelector("img").GetAttribute("src")).ElementAt(index)),
                PageUri = category.Select(cat => cat.QuerySelector("a").GetAttribute("href")).ElementAt(index)
            });
        }
        
        #region Old stuff

        /*
        /// <summary>
        ///     Настройки запроса к http://novoaltaysk.online/
        /// </summary>
        /// <param name="page">Сраница, к которой идет запрос, null указание на http://novoaltaysk.online/ </param>
        /// <param name="key">Категория, к которой идет запрос</param>
        /// <param name="isMain">Главные новости или нет</param>
        /// <returns></returns>
                private const string BigGridWrapper = "div[class='td-big-grid-wrapper']";
        private const string MainContentWrap = "div[class='td-main-content-wrap']";
        private const string InfoSelect = "div[class='td-module-thumb']";
        private const string CategorySelect = "a[class='td-post-category']";
        private const string MetaInfoContainer = "div[class='td-meta-info-container']";
        private const string MainPage = @"http://novoaltaysk.online/";

            
        private readonly Dictionary<string, string> _category = new Dictionary<string, string>
        {
            ["Город"] = "gorod", ["В крае"] = "v-krae", ["В мире"] = "v-mire", ["Политика"] = "politika", ["Культура"] = "kultura", ["Общество"] = "obshhestvo", ["Образование"] = "obrazovanie", ["Спорт"] = "sport", ["Криминал"] = "kriminal",
            ["Происшествия"] = "proisshestviya", ["Строительство"] = "stroitelstvo", ["Новости городов и районов"] = "novosti-gorodov-i-rajonov", ["Новости районов"] = "novosti-gorodov-i-rajonov/novosti-rajonov",
            ["На заметку"] = "na-zametku", ["Туризм"] = "turizm", ["В стране и мире"] = "v-strane-i-mire"
        };

            public async Task<IEnumerable<NewsData>> GetNewsCategoryContent(int? page, string key, bool isMain)
        {
            var wrapper = isMain ? BigGridWrapper : MainContentWrap;
            var pageUri = page.HasValue ? $"{MainPage}/category/{_category[key]}/page/{page}/" : MainPage;
            return await PageNewsParser(pageUri, wrapper + InfoSelect, wrapper + CategorySelect, wrapper + MetaInfoContainer);
        }

        private static Task<IEnumerable<NewsData>> PageNewsParser(string pageUri, string infoSelect, string categorySelect, string metaSelect)
        {
            return Enumerable.Range(0, 1).Select(url => GetHttp(pageUri)).Select(async content =>
            {
                var parser = new HtmlParser();
                var document = parser.Parse(await content.ConfigureAwait(false));
                var info = document.QuerySelectorAll(infoSelect);
                var category = document.QuerySelectorAll(categorySelect);
                var meta = document.QuerySelectorAll(metaSelect);
                return info.Select((element, index) => new NewsData
                {
                    Id = index, Header = element.QuerySelector("a").GetAttribute("title"), ImgSrc = element.QuerySelector("img").GetAttribute("src"), PageUri = element.QuerySelector("a").GetAttribute("href"),
                    Category = category.Select(cat => cat.TextContent).ElementAt(index)
                    //Author = meta.Select(met => met.QuerySelector("meta[itemprop='author']")
                    //                .GetAttribute("content")).ElementAt(index),
                    //PubDate = meta.Select(met => met.QuerySelector("meta[itemprop='datePublished']")
                    //                .GetAttribute("content")).ElementAt(index),
                });
            }).First();
        }  
                public async Task<ContentViewModel> GetPageContent(string pageUri)
        {
            var content = GetHttp(pageUri);
            var parser = new HtmlParser();
            var document = parser.Parse(await content.ConfigureAwait(false));
            var data = document.QuerySelectorAll("div[class='td-post-date'] time");
            var pageContent = document.QuerySelectorAll("div[class='td-post-content']");
            return new ContentViewModel
            {
                ContentDataList = pageContent.Select(pcon => pcon.InnerHtml.Split(new[]
                {
                    '\n'
                }, StringSplitOptions.RemoveEmptyEntries).TakeWhile(con => !con.Contains("<!--")).Select(con =>
                {
                    var document2 = parser.Parse(con).All.First();
                    return new ContentData
                    {
                        Content = document2.TextContent, ImgSrc = document2.InnerHtml.Contains("img") && document2.InnerHtml.Contains("src") ? document2.QuerySelector("img").GetAttribute("src") : null
                    };
                }).Where(d => !string.IsNullOrWhiteSpace(d.Content) || (d.ImgSrc != null))).First()
            };
        }*/

        #endregion
    }
}