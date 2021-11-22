using System.ComponentModel;

namespace SportLeagueOverview.Core.Entitites
{
  public class MannschaftEntity : EntityBase
  {
    public MannschaftEntity()
      : base("Mannschaft")
    {
    }

    [DisplayName("Mannschaftsnummer")]
    public int MannschaftId { get; set; }
    [DisplayName("Mannschaftsname")]
    public string Name { get; set; }
    [DisplayName("Gründung")]
    public int Gruendungsjahr { get; set; }
    [DisplayName("Wappen")]
    public string Wappen { get; set; }
    public int TrainerId { get; set; }
  }
}
