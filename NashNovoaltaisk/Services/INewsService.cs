using System.Collections.Generic;
using System.Threading.Tasks;
using NashNovoaltaisk.Models;

namespace NashNovoaltaisk.Services
{
    public interface INewsService
    {
        Task<IEnumerable<NewsItem>> GetNews(string url);
    }
}