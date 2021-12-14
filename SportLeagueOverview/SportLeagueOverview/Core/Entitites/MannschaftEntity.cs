using SportLeagueOverview.Core.Attributes;
using System;
using System.ComponentModel;

namespace SportLeagueOverview.Core.Entitites
{
  public class MannschaftEntity : EntityBase
  {
    public MannschaftEntity()
      : base("Mannschaft", "MannschaftId")
    {
    }

    [ColumnName("MannschaftId")]
    [DisplayName("Mannschaftsnummer")]
    public int MannschaftId { get; set; }

    [ColumnName("Name")]
    [DisplayName("Mannschaftsname")]
    public string Name { get; set; }

    [ColumnName("Gruendungsjahr")]
    [DisplayName("Gründung")]
    public int Gruendungsjahr { get; set; }

    [ColumnName("Wappen")]
    [DisplayName("Wappen")]
    public string Wappen { get; set; }

    [ColumnName("TrainerId")]
    public int TrainerId { get; set; }
  }
}
