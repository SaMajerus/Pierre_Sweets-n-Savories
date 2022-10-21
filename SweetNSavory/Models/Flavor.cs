using System.Collections.Generic;

namespace SweetNSavory.Models
{
  public class Flavor
  {
    public Flavor()
    {
      this.JoinTreFlav = new HashSet<TreatFlavor>();
    }

    public int FlavorId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<TreatFlavor> JoinTreFlav { get; set; }
  }
}