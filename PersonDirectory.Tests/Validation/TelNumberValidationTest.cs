using NUnit.Framework;
using PersonDirectory.Core.Entities;
using Shouldly;
using System.Linq;
using static PersonDirectory.Core.Helpers.Constants;

namespace PersonDirectory.Tests.Validation
{
    public class TelNumberValidationTest : NUnitTestBase
    {
        [Test(Description = "ტელეფონის ნომერი არ უნდა იყოს მინიმუმ 4 სიმბოლოზე ნაკლები")]
        public void TelNumberShouldNotBeLessThen4Symbol() => CreateTelNumberAndGetValidationResult("12").ShouldBe(STR_TelNumberIsNotCurrentFormat);

        [Test(Description = "ტელეფონის ნომერი არ უნდა იყოს მინიმუმ 50 სიმბოლოზე მეტი")]
        public void TelNumberShouldNotBeMoreThen50Symbol() => CreateTelNumberAndGetValidationResult("123456789012345678901234567890123456789012345678901234567890").ShouldBe(STR_TelNumberIsNotCurrentFormat);

        [Test(Description = "ტელეფონის ნომერი შესაძლებელია იწყებოდეს + სიმბოლოთი")]
        public void ValidTelNumberWithPlusSymbol() => CreateTelNumberAndGetValidationResult("+995558231299").ShouldBeNull();

        [Test(Description = "ტელეფონის ნომერი შესაძლებელია არ იწყებოდეს + სიმბოლოთი")]
        public void ValidTelNumberWithoutPlusSymbol() => CreateTelNumberAndGetValidationResult("558231299").ShouldBeNull();

        [Test(Description = "ტელეფონის ნომერი არ უნდა შეიცავდეს ერთზე მეტ + სიმბოლოს")]
        public void TelNumberShouldNotContainMoreThenOnePlusSymbol() => CreateTelNumberAndGetValidationResult("++558").ShouldBe(STR_TelNumberIsNotCurrentFormat);

        [Test(Description = "ტელეფონის ნომერი უნდა არ უნდა შეიცავდეს ასოებს")]
        public void TelNumberShoudNotContainAlphabetLetters() => CreateTelNumberAndGetValidationResult("ა").ShouldBe(STR_TelNumberIsNotCurrentFormat);

        string CreateTelNumberAndGetValidationResult(string number)
        {
            var result = new TelNumber() { Number = number }.GetValidationResults();
            return result.SingleOrDefault()?.ErrorMessage;
        }
    }
}
