using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_Persons.Model
{
    [Table("users")]
    public class User
    {
        public long? ID { get; set; }
        public string Login { get; set; }
        public string AccessKey { get; set; }
    }
}
