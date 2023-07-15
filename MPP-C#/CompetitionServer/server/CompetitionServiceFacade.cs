using CompetitionModel.model;
using CompetitionPersistence.repos;
using CompetitionServices.services;
using System.Collections.Concurrent;

namespace CompetitionServer.server
{
    public class CompetitionServiceFacade : ICompetitionServices
    {
        private UserService userService;
        private ParticipantService participantService;
        private RoundService roundService;
        private ScoreService scoreService;
        private ConcurrentDictionary<string, ICompetitionObserver> loggedUsers;
        private int defaultThreadsNo = 5;

        public CompetitionServiceFacade(UserDBRepo userDBRepo,ParticipantDBRepo participantDBRepo,RoundDBRepo roundDBRepo,ScoreDBRepo scoreDBRepo)
        {
            userService = new UserService(userDBRepo);
            participantService = new ParticipantService(participantDBRepo);
            roundService = new RoundService(roundDBRepo);
            scoreService = new ScoreService(scoreDBRepo);
            loggedUsers = new ConcurrentDictionary<string, ICompetitionObserver>();
        }
        public void addRoundScore(string roundName, Participant participant, int points)
        {
            Round round = roundService.getRoundWithName(roundName);
            if (round == null) 
            {
                roundService.save(roundName);
                round = roundService.getRoundWithName(roundName);
                foreach(var value in loggedUsers.Values)
                { 
                    try
                    {
                        value.newRound();
                    }
                    catch (CompetitionException e)
                    {
                        throw new CompetitionException("");
                    }
                }
            }
            int ok = scoreService.save(round, participant, points);
            participantService.updatePoints(participant, points);
            if (ok == 0)
            {
                foreach (var value in loggedUsers.Values)
                {
                    try
                    {
                        value.newScore(new Score(participant, round, points));
                    }
                    catch (CompetitionException e)
                    {
                        throw new CompetitionException("");
                    }
                }
            }
        }

        public List<Participant> getParticipantList()
        {
            return participantService.getParticipantList();
        }

        public List<Round> getRoundList()
        {
            return roundService.getRoundList();
        }

        public List<Score> getScoreListFromRound(string roundName)
        {
            return scoreService.getScoreListFromRound(roundName);
        }

        public void login(User user, ICompetitionObserver client)
        {
            User result = userService.userExists(user.Username, user.Password);
            if (result != null)
            {
                if (loggedUsers.TryAdd(user.Username, client) == false)
                    throw new CompetitionException("User is already logged in.");
            }
            else
                throw new CompetitionException("There is no account with this username and password");
        }

        public void logout(User user, ICompetitionObserver client)
        {
            if (!loggedUsers.TryRemove(user.Username,out _))
                throw new CompetitionException("User " + user.Username + " is not logged in.");
        }
    }
}
