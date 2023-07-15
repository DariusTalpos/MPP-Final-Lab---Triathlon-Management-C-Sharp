using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionNetworking.networking.dto
{
    [Serializable]
    public class ParticipantDTO
    {
        public String Name { get; set; }
        public int fullPoints { get; set; }
        public long id { get; set; }
        public ParticipantDTO(String name, int fullPoints, long id)
        {
            this.Name = name;
            this.fullPoints = fullPoints;
            this.id = id;
        }
    }
}
