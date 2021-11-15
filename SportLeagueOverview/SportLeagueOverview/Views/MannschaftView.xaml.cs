using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Entitites;
using SportLeagueOverview.ViewModels;
using System.ComponentModel;

namespace SportLeagueOverview.Views
{
  /// <summary>
  /// Interaction logic for MannschaftView.xaml
  /// </summary>
  public partial class MannschaftView : UserControlBase
  {
    public MannschaftView()
    {
      InitializeComponent();
      DataContext = new MannschaftViewModel();
    }

    private void DataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
    {
      e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;
    }

    private void __RowSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
      if (DataContext is MannschaftViewModel VM
        && e.AddedItems[0] is MannschaftEntity Mannschaft)
      {
          VM.CurrentMannschaft = Mannschaft;
      }
      //if(e.AddedItems)
    }
  }
}
