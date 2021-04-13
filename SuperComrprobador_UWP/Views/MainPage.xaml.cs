using System;

using SuperComrprobador_UWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SuperComrprobador_UWP.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();

            DataContext = new MainViewModel(); // Botones del menú
        }

        private void GridBotones_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sel = e.ClickedItem;

            if (((SuperComrprobador_UWP.Models.BotonesMenu)(sel)).Title == "Comprueba NIF")
            {
                this.Frame.Navigate(typeof(P1NIFPage));
            }
            else if (((SuperComrprobador_UWP.Models.BotonesMenu)(sel)).Title == "Comprueba CCC")
            {
                this.Frame.Navigate(typeof(P2CCCPage));
            }
            else if (((SuperComrprobador_UWP.Models.BotonesMenu)(sel)).Title == "Comprueba IBAN")
            {
                this.Frame.Navigate(typeof(P3IbanPage));
            }
        }


        private void StackPanel_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }
    }
}
