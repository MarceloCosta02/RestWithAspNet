﻿using WebApi_Persons.Model;
using System.Collections.Generic;
using WebApi_Persons.Data.VO;

namespace WebApi_Persons.Business
{
    public interface ILoginBusiness
    {
        object FindByLogin(UserVO user);
    }
}
