using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.Attributes {
    public class MinimumAgeAttribute : ValidationAttribute {
        private readonly int _minimumAge;
        public MinimumAgeAttribute(int minimumAge) {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value) {
            if (DateTime.TryParse(value.ToString(), out DateTime date)) {
                return date.AddYears(_minimumAge) < DateTime.Now;
            }
            throw new InvalidOperationException("NotInFuture can only be called on dates");
        }
    }
}
