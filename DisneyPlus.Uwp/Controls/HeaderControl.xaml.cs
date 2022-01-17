using DisneyPlus.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace DisneyPlus.Uwp.Controls
{
    public sealed partial class HeaderControl : UserControl
    {
        private IAppState _appState;
        public HeaderControl()
        {
            this.InitializeComponent();
            ResolveDependencies();
            AddHandlers();
        }

        private void ResolveDependencies() =>
            _appState = (IAppState)ActivatorUtilities.GetServiceOrCreateInstance(((App)Application.Current).Container, typeof(IAppState));

        private void AddHandlers()
        {
            Loaded += (s, a) => _appState.HeaderControls.Add(this);
            Unloaded += (s, a) => _appState.HeaderControls.Remove(this);
        }

        public string BackgroundUri
        {
            get => (string)GetValue(BackgroundUriProperty);
            set => SetValue(BackgroundUriProperty, value);
        }
        public static readonly DependencyProperty BackgroundUriProperty =
            DependencyProperty.Register(nameof(BackgroundUri),
                                        typeof(string),
                                        typeof(HeaderControl),
                                        new PropertyMetadata(null));

        public string ForegroundUri
        {
            get => (string)GetValue(ForegroundUriProperty);
            set => SetValue(ForegroundUriProperty, value);
        }
        public static readonly DependencyProperty ForegroundUriProperty =
            DependencyProperty.Register(nameof(ForegroundUri),
                                        typeof(string),
                                        typeof(HeaderControl),
                                        new PropertyMetadata(null));

        public void OnSelected()
        {
            var sb = Resources["sbForeground"] as Storyboard;
            sb.Begin();
        }
    }
}
