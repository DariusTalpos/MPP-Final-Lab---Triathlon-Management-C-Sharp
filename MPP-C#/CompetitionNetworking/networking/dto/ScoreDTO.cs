using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionNetworking.networking.dto
{
    [Serializable]
    public class ScoreDTO
    {
        public ParticipantDTO Participant { get; set;}
        public RoundDTO Round { get; set;}
        public int Points { get; set; }
        public long id { get; set;}

        public ScoreDTO(ParticipantDTO participant, RoundDTO round, int points, long id)
        {
            Participant = participant;
            Round = round;
            Points = points;
            this.id = id;
        }
    }
}
