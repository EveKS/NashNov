using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using NashNovoaltaisk03.Data;

namespace NashNovoaltaisk03.Control
{
    public class VariableSizeGridView : GridView
    {
        //private int rowVal;
        //private int colVal;

        protected override void PrepareContainerForItemOverride
            (DependencyObject element, object item)
        {
            //NewsData dataItem = item as NewsData;

            //VariableSizedWrapGrid.SetRowSpan(element as UIElement, rowVal);
            //VariableSizedWrapGrid.SetColumnSpan(element as UIElement, colVal);

            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
