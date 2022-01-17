using DisneyPlus.Uwp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DisneyPlus.Uwp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ResolveDependencies();
            ApplyPreferences();
        }

        private void ResolveDependencies() =>
            this.DataContext = ActivatorUtilities.GetServiceOrCreateInstance(((App)Application.Current).Container, typeof(MainPageViewModel));

        private void ApplyPreferences()
        {
            var appView = ApplicationView.GetForCurrentView();
            var tb = appView.TitleBar;
            tb.BackgroundColor = Color.FromArgb(1, 17, 19, 29);
            tb.ButtonBackgroundColor = tb.BackgroundColor;
        }
    }
}
