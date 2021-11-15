using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using System.Collections.ObjectModel;
using System.Linq;

namespace SportLeagueOverview.ViewModels
{
  public class MannschaftViewModel : ViewModelBase
  {
    private MannschaftEntity m_CurrentMannschaft;

    public MannschaftViewModel()
    {
    }

    public MannschaftEntity CurrentMannschaft
    {
      get
      {
        return m_CurrentMannschaft ?? new MannschaftEntity();
      }
      set
      {
        m_CurrentMannschaft = value;
        OnPropertyChanged(nameof(CurrentMannschaft));
        OnPropertyChanged(nameof(GründungsJahr));
        OnPropertyChanged(nameof(MannschaftsName));
        OnPropertyChanged(nameof(Trainer));
      }
    }

    public ObservableCollection<MannschaftEntity> Mannschaften
    {
      get
      {
        var Result = new ObservableCollection<MannschaftEntity>();
        DatenbankHelfer.ReadEntity<MannschaftEntity>().ForEach(x => Result.Add(x));
        CurrentMannschaft = Result.FirstOrDefault();
        return Result;
      }
    }
    public string MannschaftsName
    {
      get => CurrentMannschaft.Name;
      set
      {
        CurrentMannschaft.Name = value;
        OnPropertyChanged(nameof(MannschaftsName));
      }
    }

    public int GründungsJahr
    {
      get => CurrentMannschaft.Gruendungsjahr;
      set
      {
        CurrentMannschaft.Gruendungsjahr = value;
        OnPropertyChanged(nameof(GründungsJahr));
      }
    }

    public string Trainer
    {
      get => CurrentMannschaft.TrainerId.ToString();
      set
      {
        if (value != null && value.Length > 0)
          CurrentMannschaft.TrainerId = int.Parse(value);
        else
          CurrentMannschaft.TrainerId = 0;
        OnPropertyChanged(nameof(Trainer));
      }
    }
  }
}
