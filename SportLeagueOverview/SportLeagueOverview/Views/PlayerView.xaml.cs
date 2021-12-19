using SportLeagueOverview.Core;
using SportLeagueOverview.ViewModels;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace SportLeagueOverview.Views
{
  /// <summary>
  /// Interaction logic for PlayerView.xaml
  /// </summary>
  public partial class PlayerView : UserControlBase
  {
    public PlayerView()
    {
      InitializeComponent();
      DataContext = new PlayerViewModel();
    }

    private void DataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
    {
      if (e.PropertyType == typeof(DateTime))
      {
        (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
      }
      e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;
      if (e.Column.Header.Equals("Bild") ||
        e.Column.Header.Equals("Tabelle") ||
        e.Column.Header.Equals("PrimaryKeyColumn") ||
          e.Column.Header.Equals("IsNew"))
      {
        e.Cancel = true;
      }
    }
  }
}
