using System;
using System.ComponentModel.DataAnnotations;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;

namespace Backend.Erp.Skeleton.Application.DataAnnotation
{
    public class ValidateInt : ValidationAttribute
    {
        private bool _allowsZero;

        public ValidateInt(bool allowsZero = false)
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

            if (!int.TryParse(value.ToString(), out int intValue))
                return new ValidationResult(ErrorMessage ?? InvalidMessage(validationContext.DisplayName));

            if (intValue < 0)
                return new ValidationResult(ErrorMessage ?? GreaterThanZeroMessage(validationContext.DisplayName));

            if (!_allowsZero && intValue == 0)
                return new ValidationResult(ErrorMessage ?? GreaterThanZeroMessage(validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
