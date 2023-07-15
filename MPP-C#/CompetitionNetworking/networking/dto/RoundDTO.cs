using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionNetworking.networking.dto
{
    [Serializable]
    public class RoundDTO
    {
        public String Name { get; set; }
        public long id { get; set; }

        public RoundDTO(string name, long id)
        {
            Name = name;
            this.id = id;
        }
    }
}
