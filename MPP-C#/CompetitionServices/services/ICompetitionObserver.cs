using CompetitionModel.model;

namespace CompetitionServices.services
{
    public interface ICompetitionObserver
    {
        void newRound();

        void newScore(Score score);
    }
}
