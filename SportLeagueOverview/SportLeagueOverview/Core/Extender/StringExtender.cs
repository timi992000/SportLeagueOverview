using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLeagueOverview.Core.Extender
{
  public static class StringExtender
  {

    public static bool IsNullOrEmpty(this string Value)
    {
      return Value == null || Value.Trim().Length == 0;
    }

    public static bool IsNullOrEmpty(this object Value)
    {
      return Value == null || Value.ToString().Trim().Length == 0;
    }

    public static bool IsNotNullOrEmpty(this string Value) => !IsNullOrEmpty(Value);

  }
}
