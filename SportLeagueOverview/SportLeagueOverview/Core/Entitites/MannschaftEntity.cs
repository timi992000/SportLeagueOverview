namespace SportLeagueOverview.Core.Entitites
{
  public class MannschaftEntity : EntityBase
  {
    public MannschaftEntity()
    {
    }

    public int MannschaftId { get; set; }
    public string Name { get; set; }
    public string Gruendungsjahr { get; set; }
    public string Wappen { get; set; }
    public int TrainerId { get; set; }
  }
}
