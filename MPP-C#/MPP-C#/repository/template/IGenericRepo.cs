using MPP_C_.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.repository.template
{
    internal interface IGenericRepo<ID, E> where E : Entity<ID>
    {
        E Save(E entity);
        E Update(E entity);
        E Delete(ID id);
        E FindOne(ID id);
        List<E> FindAll();
    }
}
