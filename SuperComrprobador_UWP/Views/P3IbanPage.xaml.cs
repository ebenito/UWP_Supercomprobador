using System;

using SuperComrprobador_UWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SuperComrprobador_UWP.Views
{
    public sealed partial class P3IbanPage : Page
    {
        public P3IbanViewModel ViewModel { get; } = new P3IbanViewModel();

        public P3IbanPage()
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
