using System;

using SuperComrprobador_UWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SuperComrprobador_UWP.Views
{
    public sealed partial class P1NIFPage : Page
    {
        public P1NIFViewModel ViewModel { get; } = new P1NIFViewModel();

        public P1NIFPage()
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
