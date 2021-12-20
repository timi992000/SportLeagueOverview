using MahApps.Metro.Controls;
using SportLeagueOverview.Core;
using SportLeagueOverview.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SportLeagueOverview
{
  /// <summary>
  /// Interaction logic for LISMatchDetail.xaml
  /// </summary>
  public partial class MatchDetailWindow : MetroWindow
  {
    public MatchDetailWindow(MatchViewModel MatchDetailViewModel)
    {
      DataContext = MatchDetailViewModel;
      InitializeComponent();
    }

    public void DoubleClickHandler(object sender, MouseEventArgs e)
    {
      var item = sender as ListBoxItem;
      if (item.DataContext is EventViewModel EventViewModel)
      {
        var Window = new EventDetailWindow(EventViewModel);
        Window.ShowDialog();
        if (DataContext is MatchViewModel MatchViewModel)
        {
          MatchViewModel.RefreshEvents();
        }
      }

    }
  }
}
