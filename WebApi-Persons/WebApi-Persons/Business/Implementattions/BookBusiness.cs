using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Persons.Business.Implementattions;
using WebApi_Persons.Model;
using WebApi_Persons.Repository.Generic;

namespace WebApi_Persons.Business
{
    public class BookBusiness : IBookBusiness
    {
        private IRepository<Book> _repository;

        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }
    }

}
