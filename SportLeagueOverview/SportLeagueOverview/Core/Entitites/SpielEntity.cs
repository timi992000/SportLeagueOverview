using System;

namespace SportLeagueOverview.Core.Entitites
{
  public class SpielEntity : EntityBase
  {
    public SpielEntity()
  : base("Spiel", "SpielId")
    {

    }
    public int SpielId { get; set; }
    public SpielStatus Status { get; set; }
    public DateTime Anpfiff { get; set; }
    public MannschaftEntity HeimMannschaft { get; set; }
    public MannschaftEntity AuswärtsMannschaft { get; set; }
    public AdressEntity AustragungsOrt { get; set; }

    public void ÄndereStatus()
    {
    }
  }
}
