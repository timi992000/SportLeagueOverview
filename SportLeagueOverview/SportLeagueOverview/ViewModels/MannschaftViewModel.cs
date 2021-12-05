﻿using SportLeagueOverview.Core;
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
      ImageChanged += (sender, EventArgs) =>
      {
        OnPropertyChanged(nameof(ImageSource));
      };
      CurrentItemChanged += (sender, EventArgs) =>
      {
        DeserializeImage(CurrentItem.Wappen);
      };
      EntitySpectator.SaveRequested += (sender, EventArgs) =>
       {
         CurrentItem.Wappen = Convert.ToBase64String(SerializedImage);
       };
      EntitySpectator.SaveCompleted += (sender, EventArgs) =>
      {
        __RefreshTrainer();
      };
      EntitySpectator.DeleteCompleted += (sender, EventArgs) =>
      {
        __RefreshTrainer();
      };
      DeserializeImage(CurrentItem.Wappen);
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

    public int GründungsJahr
    {
      get => CurrentItem.Gruendungsjahr;
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
      get => m_Trainers.FirstOrDefault(x => x.PersonId == CurrentItem.TrainerId);
      set
      {
        CurrentItem.TrainerId = value.PersonId;
        OnPropertyChanged(nameof(Trainer));
      }
    }

    private void __RefreshTrainer()
    {
      m_Trainers = DatenbankHelfer.ReadEntity<PersonEntity>().Where(x => x.IsTrainer).ToList();
      OnPropertyChanged(nameof(Trainers));
    }
  }
}
