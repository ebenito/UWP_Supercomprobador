using System;
using System.Threading.Tasks;
using System.Windows.Input;

using SuperComrprobador_UWP.Helpers;
using SuperComrprobador_UWP.Services;

using Windows.ApplicationModel;
using Microsoft.UI.Xaml;

namespace SuperComrprobador_UWP.ViewModels
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/pages/settings.md
    public class SettingsViewModel : Observable
    {
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        // TODO: Microsoft.Services.Store.Engagement is not compatible with WinUI 3 desktop apps.
        // Re-evaluate if Store feedback integration is needed via a different mechanism.
        public Visibility FeedbackLinkVisibility => Visibility.Collapsed;

        // TODO: StoreServicesFeedbackLauncher is not compatible with WinUI 3 desktop apps.
        public ICommand LaunchFeedbackHubCommand => new RelayCommand(() => { });

        public SettingsViewModel()
        {
        }

        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
            await Task.CompletedTask;
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
