using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SportLeagueOverview.ViewModels
{
  public class EventViewModel : ViewModelBase<EventEntity>
  {
    private TeamEntity m_SelectedTeam;

    public EventViewModel(MatchViewModel Parent, EventEntity Event, bool IsHomeTeam)
    {
      Event.MatchId = Parent.CurrentItem.MatchId;
      CurrentItem = Event;
      this.Parent = Parent;
      this.IsHomeTeam = IsHomeTeam;
      PlayersForTeam = new ObservableCollection<PersonEntity>();
      if (Event.TeamId != 0)
      {
        SelectedTeam = Teams.FirstOrDefault(x => x.TeamId == Event.TeamId);
      }
      __RefreshPlayer();
      OnPropertyChanged(nameof(IsHomeTeam));
      OnPropertyChanged(nameof(IsAwayTeam));
    }

    public MatchViewModel Parent
    {
      get; set;
    }

    public string PlayerName
    {
      get { return CurrentItem.Person.PlayerName; }
      set { CurrentItem.Person.PlayerName = value; OnPropertyChanged(nameof(PlayerName)); }
    }
    public EventType EventType
    {
      get { return CurrentItem.EventType; }
      set { CurrentItem.EventType = value; OnPropertyChanged(nameof(EventType)); }
    }
    public int Minute
    {
      get { return CurrentItem.Minute; }
      set { CurrentItem.Minute = value; OnPropertyChanged(nameof(Minute)); }
    }

    public bool IsHomeTeam { get; set; }
    public bool IsAwayTeam { get => !IsHomeTeam; }

    public TeamEntity SelectedTeam
    {
      get
      {
        return m_SelectedTeam;
      }
      set
      {
        m_SelectedTeam = value;
        CurrentItem.TeamId = value.TeamId;
        OnPropertyChanged(nameof(SelectedTeam));
        __RefreshPlayer();
        OnPropertyChanged(nameof(PlayersForTeam));
      }
    }

    public ObservableCollection<TeamEntity> Teams
    {
      get => new ObservableCollection<TeamEntity>() { Parent.CurrentItem.AwayTeam, Parent.CurrentItem.HomeTeam };
    }

    public string SelectedEventType
    {
      get
      {
        return __ConvertEventTypeToString(CurrentItem.EventType);
      }
      set
      {
        CurrentItem.EventType = __ConvertStringToEventType(value);
      }
    }


    public List<string> EventTypes
    {
      get => new List<string>()
            {
                "Gelbe Karte",
                "Rote Karte",
                "Tor",
                "Eigentor",
            };
    }

    public bool IsTeamSelected { get => SelectedTeam != null; }
    public ObservableCollection<PersonEntity> PlayersForTeam { get; set; }

    public PersonEntity Player
    {
      get
      {
        return CurrentItem.Person;
      }
      set
      {
        CurrentItem.Person = value;
        OnPropertyChanged(nameof(Player));
      }
    }

    public new void Execute_Save(object sender)
    {
      base.Execute_Save(sender);
      IsHomeTeam = CurrentItem.TeamId == Parent.CurrentItem.HomeTeam.TeamId;
      Parent.ReloadEvents();
    }

    private void __RefreshPlayer()
    {
      if (SelectedTeam != null)
      {
        var Player = DBAccess.ReadEntity<PersonEntity>().Where(x => x.TeamId == SelectedTeam.TeamId).ToList();
        PlayersForTeam.Clear();
        foreach (var player in Player)
        {
          PlayersForTeam.Add(player);
        }
      }
    }

    private EventType __ConvertStringToEventType(string value)
    {
      switch (value)
      {
        case "Gelbe Karte":
          return EventType.YellowCard;
        case "Rote Karte":
          return EventType.RedCard;
        case "Tor":
          return EventType.Goal;
        case "Eigentor":
          return EventType.OwnGoal;
        default:
          return default(EventType);
      }
    }

    private string __ConvertEventTypeToString(EventType value)
    {
      switch (value)
      {
        case EventType.YellowCard:
          return "Gelbe Karte";
        case EventType.RedCard:
          return "Rote Karte";
        case EventType.Goal:
          return "Tor";
        case EventType.OwnGoal:
          return "Eigentor";
        default:
          return default(string);
      }
    }
  }
}
