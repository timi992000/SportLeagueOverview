using SportLeagueOverview.Core.Enums;
using System;

namespace SportLeagueOverview.Core.Entitites
{
  public class MatchEntity : EntityBase
  {
    public MatchEntity()
  : base("Spiel", "SpielId")
    {
      HomeTeam = new TeamEntity();
      AwayTeam = new TeamEntity();
      Venue = new AdressEntity();
    }
    public int MatchId { get; set; }
    public MatchState State { get; set; }
    public DateTime Kickoff { get; set; }
    public TeamEntity HomeTeam { get; set; }
    public TeamEntity AwayTeam { get; set; }
    public AdressEntity Venue { get; set; }

    public void ChangeStatus(MatchState NewState)
    {
      if (State != NewState)
      {
        State = NewState;
      }
    }
  }
}
