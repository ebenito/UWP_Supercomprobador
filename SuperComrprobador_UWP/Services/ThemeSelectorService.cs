using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using SuperComrprobador_UWP.Helpers;

namespace SuperComrprobador_UWP.Services
{
    public static class ThemeSelectorService
    {
        private const string SettingsKey = "AppBackgroundRequestedTheme";
        public static Window AppWindow { get; set; }
        public static ElementTheme Theme { get; set; } = ElementTheme.Default;
        private static readonly string _fallbackSettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SuperComrprobador_UWP", "settings.ini");

        private static bool HasPackageIdentity()
        {
            try { _ = Windows.Storage.ApplicationData.Current; return true; }
            catch { return false; }
        }

        public static async Task InitializeAsync() => Theme = await LoadThemeFromSettingsAsync();

        public static async Task SetThemeAsync(ElementTheme theme)
        {
            Theme = theme;
            await SetRequestedThemeAsync();
            await SaveThemeInSettingsAsync(Theme);
        }

        public static async Task SetRequestedThemeAsync()
        {
            if (AppWindow?.Content is FrameworkElement fe)
            {
                var dq = fe.DispatcherQueue;
                if (dq.HasThreadAccess) { fe.RequestedTheme = Theme; }
                else
                {
                    var tcs = new TaskCompletionSource();
                    dq.TryEnqueue(DispatcherQueuePriority.Normal, () => { fe.RequestedTheme = Theme; tcs.SetResult(); });
                    await tcs.Task;
                }
            }
        }

        private static async Task<ElementTheme> LoadThemeFromSettingsAsync()
        {
            string themeName = HasPackageIdentity()
                ? await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<string>(SettingsKey)
                : ReadFromFallbackFile(SettingsKey);
            return Enum.TryParse(themeName, out ElementTheme t) ? t : ElementTheme.Default;
        }

        private static async Task SaveThemeInSettingsAsync(ElementTheme theme)
        {
            if (HasPackageIdentity())
                await Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, theme.ToString());
            else
                WriteToFallbackFile(SettingsKey, theme.ToString());
        }

        private static string ReadFromFallbackFile(string key)
        {
            try
            {
                if (!File.Exists(_fallbackSettingsPath)) return null;
                foreach (var line in File.ReadAllLines(_fallbackSettingsPath))
                {
                    var p = line.Split('=', 2);
                    if (p.Length == 2 && p[0].Trim() == key) return p[1].Trim();
                }
            }
            catch { }
            return null;
        }

        private static void WriteToFallbackFile(string key, string value)
        {
            try
            {
                var dir = Path.GetDirectoryName(_fallbackSettingsPath);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                var lines = File.Exists(_fallbackSettingsPath) ? new List<string>(File.ReadAllLines(_fallbackSettingsPath)) : new List<string>();
                bool found = false;
                for (int i = 0; i < lines.Count; i++)
                    if (lines[i].StartsWith(key + "=")) { lines[i] = key + "=" + value; found = true; break; }
                if (!found) lines.Add(key + "=" + value);
                File.WriteAllLines(_fallbackSettingsPath, lines);
            }
            catch { }
        }
    }
}