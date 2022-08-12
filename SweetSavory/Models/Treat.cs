using System.Collections.Generic;

namespace SweetSavory.Models
{
    public class Treat
    {
        // public Treat()
        // {
        //     this.JoinEntities = new HashSet<FlavorTreat>();
        // }

        public int TreadId { get; set; }
        public string Name { get; set; }
        // public virtual ICollection<FlavorTreat> JoinEntities { get; set; }
    }
}