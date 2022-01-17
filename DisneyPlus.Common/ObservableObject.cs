using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DisneyPlus.Common
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals((object)storage, (object)value)) return false;
            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This cannot be an event")]
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ValidateProperty(propertyName);
        }

        public bool ValidateProperty(string propertyName) => ValidateProperty(this, propertyName);

        [SuppressMessage("Microsoft.Design", "CA1006:GenericMethodsShouldProvideTypeParameter", Justification = "This syntax is more convenient than other alternatives.")]
        public bool ValidateProperty<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));

            var body = propertyExpression.Body as MemberExpression;
            var expression = body.Expression as ConstantExpression;
            return ValidateProperty(expression.Value, body.Member.Name);
        }

        protected virtual bool ValidateProperty(object source, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            var propertyInfo = source.GetType().GetRuntimeProperty(propertyName);
            if (propertyInfo == null) return true;

            var propertyErrors = new List<ValidationResult>();
            var isValid = TryValidateProperty(source, propertyInfo, propertyErrors);

            return isValid;
        }

        private bool TryValidateProperty(object source, PropertyInfo propertyInfo, List<ValidationResult> propertyErrors)
        {
            try
            {
                var results = new List<ValidationResult>();
                var context = new ValidationContext(source) { MemberName = propertyInfo.Name };
                var propertyValue = propertyInfo.GetValue(source);

                var isValid = Validator.TryValidateProperty(propertyValue, context, results);
                if (results.Any()) propertyErrors.AddRange(results);
                return isValid;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }

}
