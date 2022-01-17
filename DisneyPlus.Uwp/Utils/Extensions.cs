using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace DisneyPlus.Uwp
{
    public static class FrameworkExtensions
    {
        public static void CreateShadow(this FrameworkElement target)
        {
            var compositor = ElementCompositionPreview.GetElementVisual(target).Compositor;
            var shadow = compositor.CreateDropShadow();
            shadow.Color = Colors.Black;
            shadow.BlurRadius = 20;
            shadow.Opacity = 20;
            shadow.Offset = new Vector3(5, 5, 20);

            var sprite = compositor.CreateSpriteVisual();
            sprite.Size = new Vector2((float)target.ActualWidth, (float)target.ActualHeight);
            sprite.Shadow = shadow;

            ElementCompositionPreview.SetElementChildVisual(target, sprite);
        }

        public static void FadeIn(this FrameworkElement target, Action continueWith = null) =>
            target.AnimateOpacity(0, 1, continueWith);


        public static void FadeOut(this FrameworkElement target, Action continueWith = null) =>
            target.AnimateOpacity(1, 0, continueWith, 300);

        private static void AnimateOpacity(this FrameworkElement target, double from, double to, Action continueWith = null, double milliseconds = 700)
        {
            var sb = new Storyboard();
            var da = new DoubleAnimation
            {
                From = from,
                To = to,                
                Duration = TimeSpan.FromMilliseconds(milliseconds),
                FillBehavior = FillBehavior.HoldEnd,
                AutoReverse = false,
            };
            da.Completed += (s, a) => continueWith?.Invoke();
            Storyboard.SetTarget(da, target);
            Storyboard.SetTargetProperty(da, "Opacity");
            sb.Children.Add(da);
            sb.Begin();
        }

        public static T GetVisualChild<T>(this DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    var childOfChild = GetVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static List<T> GetVisualChildren<T>(this DependencyObject obj) where T : DependencyObject
        {
            var childList = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    childList.Add(child as T);
            }

            if (childList.Count > 0)
                return childList;

            return null;
        }
    }
}
