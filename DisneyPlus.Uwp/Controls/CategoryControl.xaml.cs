using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace DisneyPlus.Uwp.Controls
{
    public sealed partial class CategoryControl : UserControl
    {
        public CategoryControl()
        {
            this.InitializeComponent();
            AddHandlers();
        }

        private void AddHandlers() => 
            Loaded += (s, a) => canvas.CreateShadow();

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        public static readonly DependencyProperty IsSelectedProperty = 
            DependencyProperty.Register(nameof(IsSelected),
                                        typeof(bool),
                                        typeof(CategoryControl),
                                        new PropertyMetadata(false, new PropertyChangedCallback(IsSelectedChanged)));
        private static void IsSelectedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
        }

        public string ImageUri
        {
            get => (string)GetValue(ImageUriProperty);
            set => SetValue(ImageUriProperty, value);
        }
        public static readonly DependencyProperty ImageUriProperty = 
            DependencyProperty.Register(nameof(ImageUri),
                                        typeof(string),
                                        typeof(CategoryControl),
                                        new PropertyMetadata(null));

        public string VideoUri
        {
            get => (string)GetValue(VideoUriProperty);
            set => SetValue(VideoUriProperty, value);
        }
        public static readonly DependencyProperty VideoUriProperty = 
            DependencyProperty.Register(nameof(VideoUri),
                                        typeof(string),
                                        typeof(CategoryControl),
                                        new PropertyMetadata(null));


        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);
            BackgroundMovie.FadeIn(() => BackgroundMovie.Play());
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);
            BackgroundMovie.FadeOut(() => BackgroundMovie.Stop());
        }        
        
    }
}
