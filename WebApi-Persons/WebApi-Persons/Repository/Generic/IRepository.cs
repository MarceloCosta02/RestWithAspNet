using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Persons.Model.Base;

namespace WebApi_Persons.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);

        bool Exists(long? id);

        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);

    }
}
