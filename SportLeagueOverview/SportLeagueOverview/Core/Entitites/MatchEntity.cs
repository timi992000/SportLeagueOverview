using SportLeagueOverview.Core.Attributes;
using System;
using System.ComponentModel;

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
    public TeamEntity AwayTeam { get; set; }

    [DisplayName("Adress Id")]
    [ColumnName("AdressId")]
    public int AdressId { get; set; }

    public void ChangeStatus(MatchState NewState)
    {
      if(State != NewState)
      {
        State = NewState;
      }
    }
  }
}
