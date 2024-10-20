using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Validation
{
    public class InAttribute : ValidationAttribute
    {
        private readonly string[] _validTypes;

        public InAttribute(params string[] validTypes)
        {
            _validTypes = validTypes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !_validTypes.Contains(value.ToString()))
            {
                throw new ArgumentException(
                    $"{validationContext.DisplayName} must be one of the following: {string.Join(", ", _validTypes)}");
            }

            return ValidationResult.Success;
        }
    }
}