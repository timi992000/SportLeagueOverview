using SportLeagueOverview.Core;
using SportLeagueOverview.ViewModels;
using System.ComponentModel;

namespace SportLeagueOverview.Views
{
  /// <summary>
  /// Interaction logic for SpielerView.xaml
  /// </summary>
  public partial class SpielerView : UserControlBase
  {
    public SpielerView()
    {
      InitializeComponent();
      DataContext = new SpielerViewModel();
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
