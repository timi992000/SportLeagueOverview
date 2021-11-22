using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using System.Collections.ObjectModel;
using System.Linq;

namespace SportLeagueOverview.ViewModels
{
  public class MannschaftViewModel : ViewModelBase<MannschaftEntity>
  {
    public MannschaftViewModel()
    { 
    }

    public string MannschaftsName
    {
      get => CurrentItem.Name;
      set
      {
        CurrentItem.Name = value;
        OnPropertyChanged(nameof(MannschaftsName));
      }
    }

    public int GründungsJahr
    {
      get => CurrentItem.Gruendungsjahr;
      set
      {
        CurrentItem.Gruendungsjahr = value;
        OnPropertyChanged(nameof(GründungsJahr));
      }
    }

    public string Trainer
    {
      get => CurrentItem.TrainerId.ToString();
      set
      {
        if (value != null && value.Length > 0)
          CurrentItem.TrainerId = int.Parse(value);
        else
          CurrentItem.TrainerId = 0;
        OnPropertyChanged(nameof(Trainer));
      }
    }
  }
}
