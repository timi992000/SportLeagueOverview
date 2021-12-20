using SportLeagueOverview.Core.Attributes;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
      State = MatchState.Open;
    }
    [ColumnName("SpielId")]
    public int MatchId { get; set; }

    [ColumnName("Status")]
    public int MatchStateId
    {
      get
      {
        return Convert.ToInt32(State);
      }
      set
      {
        State = (MatchState)value;
      }
    }
    [IsDataColumn(false)]
    public MatchState State { get; set; }

    [ColumnName("Anpfiff")]
    public DateTime Kickoff { get; set; }

    [IsDataColumn(false)]
    public TeamEntity HomeTeam { get; set; }
    [ColumnName("HeimMannId")]
    public int HomeTeamId
    {
      get
      {
        return HomeTeam.TeamId;
      }
      set
      {
        HomeTeam = DBAccess.ReadEntity<TeamEntity>(new List<int> { value }, true)[0];
      }
    }

    [IsDataColumn(false)]
    public AdressEntity Venue { get; set; }

    [IsDataColumn(false)]
    public TeamEntity AwayTeam { get; set; }

    [ColumnName("AuswaertsMannId")]
    public int AwayTeamId
    {
      get
      {
        return AwayTeam.TeamId;
      }
      set
      {
        AwayTeam = DBAccess.ReadEntity<TeamEntity>(new List<int> { value }, true)[0];
      }
    }

    [DisplayName("Adress Id")]
    [ColumnName("AdressId")]
    public int AdressId { get; set; }

    public void ChangeStatus(MatchState NewState)
    {
      if (State != NewState)
      {
        State = NewState;
      }
    }
  }
}
