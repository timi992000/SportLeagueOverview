namespace SportLeagueOverview.Core.Entitites
{
  public class EreignisEntity : EntityBase
  {
    public EreignisEntity() :
      base("Ereignis")
    {
    }

    public int EreignisId { get; set; }
    public int MannId { get; set; }
    public int EreignisTyp { get; set; }
    public int Minute { get; set; }
    public int SpielerId { get; set; }
  }
}
