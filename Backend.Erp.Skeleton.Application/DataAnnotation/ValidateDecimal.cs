using System;
using System.ComponentModel.DataAnnotations;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;

namespace Backend.Erp.Skeleton.Application.DataAnnotation
{
    public class ValidateDecimal : ValidationAttribute
    {
        private bool _allowsZero;

        public ValidateDecimal(bool allowsZero = false)
        {
            _allowsZero = allowsZero;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyType = validationContext.ObjectType.GetProperty(validationContext.MemberName)?.PropertyType;
            bool isNullable = Nullable.GetUnderlyingType(propertyType) is not null || propertyType.IsClass;

            if (value is null)
            {
                if (isNullable)
                    return ValidationResult.Success;

                return new ValidationResult(ErrorMessage ?? NotNullMessage(validationContext.DisplayName));
            }

            if (!decimal.TryParse(value.ToString(), out decimal decimalValue))
                return new ValidationResult(ErrorMessage ?? InvalidMessage(validationContext.DisplayName));

            if (decimalValue < 0)
                return new ValidationResult(ErrorMessage ?? GreaterThanZeroMessage(validationContext.DisplayName));

            if (!_allowsZero && decimalValue == 0)
                return new ValidationResult(ErrorMessage ?? GreaterThanZeroMessage(validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
