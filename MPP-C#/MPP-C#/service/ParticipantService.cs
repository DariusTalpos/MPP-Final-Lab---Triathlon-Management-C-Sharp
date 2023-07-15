using MPP_C_.domain;
using MPP_C_.repository.template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.service
{
    internal class ParticipantService
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
