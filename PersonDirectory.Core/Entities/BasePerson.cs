using PersonDirectory.Core.Enums;
using PersonDirectory.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static PersonDirectory.Core.Helpers.Constants;

namespace PersonDirectory.Core.Entities
{
    public class BasePerson : EntityBase, IValidatableObject
    {
        [Required(ErrorMessage = STR_FirstnameIsRequired), CheckNameValidity("სახელი")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = STR_LastnameIsRequired), CheckNameValidity("გვარი")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = STR_GenderIsRequired)]
        public GenderEnum? Gender { get; set; }
        [Required(ErrorMessage = STR_IdNumberIsRequired), RegularExpression(@"([0-9]{11})", ErrorMessage = STR_IdNUmberIsNotValid)]
        public string IDNumber { get; set; }
        [Required(ErrorMessage = STR_BirthDateIsRequired)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateOfBirth { get; set; }
        public ICollection<TelNumber> TelNumbers { get; set; }

        string Fullname => $"{Lastname} {Firstname}";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth == null)
                yield return new ValidationResult(STR_BirthDateIsRequired);

            if (CalculateAge(DateOfBirth.Value, DateTime.Now) < 18)
                yield return new ValidationResult(STR_AgeShouldNotBeLessThen18Year);

            if (!Regex.IsMatch(Fullname, CheckNameValidityAttribute.STR_ValidNamePattern))
                yield return new ValidationResult($"გვარი და სახელი{STR_PropertyShouldNotContainGeoAndLatinSymbols}");
        }

        int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;

            return age;
        }
    }
}
