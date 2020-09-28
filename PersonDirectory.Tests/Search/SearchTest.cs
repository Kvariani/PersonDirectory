using NUnit.Framework;
using System.Linq;
using Shouldly;
using PersonDirectory.Core.Entities;
using System.Collections.Generic;

namespace PersonDirectory.Tests.Search
{
    public class SearchTest : SearchTestBase
    {
        [Test(Description = "ფიზიკური პირის სახელი შეიცავს გადაცემულ მნიშვნელობას")]
        public void SearchByFirstname() => FindPersons("სატესტ", false).Single().Firstname.ShouldContain("სატესტ");

        [Test(Description = "ფიზიკური პირის გვარი შეიცავს გადაცემულ მნიშვნელობას")]
        public void SearchByLastname() => FindPersons("ქვარიან", false).Single().Lastname.ShouldContain("ქვარიან");

        [Test(Description = "ფიზიკური პირის გვარი შეიცავს გადაცემულ მნიშვნელობას")]
        public void SearchByIdNumber() => FindPersons("01027058", false).Single().IDNumber.ShouldContain("01027058");

        [Test(Description = "სხვადასხვა ფიზიკური პირის სახელი, გვარი ან პირადი ნომერი შეიცავს გადაცემულ მნიშვნელობებს")]
        public void SearchByMultipleValue() => FindPersons("pirovneba 01027058", false).Count().ShouldBe(2);

        [Test(Description = "ფიზიკური პირი უნდა მოიძებნოს ტელეფონის ნომრით")]
        public void SearchByTelNumbers()
        {
            const string number = "558231299";
            AddTelNumbertToPerson(number);
            FindPersons(number, false).Single().TelNumbers.Single().Number.ShouldBe(number);
        }



        void AddTelNumbertToPerson(string number)
        {
            using (var context = GetDbContext())
            {
                context.Persons.First().TelNumbers = new List<TelNumber>() { new TelNumber() { Number = number, TelNumberType = Core.Enums.TelNumberTypeEnum.Mobile } };
                context.SaveChanges();
            }
        }
    }
}
