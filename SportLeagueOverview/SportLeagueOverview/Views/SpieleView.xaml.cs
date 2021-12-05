using SportLeagueOverview.Core;
using SportLeagueOverview.ViewModels;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace SportLeagueOverview.Views
{
  /// <summary>
  /// Interaction logic for SpieleView.xaml
  /// </summary>
  public partial class SpieleView : UserControlBase
  {
    public SpieleView()
    {
      InitializeComponent();
      DataContext = new SpieleViewModel();
    }

    private void DataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
    {
      if (e.PropertyType == typeof(DateTime))
      {
        (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
      }
      e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;
      //if (e.Column.Header.Equals("Wappen") ||
      //  e.Column.Header.Equals("Tabelle"))
      //{
      //  e.Cancel = true;
      //}
    }
  }
}
