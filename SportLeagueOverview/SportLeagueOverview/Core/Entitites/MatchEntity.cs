using System;

namespace SportLeagueOverview.Core.Entitites
{
  public class MatchEntity : EntityBase
  {
    public MatchEntity()
  : base("Spiel", "SpielId")
    {

    }
    public int GameId { get; set; }
    public MatchState State { get; set; }
    public DateTime Kickoff { get; set; }
    public TeamEntity HomeTeam { get; set; }
    public TeamEntity AwaTeam { get; set; }
    public AdressEntity Venue { get; set; }

    public void ChangeStatus(MatchState NewState)
    {
      if(State != NewState)
      {
        State = NewState;
      }
    }
  }
}
