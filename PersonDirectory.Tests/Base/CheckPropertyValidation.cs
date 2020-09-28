using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonDirectory.Tests
{
    public static class CheckPropertyValidation
    {
        public static IList<ValidationResult> GetValidationResults(this object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result, true);
            if (model is IValidatableObject) 
                (model as IValidatableObject).Validate(validationContext);
            return result;
        }
    }
}
