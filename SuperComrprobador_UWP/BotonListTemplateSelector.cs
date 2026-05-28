using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using SuperComrprobador_UWP.Models;

namespace SuperComrprobador_UWP
{
    public class BotonListTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is BotonesMenu)
            {
                // WinUI 3 desktop apps don't have ApplicationView orientation.
                // Always use the standard item template.
                var currentFrame = App.Current.Resources;
                if (container is FrameworkElement fe && fe.FindName("BotonesItemTemplate") is DataTemplate dt)
                    return dt;

                // Fallback: look in page resources
                if (container is FrameworkElement element)
                {
                    var page = element;
                    while (page != null && page is not Page)
                        page = VisualTreeHelper.GetParent(page) as FrameworkElement;

                    if (page is Page p && p.Resources["BotonesItemTemplate"] is DataTemplate pageTemplate)
                        return pageTemplate;
                }
            }

            return base.SelectTemplateCore(item, container);
        }
    }
}
