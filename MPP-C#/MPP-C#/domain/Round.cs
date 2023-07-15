using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.domain
{
    internal class Round: Entity<long>
    {
        public string Name { get; set; }
        public Round(string name)
        {
            this.Name = name;
        }

        public override string ToString() 
        {
            return "Id: "+Id.ToString()+"; Name: "+Name.ToString();
        }
    }
}
