using System.ComponentModel.DataAnnotations;

namespace PersonDirectory.Core.Helpers
{
    public class CheckNameValidityAttribute : RegularExpressionAttribute
    {
        public const string STR_ValidNamePattern = @"((^[a-zA-Z ]{2,50})$|(^[ა-ჰ ]{2,50})$)";
        public CheckNameValidityAttribute(string propertyName) : base(STR_ValidNamePattern)
        {
            ErrorMessage = $"{propertyName}{Constants.STR_PropertyShouldNotContainGeoAndLatinSymbols}";
        }
    }
}
