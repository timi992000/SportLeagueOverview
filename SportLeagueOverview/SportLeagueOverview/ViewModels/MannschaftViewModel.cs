using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SportLeagueOverview.ViewModels
{
  public class MannschaftViewModel : ViewModelBase<MannschaftEntity>
  {
    private List<PersonEntity> m_Trainers;

    public MannschaftViewModel()
    {
      __RefreshTrainer();
      __AttachEvents();
    }

    public string Name
    {
      get => CurrentItem.Name;
      set
      {
        CurrentItem.Name = value;
        OnPropertyChanged(nameof(CurrentItem.Name));
      }
    }

    public ImageSource Wappen
    {
      get => DeserializeImage(CurrentItem.Wappen);
      set
      {
        CurrentItem.Wappen = SerializeImage(value);
        OnPropertyChanged(nameof(CurrentItem.Wappen));
      }
    }

    public int GründungsJahr
    {
      get
      {
        if (CurrentItem.Gruendungsjahr == 0)
          CurrentItem.Gruendungsjahr = DateTime.Now.Year;
        return CurrentItem.Gruendungsjahr;
      }
      set
      {
        CurrentItem.Gruendungsjahr = value;
        OnPropertyChanged(nameof(CurrentItem.Gruendungsjahr));
      }
    }

    public List<PersonEntity> Trainers
    {
      get
      {
        return m_Trainers;
      }
    }


    public PersonEntity Trainer
    {
      get => Trainers.FirstOrDefault(x => x.PersonId == CurrentItem.TrainerId);
      set
      {
        if (value == null)
          return;
        CurrentItem.TrainerId = value.PersonId;
        OnPropertyChanged(nameof(Trainer));
        OnPropertyChanged(nameof(CurrentItem.TrainerId));
      }
    }

    private void __AttachEvents()
    {
      ImageSelected += (sender, e) =>
      {
        Wappen = (ImageSource)sender;
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
      m_Trainers = DatenbankHelfer.ReadEntity<PersonEntity>().Where(x => x.IsTrainer).ToList();
      OnPropertyChanged(nameof(Trainers));
      OnPropertyChanged(nameof(Trainer));
    }
  }
}
