using System;

namespace NashNovoaltaisk.Models
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public Uri ImgSrc { get; set; }
        public string Link { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
    }
}