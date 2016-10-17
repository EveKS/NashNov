using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using NashNovoaltaisk.Models;

namespace NashNovoaltaisk.Services
{
    public class NewsService : INewsService
    {
        private const string AnimationStackSelector = "div[class='td_module_10 td_module_wrap td-animation-stack']";
        private const string ItemDetailsSelector = AnimationStackSelector + " " + "div[class='item-details']";
        private const string ModuleThumbSelector = AnimationStackSelector + " " + "div[class='td-module-thumb']";

        public async Task<IEnumerable<NewsItem>> GetNews(string url)
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync(url);
            var parser = new HtmlParser();
            var document = parser.Parse(result);
            var info = document.QuerySelectorAll(ItemDetailsSelector);
            var category = document.QuerySelectorAll(ModuleThumbSelector);
            var meta = document.QuerySelectorAll(AnimationStackSelector);
            return info.Select((element, index) => new NewsItem
            {
                Id = index, Category = element.QuerySelector("div[class='td-module-meta-info'] a").TextContent, Content = element.QuerySelector("div[class='td-excerpt']").TextContent.Trim(),
                PublishDate = DateTime.Parse(element.QuerySelector("time").GetAttribute("datetime"), null, DateTimeStyles.RoundtripKind),
                Author = meta.Select(met => met.QuerySelector("meta[itemprop='author']").GetAttribute("content")).ElementAt(index), Title = category.Select(cat => cat.QuerySelector("a").GetAttribute("title")).ElementAt(index),
                ImgSrc = new Uri(category.Select(cat => cat.QuerySelector("img").GetAttribute("src")).ElementAt(index)), Link = category.Select(cat => cat.QuerySelector("a").GetAttribute("href")).ElementAt(index)
            });
        }
    }
}