using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApi_Persons.Data.VO
{
    // Com o DataContract, eu posso ordenar e renomear os campos
    // para aparecerem de maneira personalizada na hora da requisição
    [DataContract]
    public class BookVO
    {
        [DataMember(Order = 1, Name = "codigo")]
        public long? Id { get; set; }

        [DataMember(Order = 2)]
        public string Title { get; set; }

        [DataMember(Order = 3)]
        public string Author { get; set; }

        [DataMember(Order = 5)]
        public decimal Price { get; set; }

        [DataMember(Order = 4)]
        public DateTime LaunchDate { get; set; }
    }
}
