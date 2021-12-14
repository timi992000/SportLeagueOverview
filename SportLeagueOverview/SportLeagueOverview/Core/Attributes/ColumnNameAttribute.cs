using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLeagueOverview.Core.Attributes
{
  public class ColumnNameAttribute : Attribute
  {
    public string ColumnName { get; set; }

    public ColumnNameAttribute(string columnName)
    {
      ColumnName = columnName;
    }
  }
}
