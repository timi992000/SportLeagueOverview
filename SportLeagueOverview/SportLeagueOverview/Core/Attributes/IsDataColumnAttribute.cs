using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLeagueOverview.Core.Attributes
{
  public class IsDataColumnAttribute : Attribute
  {
    public bool IsDataColumn { get; set; }

    public IsDataColumnAttribute(bool IsDataTableColumn = false)
    {
      IsDataColumn = IsDataTableColumn;
    }
  }
}
