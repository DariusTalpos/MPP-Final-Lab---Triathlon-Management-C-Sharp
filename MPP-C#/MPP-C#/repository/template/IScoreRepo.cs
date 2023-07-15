using MPP_C_.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.repository.template
{
    internal interface IScoreRepo : IGenericRepo<long, Score>
    {
        public List<Score> FindAllWithPointsInRound(long roundID);
    }
}
