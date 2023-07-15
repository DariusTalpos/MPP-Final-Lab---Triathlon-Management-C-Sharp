using CompetitionModel.model;
using CompetitionPersistence.template;

namespace CompetitionServer.server
{
    public class RoundService
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
