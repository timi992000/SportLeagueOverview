using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using SportLeagueOverview.Core.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace SportLeagueOverview.ViewModels
{
  public class MatchViewModel : ViewModelBase<MatchEntity>
  {
    private List<TeamEntity> m_Teams;
    ObservableCollection<EventViewModel> m_Events;
    private int m_HomeGoals;
    private int m_AwayGoals;

    public MatchViewModel(bool IsNew = false)
    {
      CurrentItem.IsNew = IsNew;
      Events = new ObservableCollection<EventViewModel>();
      __RefreshTeams();
      RefreshEvents();
    }

    public bool IsNewMatch { get; set; }

    public string HomeTeamName
    {
      get
      {
        return CurrentItem?.HomeTeam?.TeamName;
      }
    }

    public string AwayTeamName
    {
      get
      {
        return CurrentItem?.AwayTeam?.TeamName;
      }
    }

    public int AwayGoals
    {
      get
      {
        return m_AwayGoals;
      }
      set
      {
        m_AwayGoals = value;
        OnPropertyChanged(nameof(AwayGoals));
      }
    }

    public int HomeGoals
    {
      get
      {
        return m_HomeGoals;
      }
      set
      {
        m_HomeGoals = value;
        OnPropertyChanged(nameof(HomeGoals));
      }
    }

    public ImageSource AwayCoatOfArms
    {
      get => DeserializeImage(CurrentItem.AwayTeam.CoatOfArms);
    }

    public ImageSource HomeCoatOfArms
    {
      get => DeserializeImage(CurrentItem.HomeTeam.CoatOfArms);
    }

    public string DateString
    {
      get
      {
        return CurrentItem.Kickoff.ToString("dd/mm/yyyy HH:MM");
      }
    }

    public DateTime? Date
    {
      get
      {
        return CurrentItem?.Kickoff;
      }
      set
      {
        var KickOffDate = value;
        if (KickOffDate.HasValue)
        {
          CurrentItem.Kickoff = KickOffDate.Value;
        }
        OnPropertyChanged(nameof(Date));
      }
    }

    public string Street
    {
      get
      {
        return CurrentItem?.Venue.Street;
      }
      set
      {
        CurrentItem.Venue.Street = value;
        OnPropertyChanged(nameof(Street));
      }
    }
    public string City
    {
      get
      {
        return CurrentItem?.Venue.City;
      }
      set
      {
        CurrentItem.Venue.City = value;
        OnPropertyChanged(nameof(City));
      }
    }

    public string AdressAddition
    {
      get
      {
        return CurrentItem?.Venue.AdressAddition;
      }
      set
      {
        CurrentItem.Venue.AdressAddition = value;
        OnPropertyChanged(nameof(AdressAddition));
      }
    }

    public int ZipCode
    {
      get
      {
        return CurrentItem.Venue.ZipCode;
      }
      set
      {
        CurrentItem.Venue.ZipCode = value;
        OnPropertyChanged(nameof(ZipCode));
      }
    }

    public MatchState State
    {
      get => CurrentItem.State;
      set
      {
        CurrentItem.ChangeStatus(value);
        OnPropertyChanged(nameof(State));
      }
    }

    public void ReloadEvents()
    {
      RefreshEvents();
      ReloadRequested += (sender, e) =>
      {
        RefreshEvents();
      };
    }

    public MatchState[] StateValues => (MatchState[])Enum.GetValues(typeof(MatchState));

    public bool IsNotDone
    {
      get => State != MatchState.Done;
    }


    public List<TeamEntity> Teams
    {
      get
      {
        return m_Teams;
      }
    }

    public string Place
    {
      get => CurrentItem?.Venue?.City;
    }

    public TeamEntity HomeTeam
    {
      get
      {
        return CurrentItem?.HomeTeam;
      }
      set
      {
        CurrentItem.HomeTeam = value;
        OnCurrentItemChanged();
      }
    }

    public TeamEntity AwayTeam
    {
      get
      {
        return CurrentItem?.AwayTeam;
      }
      set
      {
        CurrentItem.AwayTeam = value;
        OnCurrentItemChanged();
      }
    }

    public ObservableCollection<EventViewModel> Events
    {
      get
      {
        return m_Events;
      }
      set
      {
        m_Events = value;
        OnPropertyChanged(nameof(Events));
      }
    }

    public override void Execute_New(object sender)
    {
      //CurrentItem = new MatchEntity { IsNew = true };
      var Window = new EventDetailWindow(new EventViewModel(this, new EventEntity() { IsNew = true, MatchId = CurrentItem.MatchId}, false) { IsNew = true });
      Window.ShowDialog();
      this.Reload.Execute(null);
    }

    public void RefreshEvents()
    {
      var EventEntities = DBAccess.ReadEntity<EventEntity>().Where(x => x.MatchId == CurrentItem.MatchId);
      EventEntities = EventEntities.OrderBy(x => x.Minute).ToList();
      foreach (var Event in EventEntities)
      {
        Events.Add(new EventViewModel(this, Event, Event.TeamId == CurrentItem.HomeTeam.TeamId));
      }
      OnPropertyChanged(nameof(Events));
      __RefreshGoals();
    }

    private void __RefreshGoals()
    {
      HomeGoals = Events.Where(x => x.IsHomeTeam && x.EventType == EventType.Goal).Count();
      HomeGoals += Events.Where(x => x.IsAwayTeam && x.EventType == EventType.OwnGoal).Count();
      AwayGoals = Events.Where(x => x.IsAwayTeam && x.EventType == EventType.Goal).Count();
      AwayGoals += Events.Where(x => x.IsHomeTeam && x.EventType == EventType.OwnGoal).Count();
    }


    private void __RefreshTeams()
    {
      m_Teams = DBAccess.ReadEntity<TeamEntity>();
      OnPropertyChanged(nameof(Teams));
    }
  }
}
