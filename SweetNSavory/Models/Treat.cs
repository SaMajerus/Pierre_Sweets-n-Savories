using System.Collections.Generic;
using System;

namespace SweetNSavory.Models
{
  public class Treat
  {
    public Treat()
    {
      this.JoinTreFlav = new HashSet<TreatFlavor>();
      //this.JoinTreOrd = new HashSet<TreatOrder>();
    }

    public int TreatId { get; set; }
    public string Description { get; set; }
    //public int Rating { get; set; }
    //public string Steps { get; set; }
    public virtual ApplicationUser User { get; set; }  //Declared as 'virtual' to allow Entity to lazy-load the property's contents. 

    public virtual ICollection<TreatFlavor> JoinTreFlav { get;}
    //public virtual ICollection<TreatOrder> JoinTreOrd { get;}
  }
}