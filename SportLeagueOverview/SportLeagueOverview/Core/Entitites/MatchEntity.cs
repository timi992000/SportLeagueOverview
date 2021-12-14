using System;

namespace SportLeagueOverview.Core.Entitites
{
  public class MatchEntity : EntityBase
  {
    public MatchEntity()
  : base("Spiel", "SpielId")
    {

    }
    public int SpielId { get; set; }
    public MatchState Status { get; set; }
    public DateTime Anpfiff { get; set; }
    public TeamEntity HeimMannschaft { get; set; }
    public TeamEntity AuswärtsMannschaft { get; set; }
    public AdressEntity AustragungsOrt { get; set; }

    public void ÄndereStatus()
    {
    }
  }
}
