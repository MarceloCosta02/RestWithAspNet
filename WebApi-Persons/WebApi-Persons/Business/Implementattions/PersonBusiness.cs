using System.Collections.Generic;
using WebApi_Persons.Model;
using System.Threading;
using WebApi_Persons.Model.Context;
using System;
using System.Linq;
using WebApi_Persons.Repository;
using WebApi_Persons.Repository.Generic;

namespace WebApi_Persons.Business.Implementattions
{
    public class PersonBusiness : IPersonBusiness
    {

        private IRepository<Person> _repository;

        public PersonBusiness(IRepository<Person> repository)
        {
            _repository = repository;
        }

        // Metodo responsável por criar uma nova pessoa    
        public Person Create(Person person)
        {            
            return _repository.Create(person);
        }

        // Método responsável por retornar uma pessoa
        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        // Método responsável por retornar todas as pessoas
        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        // Método responsável por atualizar uma pessoa
        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        // Método responsável por deletar
        // uma pessoa a partir de um ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
