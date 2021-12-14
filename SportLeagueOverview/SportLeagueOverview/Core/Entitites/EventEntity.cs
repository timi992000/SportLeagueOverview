namespace SportLeagueOverview.Core.Entitites
{
  public class EventEntity : EntityBase
  {
    public EventEntity() :
      base("Ereignis", "EreignisId")
    {
    }

    public int EventId { get; set; }
    public int TeamId { get; set; }
    public int EventType { get; set; }
    public int Minute { get; set; }
    public int PlayerId { get; set; }
  }
}
