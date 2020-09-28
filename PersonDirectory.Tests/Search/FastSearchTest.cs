using NUnit.Framework;
using System.Linq;
using Shouldly;

namespace PersonDirectory.Tests.Search
{
    public class FastSearchTest : SearchTestBase
    {

        [Test(Description = "ფიზიკური პირის სახელი შეიცავს გადაცემულ მნიშვნელობას")]
        public void SearchByFirstname() => FindPersons("სატესტ", true).Single().Firstname.ShouldContain("სატესტ");

        [Test(Description = "ფიზიკური პირის გვარი შეიცავს გადაცემულ მნიშვნელობას")]
        public void SearchByLastname() => FindPersons("ქვარიან", true).Single().Lastname.ShouldContain("ქვარიან");

        [Test(Description = "ფიზიკური პირის გვარი შეიცავს გადაცემულ მნიშვნელობას")]
        public void SearchByIdNumber() => FindPersons("01027058", true).Single().IDNumber.ShouldContain("01027058");

        [Test(Description = "სხვადასხვა ფიზიკური პირის სახელი, გვარი ან პირადი ნომერი შეიცავს გადაცემულ მნიშვნელობებს")]
        public void SearchByMultipleValue() => FindPersons("pirovneba 01027058", true).Count().ShouldBe(2);

     
    }
}

