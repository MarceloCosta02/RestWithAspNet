using Microsoft.AspNetCore.Mvc;
using WebApi_Persons.Model;
using WebApi_Persons.Controllers;
using WebApi_Persons.Services.Implementations;

namespace WebApi_Persons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET api/person
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // GET api/persons/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _personService.FindById(id);

            if (person == null)
                return NotFound();
            else
                return Ok(person);
        }

        // POST api/persons
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null)
                return BadRequest();
            else
                return new ObjectResult(_personService.Create(person));
        }

        // PUT api/persons/id
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null)
                return BadRequest();
            else
                return new ObjectResult(_personService.Update(person));
        }

        // DELETE api/persons/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
