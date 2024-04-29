using Api.Contracts;
using Api.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private ILogger<PersonController> _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public PersonController
        (
            ILogger<PersonController> logger, 
            IRepositoryWrapper repository, 
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllPerson()
        {
            try
            {
                var persons = _repository.Persons.GetAllPersons();

                var personsResult = _mapper.Map<IEnumerable<PersonDtoRead>>(persons);

                return Ok(personsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("page")]
        public IActionResult GetPagePerson([FromQuery] PersonParameters parameters)
        {
            try
            {
                var persons = _repository.Persons.GetPagePersons(parameters);

                Response.Headers.Add("X-Pagination", persons.Meta);

                var personsResult = _mapper.Map<IEnumerable<PersonDtoRead>>(persons);

                return Ok(personsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetPersonById")]
        public IActionResult GetPersonById(int id)
        {
            try
            {
                var Person = _repository.Persons.GetPersonById(id);

                if (Person is null)
                    return NotFound();
                else
                    return Ok(_mapper.Map<PersonDtoRead>(Person));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonDtoCreateUpdate person)
        {
            try
            {
                if (person == null) 
                    return BadRequest();
                if (!ModelState.IsValid) 
                    return BadRequest(ModelState);

                var personEntity = _mapper.Map<Person>(person);
                _repository.Persons.CreatePerson(personEntity);
                _repository.Save();

                var personDto = _mapper.Map<PersonDtoRead>(personEntity);

                return CreatedAtRoute("GetPersonById", new { id = personDto.Id }, personDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] PersonDtoCreateUpdate person)
        {
            try
            {
                if (person == null)
                    return BadRequest();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var personEntity = _repository.Persons.GetPersonById(id);

                if (personEntity is null)
                    return NotFound();
                
                _mapper.Map(person, personEntity);
                
                _repository.Persons.UpdatePerson(personEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            try
            {
                var person = _repository.Persons.GetPersonById(id);
                
                if (person == null)
                    return NotFound();
                
                _repository.Persons.DeletePerson(person);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
