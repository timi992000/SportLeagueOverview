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
      if (e.Column.Header.Equals("Wappen") ||
        e.Column.Header.Equals("Tabelle"))
      {
        e.Cancel = true;
      }
    }
  }
}
