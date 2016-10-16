using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NashNovoaltaisk03.Data;

namespace NashNovoaltaisk03.ViewModel
{
    public class ContentViewModel
    {
        public IEnumerable<ContentData> ContentDataList { get; set; }
    }
}
