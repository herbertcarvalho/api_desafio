using Backend.Erp.Skeleton.Application.Extensions;
using System.ComponentModel.DataAnnotations;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;
namespace Backend.Erp.Skeleton.Application.DataAnnotation
{
    public class ValidateString : ValidationAttribute
    {
        private readonly bool _isRequired;
        public ValidateString(bool isRequired = true)
        {
            _isRequired = isRequired;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                if (_isRequired)
                    return new ValidationResult(NotNullMessage(validationContext.DisplayName));
            }

            var stringValue = value.ToString();

            if (!stringValue.IsValidAlphanumeric())
                return new ValidationResult(InvalidMessage(validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
