using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace DisneyPlus.Uwp.Controls
{
    public sealed partial class LoadingControl : UserControl
    {
        public LoadingControl()
        {
            this.InitializeComponent();
            AddHandlers();
        }

        private void AddHandlers()
        {
            Loaded += (s, a) =>
            {
                var sb = Resources["sbRotate"] as Storyboard;
                sb.Begin();
            };
        }
    }
}
