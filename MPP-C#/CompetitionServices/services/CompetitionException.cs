using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionServices.services
{
    public class CompetitionException: Exception
    {
        public CompetitionException()
        {
        }

        public CompetitionException(String message): base(message) { }
    }
}
