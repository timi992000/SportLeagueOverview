﻿using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Entitites;
using System;

namespace SportLeagueOverview.ViewModels
{
  public class SpielerViewModel : ViewModelBase<PersonEntity>
  {

    public SpielerViewModel()
    {
    }

    public string SpielerName
    {
      get => CurrentItem.Name;
      set
      {
        CurrentItem.Name = value;
        OnPropertyChanged(nameof(SpielerName));
      }
    }

    public int RückenNummer
    {
      get => CurrentItem.Rückennummer;
      set
      {
        CurrentItem.Rückennummer = value;
        OnPropertyChanged(nameof(RückenNummer));
      }
    }

    public DateTime Geburtsdatum
    {
      get => CurrentItem.Geburtsdatum != null ?  DateTime.Parse(CurrentItem.Geburtsdatum) : DateTime.Now;
      set
      {
        CurrentItem.Geburtsdatum = value.ToShortDateString();
        OnPropertyChanged(nameof(Geburtsdatum));
      }
    }

  }
}
