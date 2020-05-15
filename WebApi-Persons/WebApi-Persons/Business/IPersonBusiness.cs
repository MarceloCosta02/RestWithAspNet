using WebApi_Persons.Model;
using System.Collections.Generic;
using WebApi_Persons.Data.VO;
using Tapioca.HATEOAS.Utils;

namespace WebApi_Persons.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        List<PersonVO> FindByName(string firstName, string lastName);
        PersonVO Update(PersonVO person);
        void Delete(long id);
        PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

    }
}
