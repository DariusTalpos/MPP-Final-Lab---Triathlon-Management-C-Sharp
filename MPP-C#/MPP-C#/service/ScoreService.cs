using MPP_C_.domain;
using MPP_C_.repository.template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.service
{
    internal class ScoreService
    {
        private IScoreRepo scoreRepo;

        public ScoreService(IScoreRepo scoreRepo)
        {
            this.scoreRepo = scoreRepo;
        }

        public List<Score> getScoreListFromRound(long roundID) { return scoreRepo.FindAllWithPointsInRound(roundID); }

        public int save(Round round, Participant participant, int points)
        {
            Score score = new Score(participant, round, points);
            if (scoreRepo.Save(score) != null)
                return 1;
            return 0;
        }
    }
}
