using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SuperComrprobador_UWP.Activation;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

using Windows.System;

namespace SuperComrprobador_UWP.Services
{
    internal class ActivationService
    {
        private readonly App _app;
        private readonly Type _defaultNavItem;
        private Lazy<UIElement> _shell;
        private readonly Window _mainWindow;
        private object _lastActivationArgs;

        public static readonly KeyboardAccelerator AltLeftKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);
        public static readonly KeyboardAccelerator BackKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.GoBack);

        public ActivationService(App app, Type defaultNavItem, Window mainWindow, Lazy<UIElement> shell = null)
        {
            _app = app;
            _shell = shell;
            _defaultNavItem = defaultNavItem;
            _mainWindow = mainWindow;
        }

        public async Task ActivateAsync(object activationArgs)
        {
            if (IsInteractive(activationArgs))
            {
                await InitializeAsync();
                if (_mainWindow.Content == null)
                {
                    var frame = new Frame();
                    _mainWindow.Content = _shell?.Value ?? frame;
                    NavigationService.Frame = _mainWindow.Content as Frame;
                    NavigationService.NavigationFailed += (sender, e) => throw e.Exception;
                }
            }
            await HandleActivationAsync(activationArgs);
            _lastActivationArgs = activationArgs;
            if (IsInteractive(activationArgs))
            {
                _mainWindow.Activate();
                await StartupAsync();
            }
        }

        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            var ka = new KeyboardAccelerator() { Key = key };
            if (modifiers.HasValue) ka.Modifiers = modifiers.Value;
            ka.Invoked += OnKeyboardAcceleratorInvoked;
            return ka;
        }

        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            args.Handled = NavigationService.GoBack();
        }

        private async Task InitializeAsync()
        {
            await ThemeSelectorService.InitializeAsync();
        }

        private async Task HandleActivationAsync(object activationArgs)
        {
            var handler = GetActivationHandlers().FirstOrDefault(h => h.CanHandle(activationArgs));
            if (handler != null) await handler.HandleAsync(activationArgs);
            if (IsInteractive(activationArgs))
            {
                var defaultHandler = new DefaultActivationHandler(_defaultNavItem);
                if (defaultHandler.CanHandle(activationArgs)) await defaultHandler.HandleAsync(activationArgs);
            }
        }

        private async Task StartupAsync()
        {
            await ThemeSelectorService.SetRequestedThemeAsync();
        }

        private IEnumerable<ActivationHandler> GetActivationHandlers() { yield break; }

        private bool IsInteractive(object args) => args is LaunchActivatedEventArgs;
    }
}
