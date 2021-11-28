using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using System.Collections.Generic;
using System.Linq;

namespace SportLeagueOverview.ViewModels
{
  public class MannschaftViewModel : ViewModelBase<MannschaftEntity>
  {
    private List<PersonEntity> m_Trainers;

    public MannschaftViewModel()
    {
      __RefreshTrainer();
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

    public List<PersonEntity> Trainers
    {
      get
      {
        return m_Trainers;
      }
    }


    public PersonEntity Trainer
    {
      get => m_Trainers.FirstOrDefault(x => x.PersonId ==  CurrentItem.TrainerId);
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
