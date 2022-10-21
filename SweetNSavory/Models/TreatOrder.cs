namespace SweetNSavory.Models
{
  public class TreatOrder
  {
    public int TreatOrderId { get; set; }
    public int OrderId { get; set; }
    public int TreatId { get; set; }
    public virtual Order Order { get; set; }
    public virtual Treat Treat { get; set; }
  }
}