using WebApi_Persons.Model;
using System.Collections.Generic;
using WebApi_Persons.Data.VO;

namespace WebApi_Persons.Repository
{
    public interface IUserRepository
    {
        User FindByLogin(string login);

    }
}
