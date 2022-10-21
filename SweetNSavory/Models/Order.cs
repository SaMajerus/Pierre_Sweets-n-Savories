using System.Collections.Generic;
using System;  //This is present in Treat.cs, but not Flavor.cs

namespace SweetNSavory.Models
{
  public class Order
  {
    public Order()
    {
      this.JoinTreOrd = new HashSet<TreatOrder>();
    }

    public int OrderId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<TreatOrder> JoinTreOrd { get; set; }
  }
}