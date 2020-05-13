using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Persons.Model.Base;

namespace WebApi_Persons.Model
{    
        [Table("books")]
        public class Book : BaseEntity
        {
            [Column("Title")]
            public string Title { get; set; }

            [Column("Author")]
            public string Author { get; set; }

            [Column("Price")]
            public decimal Price { get; set; }

            [Column("LaunchDate")]
            public DateTime LaunchDate { get; set; }
        }    
}
