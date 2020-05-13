using WebApi_Persons.Model;
using System.Collections.Generic;
using WebApi_Persons.Data.VO;

namespace WebApi_Persons.Repository
{
    public interface IPersonRepository
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(long id);

        bool Exists(long? id);
        
    }
}
