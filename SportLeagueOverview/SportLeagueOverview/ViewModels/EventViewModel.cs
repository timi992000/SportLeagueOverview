using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SportLeagueOverview.ViewModels
{
  public class EventViewModel : ViewModelBase<EventEntity>
  {
    public EventViewModel(MatchViewModel Parent, EventEntity Event, bool IsHomeTeam)
    {
      CurrentItem = Event;
      this.Parent = Parent;
      this.IsHomeTeam = IsHomeTeam;
      if (Event.TeamId != 0)
      {
        SelectedTeam = Teams.FirstOrDefault(x => x.TeamId == Event.TeamId);
      }
      PlayersForTeam = new ObservableCollection<PersonEntity>();
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
      get { return CurrentItem.Player.PlayerName; }
      set { CurrentItem.Player.PlayerName = value; OnPropertyChanged(nameof(PlayerName)); }
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
      get; set;
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
        return CurrentItem.Player;
      }
      set
      {
        CurrentItem.Player = value;
        OnPropertyChanged(nameof(Player));
      }
    }

    public new void Execute_Save(object sender)
    {
      base.Execute_Save(sender);
      IsHomeTeam = CurrentItem.TeamId == Parent.CurrentItem.HomeTeam.TeamId;
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
        OnPropertyChanged(nameof(Teams));
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
