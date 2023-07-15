using CompetitionModel.model;
using CompetitionPersistence.template;

namespace CompetitionServer.server
{
    public class ScoreService
    {
        private IScoreRepo scoreRepo;

        public ScoreService(IScoreRepo scoreRepo)
        {
            this.scoreRepo = scoreRepo;
        }

        public List<Score> getScoreListFromRound(string roundName) { return scoreRepo.FindAllWithPointsInRound(roundName); }

        public int save(Round round, Participant participant, int points)
        {
            Score score = new Score(participant, round, points);
            if (scoreRepo.Save(score) != null)
                return 1;
            return 0;
        }
    }
}
