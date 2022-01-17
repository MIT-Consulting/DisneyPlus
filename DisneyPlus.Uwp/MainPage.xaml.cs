using DisneyPlus.Data.Models;
using DisneyPlus.Uwp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            AddHandlers();
            ApplyPreferences();            
        }

        private void ResolveDependencies() =>
            this.DataContext = ActivatorUtilities.GetServiceOrCreateInstance(((App)Application.Current).Container, typeof(MainPageViewModel));

        private void AddHandlers() =>
            Loaded += (s, a) => AddViewChanged();

        private void ApplyPreferences()
        {
            var appView = ApplicationView.GetForCurrentView();
            var tb = appView.TitleBar;
            tb.BackgroundColor = Color.FromArgb(1, 17, 19, 29);
            tb.ButtonBackgroundColor = tb.BackgroundColor;
        }

        private void AddViewChanged()
        {
            if (lvCollections.GetVisualChild<ScrollViewer>() is ScrollViewer sv)
            {
                sv.ViewChanged += (s, a) =>
                {
                    if (lvCollections.GetVisualChild<VirtualizingStackPanel>() is VirtualizingStackPanel vsp &&
                       vsp.GetVisualChildren<ListViewItem>() is List<ListViewItem> items)
                    {
                        var vm = DataContext as MainPageViewModel;
                        var toLoad = items.Select(x => x.Content)
                                          .OfType<ContentCollectionRef>()
                                          .Where(x => !x.IsLoaded);

                        foreach (var c in toLoad)
                        {
                            Debug.WriteLine($"ViewChanged: {c.Title}");
                            vm.LoadCollectionCommand.ExecuteAsync(c);
                        }
                    }
                };
            }
        }
    }
}
