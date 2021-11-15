using System.ComponentModel;

namespace SportLeagueOverview.Core.Entitites
{
  public class MannschaftEntity : EntityBase
  {
    public MannschaftEntity()
      : base("Mannschaft")
    {
    }

    [DisplayName("MannschaftsId")]
    public int MannschaftId { get; set; }
    public string Name { get; set; }
    public int Gruendungsjahr { get; set; }
    public string Wappen { get; set; }
    public int TrainerId { get; set; }
  }
}
