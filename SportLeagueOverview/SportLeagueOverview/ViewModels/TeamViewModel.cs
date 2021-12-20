using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SportLeagueOverview.ViewModels
{
  public class TeamViewModel : ViewModelBase<TeamEntity>
  {
    private List<PersonEntity> m_Trainers;

    public TeamViewModel()
    {
      __RefreshTrainer();
      __AttachEvents();
      Adress = new AdressEntity();
    }

    public string TeamName
    {
      get => CurrentItem.TeamName;
      set
      {
        CurrentItem.TeamName = value;
        OnPropertyChanged(nameof(CurrentItem.TeamName));
      }
    }

    public ImageSource CoatOfArms
    {
      get => DeserializeImage(CurrentItem.CoatOfArms);
      set
      {
        CurrentItem.CoatOfArms = SerializeImage(value);
        OnPropertyChanged(nameof(CurrentItem.CoatOfArms));
      }
    }

    public int FoundingYear
    {
      get
      {
        if (CurrentItem.FoundingYear == 0)
          CurrentItem.FoundingYear = DateTime.Now.Year;
        return CurrentItem.FoundingYear;
      }
      set
      {
        CurrentItem.FoundingYear = value;
        OnPropertyChanged(nameof(CurrentItem.FoundingYear));
      }
    }

    public List<PersonEntity> Trainers
    {
      get
      {
        return m_Trainers;
      }
    }


    public PersonEntity Coach
    {
      get => Trainers.FirstOrDefault(x => x.PersonId == CurrentItem.CoachId);
      set
      {
        if (value == null)
          return;
        CurrentItem.CoachId = value.PersonId;
        OnPropertyChanged(nameof(Coach));
        OnPropertyChanged(nameof(CurrentItem.CoachId));
      }
    }

    private void __AttachEvents()
    {
      ImageSelected += (sender, e) =>
      {
        CoatOfArms = (ImageSource)sender;
      };
      EntitySpectator.SaveCompleted += (sender, EventArgs) =>
      {
        __RefreshTrainer();
      };
      EntitySpectator.DeleteCompleted += (sender, EventArgs) =>
      {
        __RefreshTrainer();
      };
    }

    private void __RefreshTrainer()
    {
      m_Trainers = DBAccess.ReadEntity<PersonEntity>().Where(x => x.IsCoach).ToList();
      OnPropertyChanged(nameof(Trainers));
      OnPropertyChanged(nameof(Coach));
    }
  }
}
