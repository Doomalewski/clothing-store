namespace clothing_store
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public class GreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public GreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value as DateTime?;
            var comparisonValue = validationContext.ObjectType.GetProperty(_comparisonProperty)
                .GetValue(validationContext.ObjectInstance, null) as DateTime?;

            if (currentValue <= comparisonValue)
            {
                return new ValidationResult($"The {validationContext.DisplayName} must be later than {_comparisonProperty}.", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }

}
