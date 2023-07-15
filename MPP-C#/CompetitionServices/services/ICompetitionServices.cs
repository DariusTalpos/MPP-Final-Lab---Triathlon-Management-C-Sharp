using CompetitionModel.model;

namespace CompetitionServices.services
{
    public interface ICompetitionServices
    {
        void login(User user, ICompetitionObserver client);
        void logout(User user, ICompetitionObserver client);
        List<Participant> getParticipantList();
        List<Round> getRoundList();
        void addRoundScore(String roundName, Participant participant, int points);
        List<Score> getScoreListFromRound(string roundName);
    }
}
