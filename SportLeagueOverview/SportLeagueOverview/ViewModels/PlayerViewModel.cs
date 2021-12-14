using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using System;
using System.Windows.Media;

namespace SportLeagueOverview.ViewModels
{
  public class PlayerViewModel : ViewModelBase<PersonEntity>
  {

    public PlayerViewModel()
    {
      __AttachEvents();
    }

    public string PlayerName
    {
      get => CurrentItem.PlayerName;
      set
      {
        CurrentItem.PlayerName = value;
        OnPropertyChanged(nameof(PlayerName));
      }
    }

    public ImageSource ProfilePicture
    {
      get => DeserializeImage(CurrentItem.ProfilePicture);
      set
      {
        CurrentItem.ProfilePicture = SerializeImage(value);
        OnPropertyChanged(nameof(CurrentItem.ProfilePicture));
      }
    }

    public int BackNumber
    {
      get => CurrentItem.BackNumber;
      set
      {
        CurrentItem.BackNumber = value;
        OnPropertyChanged(nameof(CurrentItem.BackNumber));
      }
    }

    public DateTime BirthDate
    {
      get
      {
        if (CurrentItem.BirthDate == default(DateTime))
          CurrentItem.BirthDate = DateTime.Now;
        return CurrentItem.BirthDate;
      }
      set
      {
        CurrentItem.BirthDate = value;
        OnPropertyChanged(nameof(CurrentItem.BirthDate));
      }
    }

    public bool IsCoach
    {
      get => CurrentItem.IsCoach;
      set
      {
        CurrentItem.IsCoach = value;
        OnPropertyChanged(nameof(CurrentItem.IsCoach));
      }
    }

    public DateTime EntryDate
    {
      get
      {
        if (CurrentItem.EntryDate == default(DateTime))
          CurrentItem.EntryDate = DateTime.Now;
        return CurrentItem.EntryDate;
      }
      set
      {
        CurrentItem.EntryDate = value;
        OnPropertyChanged(nameof(CurrentItem.EntryDate));
      }
    }

    private void __AttachEvents()
    {
      ImageSelected += (sender, e) =>
      {
        ProfilePicture = (ImageSource)sender;
      };
    }

    public override void Execute_Delete(object sender)
    {
      if(!DBAccess.CheckTrainerIsUsed(CurrentItem.PersonId))
        base.Execute_Delete(sender);
      else
      {
        ThrowMessage("Trainer kann nicht gelöscht werden, da er in einer Mannschaft hinterlegt ist!");
      }
    }

  }
}
