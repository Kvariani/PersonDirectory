using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonDirectory.Core.Entities;
using PersonDirectory.Core.Mapper;
using PersonDirectory.Core.Repositories;
using PersonDirectory.Infrastructure.DBContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Tests.Search
{
    public class SearchTestBase : NUnitTestBase
    {
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
            CreatePersonsAndSave();
        }

        internal IEnumerable<Person> FindPersons(string searchText, bool fastSearch)
        {
            var context = GetDbContext();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();
            var repo = new PersonRepository(context, mapper);

            var result = repo.GetPersons(searchText, 1, 50, fastSearch).GetAwaiter().GetResult();
            return result;
        }

        public override void GlobalTeardown()
        {
            base.GlobalTeardown();

            using (var context = GetDbContext())
            {
                context.RemoveRange(context.Persons);
                context.SaveChanges();
            }
        }

        void CreatePersonsAndSave()
        {

            using (var context = GetDbContext())
            {
                var person = ObjectFactory.CreateNewPerson("ბექა", "ქვარიანი", "01027058514", new DateTime(1990, 08, 08), Core.Enums.GenderEnum.Male);
                var person2 = ObjectFactory.CreateNewPerson("სატესტო", "pirovneba", "00000000000", new DateTime(1990, 08, 08), Core.Enums.GenderEnum.Male);
                context.Persons.AddRange(person, person2);
                context.SaveChanges();
            }

        }
        internal ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "DB").Options;
            return new ApplicationDbContext(options);
        }
    }
}
