using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace YearWork.Behaviors {
    class ApearBehavior {
        public static readonly DependencyProperty ShowedProperty = DependencyProperty.RegisterAttached(
       "Showed", typeof(bool), typeof(ApearBehavior), new PropertyMetadata(false));

        public static void SetShowed(DependencyObject element, bool value) {
            element.SetValue(ShowedProperty, value);
        }

        public static bool GetShowed(DependencyObject element) {
            return (bool)element.GetValue(ShowedProperty);
        }
    }
}
