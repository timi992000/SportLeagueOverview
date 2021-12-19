using SportLeagueOverview.Core;
using SportLeagueOverview.ViewModels;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportLeagueOverview.Views
{
  /// <summary>
  /// Interaction logic for SpieleView.xaml
  /// </summary>
  public partial class MatchView : UserControlBase
  {
    public MatchView()
    {
      InitializeComponent();
      DataContext = new MatchListViewModel();
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

    public void DoubleClickHandler(object sender, MouseEventArgs e)
    {
      var item = sender as ListBoxItem;
      if (item.DataContext is MatchViewModel MatchDetailViewModel)
      {
        var Window = new MatchDetailWindow(MatchDetailViewModel);
        Window.ShowDialog();
        if (DataContext is MatchListViewModel MatchViewModel)
        {
          MatchViewModel.Reload.Execute(null);
        }
      }

    }
  }
}
