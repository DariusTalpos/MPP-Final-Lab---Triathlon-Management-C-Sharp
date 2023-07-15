using MPP_C_.domain;
using MPP_C_.repository.template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.service
{
    internal class RoundService
    {
        private IRoundRepo roundRepo;

        public RoundService(IRoundRepo roundRepo)
        {
            this.roundRepo = roundRepo;
        }

        public List<Round> getRoundList() { return roundRepo.FindAll(); }

        public Round getRoundWithName(String name) { return roundRepo.findRoundWithName(name); }

        public int save(String name)
        {
            Round round = new Round(name);
            if (roundRepo.Save(round) != null)
                return 1;
            return 0;
        }
    }
}
