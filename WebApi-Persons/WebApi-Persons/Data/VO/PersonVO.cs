﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tapioca.HATEOAS;
using WebApi_Persons.Model.Base;

namespace WebApi_Persons.Data.VO
{
    public class PersonVO 
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

    }
}
