using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SuperComprobadorPRO.ViewModels;
using Windows.UI.ViewManagement;

// La plantilla de elemento Página básica está documentada en http://go.microsoft.com/fwlink/?LinkId=234237

namespace SuperComprobadorPRO
{
    /// <summary>
    /// Página básica que proporciona características comunes a la mayoría de las aplicaciones.
    /// </summary>
    public sealed partial class MainPage : SuperComprobadorPRO.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
           
            DataContext = new MenuInicioViewModel();
        }

        /// <summary>
        /// Rellena la página con el contenido pasado durante la navegación. Cualquier estado guardado se
        /// proporciona también al crear de nuevo una página a partir de una sesión anterior.
        /// </summary>
        /// <param name="navigationParameter">Valor de parámetro pasado a
        /// <see cref="Frame.Navigate(Type, Object)"/> cuando se solicitó inicialmente esta página.
        /// </param>
        /// <param name="pageState">Diccionario del estado mantenido por esta página durante una sesión
        /// anterior. Será null la primera vez que se visite una página.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Mantiene el estado asociado con esta página en caso de que se suspenda la aplicación o
        /// se descarte la página de la memoria caché de navegación. Los valores deben cumplir los requisitos
        /// de serialización de <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">Diccionario vacío para rellenar con un estado serializable.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

       
        private void GridBotones_ItemClick(object sender, ItemClickEventArgs e)
        {
          var  sel = e.ClickedItem;

          if (((SuperComprobadorPRO.Models.BotonesMenu)(sel)).Title == "Comprueba NIF")
            {
                this.Frame.Navigate(typeof(P1NIF));
            }
          else if (((SuperComprobadorPRO.Models.BotonesMenu)(sel)).Title == "Comprueba CCC")
          {
              this.Frame.Navigate(typeof(P2CCC));
          }
          else if (((SuperComprobadorPRO.Models.BotonesMenu)(sel)).Title == "Comprueba IBAN")
          {
              this.Frame.Navigate(typeof(P3Iban));
          }
        }

        private void pageRoot_LayoutUpdated(object sender, object e)
        {
            //var currentFrame = Window.Current.Content as Frame;
            //var currentPage = currentFrame.Content as Page;

            //var newViewState = Windows.UI.ViewManagement.ApplicationView.Value;
            //if (newViewState == Windows.UI.ViewManagement.ApplicationViewState.Snapped)
            //{
            //    Windows.UI.Xaml.DataTemplate Temp = currentPage.Resources["BotonesItemTemplate"] as Windows.UI.Xaml.DataTemplate;
            //    GridBotones.ItemTemplate = Temp;
            //}
            //else
            //{
            //    Windows.UI.Xaml.DataTemplate Temp = currentPage.Resources["BotonesItemTemplate"] as Windows.UI.Xaml.DataTemplate;
            //    GridBotones.ItemTemplate = Temp;
            //}
        }

        private void pageRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var currentFrame = Window.Current.Content as Frame;
            var currentPage = currentFrame.Content as Page;

            var newViewState = Windows.UI.ViewManagement.ApplicationView.Value;
            if (newViewState == Windows.UI.ViewManagement.ApplicationViewState.Snapped)
            {
                Windows.UI.Xaml.DataTemplate Temp = currentPage.Resources["BotonesSnappedItemTemplate"] as Windows.UI.Xaml.DataTemplate;
                GridBotones.ItemTemplate = Temp;
                GridBotones.Margin = new Windows.UI.Xaml.Thickness(25, 0, 20, 100);
            }
            else
            {
                Windows.UI.Xaml.DataTemplate Temp = currentPage.Resources["BotonesItemTemplate"] as Windows.UI.Xaml.DataTemplate;
                GridBotones.ItemTemplate = Temp;
                GridBotones.Margin = new Windows.UI.Xaml.Thickness(120, 0, 40, 100);
            }
        }

  
    }
}
