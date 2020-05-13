using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_Persons.Data.Converter
{
    // Recebe como parâmetro, 2 objetos, um de origem, e um de destino
    public interface IParser<O, D>
    {
        D Parse(O origin);
        List<D> ParseList(List<O> origin);
    }
}
