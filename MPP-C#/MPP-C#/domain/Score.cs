using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.domain
{
    internal class Score: Entity<long>
    {
        public Participant Participant { get; set; }
        public Round Round { get; set; }
        public int Points { get; set; }

        public Score(Participant participant, Round round, int points)
        {
            Participant = participant;
            Round = round;
            Points = points;
        }

        public override string ToString() 
        {
            return "Round: " + Round.ToString() + "/ Participant: " + Participant.ToString() + "/ Points: " + Points.ToString();
        }
    }
}
