using SuperComprobadorPRO.Models;
using SuperComprobadorPRO.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperComprobadorPRO
{
    public class BotonListTemplateSelector : DataTemplateSelector
    {
        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            if (item != null)
            {
                var boton = item as BotonesMenu;

                if (boton != null)
                {
                    var currentFrame = Window.Current.Content as Frame;
                    var currentPage = currentFrame.Content as Page;
                                     
                    var newViewState = Windows.UI.ViewManagement.ApplicationView.Value;
                    if (newViewState == Windows.UI.ViewManagement.ApplicationViewState.Snapped)
                    {
                        return currentPage.Resources["BotonesSnappedItemTemplate"] as DataTemplate;
                    }
                    else
                    {
                        return currentPage.Resources["BotonesItemTemplate"] as DataTemplate;
                    }

                    //if (boton.Type.Equals(BotonType.Boton))
                    //    return currentPage.Resources["BotonesItemTemplate"] as DataTemplate;
                    //else
                    //    return currentPage.Resources["BotonesItemTemplate"] as DataTemplate;  //En este caso solo hay una
                }
            }

            return base.SelectTemplateCore(item, container);
        }
    }
}
