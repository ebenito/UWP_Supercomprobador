using System;

using SuperComrprobador_UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SuperComrprobador_UWP.Views
{
    // TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void GoBack(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Usar el entorno de exploración para volver a la página anterior
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeAsync();
        }
    }
}
