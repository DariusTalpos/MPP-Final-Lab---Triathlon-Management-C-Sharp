using MPP_C_.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.repository.template
{
    internal interface IRoundRepo: IGenericRepo<long,Round>
    {
        public Round findRoundWithName(String name);
    }
}
