using PersonDirectory.Core.Entities;
using PersonDirectory.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Tests
{
    public static class ObjectFactory
    {
        public static Person CreateNewPerson(string firstname, string lastname, string idNumber, DateTime dateOfBirth, GenderEnum gender) =>
            new Person() { Firstname = firstname, Lastname = lastname, IDNumber = idNumber, DateOfBirth = dateOfBirth, Gender = gender };
    }
}
