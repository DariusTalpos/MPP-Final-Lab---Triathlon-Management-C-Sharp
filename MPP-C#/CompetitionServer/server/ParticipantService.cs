using CompetitionModel.model;
using CompetitionPersistence.template;

namespace CompetitionServer.server
{
    public class ParticipantService
    {
        private IParticipantRepo participantRepo;

        public ParticipantService(IParticipantRepo participantRepo)
        {
            this.participantRepo = participantRepo;
        }

        public List<Participant> getParticipantList() { return participantRepo.FindAll(); }

        public int updatePoints(Participant participant, int points)
        {
            participant.FullPoints += points;
            if (participantRepo.Update(participant) != null)
                return 1;
            return 0;
        }
    }
}
