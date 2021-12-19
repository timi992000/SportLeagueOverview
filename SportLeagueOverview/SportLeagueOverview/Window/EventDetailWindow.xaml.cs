using MahApps.Metro.Controls;
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
    public partial class EventDetailWindow : MetroWindow
    {
        public EventDetailWindow(EventViewModel EventViewModel)
        {
            DataContext = EventViewModel;
            InitializeComponent();
        }
    }
}
