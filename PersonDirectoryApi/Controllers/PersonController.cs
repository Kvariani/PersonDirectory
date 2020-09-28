using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonDirectory.Core.Entities;
using PersonDirectory.Core.Enums;
using PersonDirectory.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PersonDirectory.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        readonly IPersonRepository _repository;
        readonly bool _useSqlFunction;

        public PersonController(IPersonRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _useSqlFunction = Convert.ToBoolean(configuration.GetSection("UseSqlFunctionForFastSearch")?.Value ?? false.ToString());
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreatePerson([FromBody] BasePerson person) => await _repository.CreatePerson(person);

        [HttpPut]
        public async Task<ActionResult> ModifyPerson(int id, [FromBody] BasePerson person) => await DoMyAction(() => _repository.ModifyPerson(id, person));

        [HttpPost]
        public async Task<ActionResult> AddRelatedPerson(int personId, int relatedPersonID, RelationTypeEnum relationType) => await DoMyAction(() => _repository.AddRelatedPerson(personId, relatedPersonID, relationType));

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteRelatedPerson(int personId, int relatedPersonID) => await DoMyAction(() => _repository.DeleteRelatedPerson(personId, relatedPersonID));

        [HttpDelete]
        public async Task<ActionResult<bool>> DeletePerson(int id) => await DoMyAction(() => _repository.DeletePerson(id));

        [HttpGet]
        public async Task<ActionResult<Person>> GetPerson(int id) => await _repository.GetPerson(id);

        [HttpGet]
        public async Task<IEnumerable<Person>> GetPersons(string searchString, int pageIndex, int pageSize, bool fastSearch) => await _repository.GetPersons(searchString, pageIndex, pageSize, fastSearch, _useSqlFunction);

        async Task<ActionResult> DoMyAction(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
