using System;

using SuperComrprobador_UWP.Services;

using Microsoft.UI.Xaml;

namespace SuperComrprobador_UWP
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;
        internal static Window MainWindow { get; private set; }

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            MainWindow = new Window();
            MainWindow.Activate();

            // Pass the window to ThemeSelectorService so it can apply the theme.
            ThemeSelectorService.AppWindow = MainWindow;

            await ActivationService.ActivateAsync(args);
        }

        private void OnAppUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainPage), MainWindow);
        }
    }
}
