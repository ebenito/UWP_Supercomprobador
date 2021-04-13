using System;

using SuperComrprobador_UWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SuperComrprobador_UWP.Views
{
    public sealed partial class P2CCCPage : Page
    {
        public P2CCCViewModel ViewModel { get; } = new P2CCCViewModel();

        public P2CCCPage()
        {
            InitializeComponent();
        }


        private void GoBack(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Usar el entorno de exploración para volver a la página anterior
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }
    }
}
