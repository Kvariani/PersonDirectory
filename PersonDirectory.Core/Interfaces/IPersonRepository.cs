using PersonDirectory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonDirectory.Core.Repositories
{
    public interface IPersonRepository
    {
        Task<int> CreatePerson(BasePerson person);
        Task ModifyPerson(int id, BasePerson person);
        Task AddRelatedPerson(int personId, int relatedPersonID, Enums.RelationTypeEnum relationType);
        Task DeleteRelatedPerson(int personId, int relatedPersonID);
        Task DeletePerson(int personId);
        Task<Person> GetPerson(int personId);
        Task<IEnumerable<Person>> GetPersons(string searchString, int pageIndex, int pageSize, bool fastSearch, bool useSqlFunction);
    }
}
