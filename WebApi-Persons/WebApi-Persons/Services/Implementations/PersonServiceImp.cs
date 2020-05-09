using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi_Persons.Model;

namespace WebApi_Persons.Services.Implementations
{
    public class PersonServiceImp : IPersonService
    {
        // Contador responsável por gerar um fake ID         
        private volatile int count;

        // Método responsável por criar uma nova pessoa
        public Person Create(Person person)
        {
            return person;
        }
                
        // Método responsável por retornar uma pessoa
        public Person FindById(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Marcelo",
                LastName = "Ponte",
                Address = "Osasco - São Paulo - Brasil",
                Gender = "Male"
            };
        }

        // Método responsácel por retornar todas as pessoas
        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }

            return persons;
        }

        // Método responsável por atualizar uma pessoa
        public Person Update(Person person)
        {
            return person;
        }

        // Responsável por deletar uma pessoa a partir do ID
        public void Delete(long id)
        {
        }

        // Retorna um Mock padrão com as informações de pessoa
        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name " + i,
                LastName = "Person Last Name " + i,
                Address = "Person Address " + i,
                Gender = "Male"
            };
        }

        // Incrementa o ID 
        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
