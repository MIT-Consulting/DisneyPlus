using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;

namespace DisneyPlus.Uwp.Converters
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public BooleanToVisibilityConverter() {}

        private static BooleanToVisibilityConverter _converter = null;
        protected override object ProvideValue() => 
            _converter ?? (_converter = new BooleanToVisibilityConverter());

        public object Convert(object value, Type targetType, object parameter, string language) =>
            value is bool && (bool)value
            ? Visibility.Visible
            : (object)Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, string language) => 
            value is Visibility vis && vis == Visibility.Visible;
    }
}
