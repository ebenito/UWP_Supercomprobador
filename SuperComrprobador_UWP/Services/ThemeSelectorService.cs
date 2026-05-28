using System;
using System.Threading.Tasks;

using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

using SuperComrprobador_UWP.Helpers;

using Windows.Storage;

namespace SuperComrprobador_UWP.Services
{
    public static class ThemeSelectorService
    {
        private const string SettingsKey = "AppBackgroundRequestedTheme";

        // In WinUI 3, the app window must be set externally (e.g. from App.xaml.cs)
        public static Window AppWindow { get; set; }

        public static ElementTheme Theme { get; set; } = ElementTheme.Default;

        public static async Task InitializeAsync()
        {
            Theme = await LoadThemeFromSettingsAsync();
        }

        public static async Task SetThemeAsync(ElementTheme theme)
        {
            Theme = theme;

            await SetRequestedThemeAsync();
            await SaveThemeInSettingsAsync(Theme);
        }

        public static async Task SetRequestedThemeAsync()
        {
            if (AppWindow?.Content is FrameworkElement frameworkElement)
            {
                var dispatcherQueue = frameworkElement.DispatcherQueue;
                if (dispatcherQueue.HasThreadAccess)
                {
                    frameworkElement.RequestedTheme = Theme;
                }
                else
                {
                    var tcs = new TaskCompletionSource();
                    dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                    {
                        frameworkElement.RequestedTheme = Theme;
                        tcs.SetResult();
                    });
                    await tcs.Task;
                }
            }
        }

        private static async Task<ElementTheme> LoadThemeFromSettingsAsync()
        {
            ElementTheme cacheTheme = ElementTheme.Default;
            string themeName = await ApplicationData.Current.LocalSettings.ReadAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(themeName))
            {
                Enum.TryParse(themeName, out cacheTheme);
            }

            return cacheTheme;
        }

        private static async Task SaveThemeInSettingsAsync(ElementTheme theme)
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, theme.ToString());
        }
    }
}
