namespace SportLeagueOverview.Core.Entitites
{
  public class AdressEntity : EntityBase
  {
    public AdressEntity()
      : base("Adresse", "AdressId")
    {
    }

    public int AdressId { get; set; }
    public string Straße { get; set; }
    public string AdressZusatz { get; set; }
    public int PLZ { get; set; }
    public string Stadt { get; set; }
  }
}
