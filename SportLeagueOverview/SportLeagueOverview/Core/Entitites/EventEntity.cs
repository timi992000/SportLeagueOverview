namespace SportLeagueOverview.Core.Entitites
{
  public class EventEntity : EntityBase
  {
    public EventEntity() :
      base("Ereignis", "EreignisId")
    {
    }

    public int EreignisId { get; set; }
    public int MannId { get; set; }
    public int EreignisTyp { get; set; }
    public int Minute { get; set; }
    public int SpielerId { get; set; }
  }
}
