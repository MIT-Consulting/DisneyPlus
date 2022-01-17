using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace DisneyPlus.Uwp.Controls
{
    public sealed partial class TileControl : UserControl
    {
        public TileControl() => 
            this.InitializeComponent();

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title),
                                        typeof(string),
                                        typeof(TileControl),
                                        new PropertyMetadata(null));

        public string ImageUri
        {
            get => (string)GetValue(ImageUriProperty);
            set => SetValue(ImageUriProperty, value);
        }
        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register(nameof(ImageUri),
                                        typeof(string),
                                        typeof(TileControl),
                                        new PropertyMetadata(null));

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);

            var sb = Resources["sbScaleUp"] as Storyboard;
            sb.Begin();            
            tileBorder.BorderBrush = new SolidColorBrush(Colors.White);
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            var sb = Resources["sbScaleDown"] as Storyboard;            
            sb.Begin();            
            tileBorder.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }
    }
}
