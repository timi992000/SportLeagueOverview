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
      SaveRequested += (sender, EventArgs) =>
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
      get => CurrentItem.Geburtsdatum;
      set
      {
        CurrentItem.Geburtsdatum = value;
        OnPropertyChanged(nameof(Geburtsdatum));
      }
    }

    public bool IsTrainer
    {
      get => CurrentItem.IsTrainer;
      set
      {
        CurrentItem.IsTrainer = value;
      }
    }

    public DateTime Eintrittsdatum
    {
      get => CurrentItem.Eintrittsdatum;
      set
      {
        CurrentItem.Eintrittsdatum = value;
        OnPropertyChanged(nameof(Eintrittsdatum));
      }
    }

  }
}
