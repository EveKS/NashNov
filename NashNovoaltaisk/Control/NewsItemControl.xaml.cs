using NashNovoaltaisk03.Models;

namespace NashNovoaltaisk03.Control
{
    public sealed partial class NewsItemControl
    {
        public NewsItemControl()
        {
            InitializeComponent();
            DataContextChanged += (s, e) => Bindings.Update();
        }

        public NewsData NewsData { get { return DataContext as NewsData; } }
    }
}