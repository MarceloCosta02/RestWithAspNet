﻿using Microsoft.AspNetCore.Mvc;
using WebApi_Persons.Model;
using WebApi_Persons.Business;
using WebApi_Persons.Data.VO;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace WebApi_Persons.Controllers
{

    /* Mapeia as requisições de http://localhost:{porta}/api/person/
    Por padrão o ASP.NET Core mapeia todas as classes que extendem Controller
    pegando a primeira parte do nome da classe em lower case [Person]Controller
    e expõe como endpoint REST
    */

    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    // Adicionado o versionameto da api
    public class PersonsController : Controller
    {
        //Declaração do business usado
        private IPersonBusiness _personBusiness;

        /* Injeção de uma instancia de IPersonBusiness ao criar
        uma instancia de PersonController */
        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/person/
        //Get sem parâmetros para o FindAll --> Busca Todos

        [HttpGet]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        // Autorização com o TOKEN JWT
        //[Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("find-by-name")]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        // Autorização com o TOKEN JWT
        [Authorize("Bearer")]
        public IActionResult GetByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            return new OkObjectResult(_personBusiness.FindByName(firstName, lastName));
        }

        [HttpGet("find-with-paged-search/{sortDirection}/{pageSize}/{page}")]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        // Autorização com o TOKEN JWT
        [Authorize("Bearer")]
        public IActionResult GetPagedSearch([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            return new OkObjectResult(_personBusiness.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/person/{id}
        //recebendo um ID como no Path da requisição
        //Get com parâmetros para o FindById --> Busca Por ID

        [HttpGet("{id}")]
        [SwaggerResponse((200), Type = typeof(PersonVO))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        // Autorização com o TOKEN JWT
        [Authorize("Bearer")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        //Mapeia as requisições POST para http://localhost:{porta}/api/person/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição

        [HttpPost]
        [SwaggerResponse((201), Type = typeof(PersonVO))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        // Autorização com o TOKEN JWT
        [Authorize("Bearer")]
        public IActionResult Post([FromBody]PersonVO person)
        {
            if (person == null) return BadRequest();
            return new  ObjectResult(_personBusiness.Create(person));
        }

        //Mapeia as requisições PUT para http://localhost:{porta}/api/person/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição

        [HttpPut]
        [SwaggerResponse((202), Type = typeof(List<PersonVO>))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        // Autorização com o TOKEN JWT
        [Authorize("Bearer")]
        public IActionResult Put([FromBody]PersonVO person)
        {
            if (person == null)
                return BadRequest();

            var updatePerson = _personBusiness.Update(person);

            if (updatePerson == null)
                return NoContent();

            return new ObjectResult(updatePerson);
        }


        //Mapeia as requisições DELETE para http://localhost:{porta}/api/person/{id}
        //recebendo um ID como no Path da requisição

        [HttpDelete("{id}")]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        // Autorização com o TOKEN JWT
        [Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
