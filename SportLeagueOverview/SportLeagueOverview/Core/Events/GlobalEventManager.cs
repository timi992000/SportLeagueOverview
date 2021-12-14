using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SportLeagueOverview.Core.Events
{
  public static class GlobalEventManager
  {
    public static event EventHandler<KeyEventArgs> KeyPressed;

    public static void InvokeKeyPressed(object sender, KeyEventArgs e) => KeyPressed?.Invoke(sender, e);

  }
}
