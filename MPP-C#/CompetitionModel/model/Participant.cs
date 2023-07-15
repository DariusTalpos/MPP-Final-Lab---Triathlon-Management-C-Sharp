using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionModel.model
{
    public class Participant: Entity<long>
    {
        public string Name { get; set; }
        public int FullPoints { get; set; }

        public Participant(string name, int fullPoints)
        {
            this.Name = name;
            this.FullPoints = fullPoints;
        }

        public override string ToString()
        {
            //return "Id: "+this.Id +"; Name: "+ Name.ToString() + "; Full points: "+ FullPoints.ToString();
            return Name.ToString();
        }
    }
}
