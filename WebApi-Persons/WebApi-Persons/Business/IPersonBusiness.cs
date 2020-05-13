using WebApi_Persons.Model;
using System.Collections.Generic;
using WebApi_Persons.Data.VO;

namespace WebApi_Persons.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(long id);
    }
}
