using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.Attributes {
    public class NotInFutureAttribute : ValidationAttribute {
        public override bool IsValid(object value) {
            if (DateTime.TryParse(value.ToString(), out DateTime date)) {
                return date < DateTime.Now;
            }
            throw new InvalidOperationException("NotInFuture can only be called on dates");
        }
    }
}
