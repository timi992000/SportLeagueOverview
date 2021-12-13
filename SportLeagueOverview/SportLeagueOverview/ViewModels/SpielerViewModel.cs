using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using System;
using System.Windows.Media;

namespace SportLeagueOverview.ViewModels
{
  public class SpielerViewModel : ViewModelBase<PersonEntity>
  {

    public SpielerViewModel()
    {
      __AttachEvents();
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

    public ImageSource Bild
    {
      get => DeserializeImage(CurrentItem.Bild);
      set
      {
        CurrentItem.Bild = SerializeImage(value);
        OnPropertyChanged(nameof(CurrentItem.Bild));
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
      get
      {
        if (CurrentItem.Geburtsdatum == default(DateTime))
          CurrentItem.Geburtsdatum = DateTime.Now;
        return CurrentItem.Geburtsdatum;
      }
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
      get
      {
        if (CurrentItem.Eintrittsdatum == default(DateTime))
          CurrentItem.Eintrittsdatum = DateTime.Now;
        return CurrentItem.Eintrittsdatum;
      }
      set
      {
        CurrentItem.Eintrittsdatum = value;
        OnPropertyChanged(nameof(CurrentItem.Eintrittsdatum));
      }
    }

    private void __AttachEvents()
    {
      ImageSelected += (sender, e) =>
      {
        Bild = (ImageSource)sender;
      };
    }

    public override void Execute_Delete(object sender)
    {
      if(!DatenbankHelfer.CheckTrainerIsUsed(CurrentItem.PersonId))
        base.Execute_Delete(sender);
      else
      {
        ThrowMessage("Trainer kann nicht gelöscht werden, da er in einer Mannschaft hinterlegt ist!");
      }
    }

  }
}
