using NUnit.Framework;
using System;
using System.Linq;
using Shouldly;
using static PersonDirectory.Core.Helpers.Constants;
using PersonDirectory.Core.Entities;
using PersonDirectory.Core.Enums;

namespace PersonDirectory.Tests.Validation
{
    public class PersonValidationTest : NUnitTestBase
    {
        [Test(Description = "ვალიდური ფიზიკური პირის ინფორმაცია")]
        public void ValidPerson() => CreatePersonAndGetValidationResult(_ => { }).ShouldBeNull();

        [Test(Description = "სახელის შევსება აუცილებელია")]
        public void FirstnameMustNotBeEmpty() => CreatePersonAndGetValidationResult(_ => _.Firstname = null).ShouldBe(STR_FirstnameIsRequired);

        [Test(Description = "გვარის შევსება აუცილებელია")]
        public void LastnameMustNotBeEmpty() => CreatePersonAndGetValidationResult(_ => _.Lastname = null).ShouldBe(STR_LastnameIsRequired);

        [Test(Description = "პირადი ნომრის შევსება აუცილებელია")]
        public void IdNumberMustNotBeEmpty() => CreatePersonAndGetValidationResult(_ => _.IDNumber = null).ShouldBe(STR_IdNumberIsRequired);

        [Test(Description = "პირადი ნომრის შევსება აუცილებელია")]
        public void IdNumberMustBe11Digit() => CreatePersonAndGetValidationResult(_ => _.IDNumber = "000asd").ShouldBe(STR_IdNUmberIsNotValid);

        [Test(Description = "სახელი არ უნდა შეიცავდეს ერთდროულად ქართულ და ლათინურ ასოებს")]
        public void FirstnameMustNotContainsEnglishAndGeoWordsTogether() => CreatePersonAndGetValidationResult(_ => _.Firstname = "ბექa").ShouldBe($"სახელი{STR_PropertyShouldNotContainGeoAndLatinSymbols}");

        [Test(Description = "გვარი არ უნდა შეიცავდეს ერთდროულად ქართულ და ლათინურ ასოებს")]
        public void LastnameMustNotContainsEnglishAndGeoWordsTogether() => CreatePersonAndGetValidationResult(_ => _.Lastname = "ქვარიანi").ShouldBe($"გვარი{STR_PropertyShouldNotContainGeoAndLatinSymbols}");

        [Test(Description = "სახელი და გვარი არ უნდა შეიცავდეს ერთდროულად ქართულ და ლათინურ ასოებს")]
        public void FullnameMustNotContainsEnglishAndGeoWordsTogether() => CreatePersonAndGetValidationResult(_ => { _.Firstname = "ბექა"; _.Lastname = "Kvariani"; }).ShouldBe($"გვარი და სახელი{STR_PropertyShouldNotContainGeoAndLatinSymbols}");

        [Test(Description = "სახელი არ უნდა იყოს 2 სიმბოლოზე ნაკლები")]
        public void FirstnameMinLenghtMustBe2() => CreatePersonAndGetValidationResult(_ => { _.Firstname = "ბ"; }).ShouldBe($"სახელი{STR_PropertyShouldNotContainGeoAndLatinSymbols}");

        [Test(Description = "სახელი არ უნდა იყოს 50 სიმბოლოზე მეტი")]
        public void FirstnameMaxLenghtMustBe50() => CreatePersonAndGetValidationResult(_ => { _.Firstname = "სახელისახელისახელისახელისახელისახელისახელისახელისახელისახელი"; }).ShouldBe($"სახელი{STR_PropertyShouldNotContainGeoAndLatinSymbols}");

        [Test(Description = "გვარი არ უნდა იყოს 2 სიმბოლოზე ნაკლები")]

        public void LastnameMinLenghtMustBe2() => CreatePersonAndGetValidationResult(_ => { _.Lastname = "ქ"; }).ShouldBe($"გვარი{STR_PropertyShouldNotContainGeoAndLatinSymbols}");
        [Test(Description = "გვარი არ უნდა იყოს 50 სიმბოლოზე მეტი")]
        public void LastnameMaxLenghtMustBe50() => CreatePersonAndGetValidationResult(_ => { _.Lastname = "გვარიგვარიგვარიგვარიგვარიგვარიგვარიგვარიგვარიგვარიგვარიგვარი"; }).ShouldBe($"გვარი{STR_PropertyShouldNotContainGeoAndLatinSymbols}");

        [Test(Description = "ასაკი არ უნდა იყოს 18 წელზე ნაკლები")]
        public void AgeShouldNotBeLessThen18Year() => CreatePersonAndGetValidationResult(_ => _.DateOfBirth = DateTime.Now).ShouldBe(STR_AgeShouldNotBeLessThen18Year);

        [Test(Description = "ასაკის მითითება აუცილებელია")]
        public void AgeShouldNotBeLessThen18Yea1r() => CreatePersonAndGetValidationResult(_ => _.DateOfBirth = null).ShouldBe(STR_BirthDateIsRequired);

        [Test(Description = "სქესის მითითება აუცილებელია")]
        public void GenderMustNotBeNull() => CreatePersonAndGetValidationResult(_ => _.Gender = null).ShouldBe(STR_GenderIsRequired);

        string CreatePersonAndGetValidationResult(Action<Person> modifyPersonAction)
        {
            var person = ObjectFactory.CreateNewPerson("ბექა", "ქვარიანი", "01027058514", new DateTime(1990, 8, 8), GenderEnum.Male);
            modifyPersonAction.Invoke(person);
            var result = person.GetValidationResults();
            return result.SingleOrDefault()?.ErrorMessage;
        }
    }
}


