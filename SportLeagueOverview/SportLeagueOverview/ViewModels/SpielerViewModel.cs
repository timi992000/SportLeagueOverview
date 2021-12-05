using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Entitites;
using System;

namespace SportLeagueOverview.ViewModels
{
  public class SpielerViewModel : ViewModelBase<PersonEntity>
  {

    public SpielerViewModel()
    {
      ImageChanged += (sender, EventArgs) =>
      {
        OnPropertyChanged(nameof(ImageSource));
      };
      CurrentItemChanged += (sender, EventArgs) =>
      {
        DeserializeImage(CurrentItem.Bild);
      };
      EntitySpectator.SaveRequested += (sender, EventArgs) =>
      {
        CurrentItem.Bild = Convert.ToBase64String(SerializedImage);
      };
      DeserializeImage(CurrentItem.Bild);
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

    public int Rückennummer
    {
      get => CurrentItem.Rückennummer;
      set
      {
        CurrentItem.Rückennummer = value;
        OnPropertyChanged(nameof(CurrentItem.Rückennummer));
      }
    }

    public DateTime Geburtsdatum
    {
      get => CurrentItem.Geburtsdatum;
      set
      {
        CurrentItem.Geburtsdatum = value;
        OnPropertyChanged(nameof(CurrentItem.Geburtsdatum));
      }
    }

    public bool IsTrainer
    {
      get => CurrentItem.IsTrainer;
      set
      {
        CurrentItem.IsTrainer = value;
        OnPropertyChanged(nameof(CurrentItem.IsTrainer));
      }
    }

    public DateTime Eintrittsdatum
    {
      get => CurrentItem.Eintrittsdatum;
      set
      {
        CurrentItem.Eintrittsdatum = value;
        OnPropertyChanged(nameof(CurrentItem.Eintrittsdatum));
      }
    }

  }
}
