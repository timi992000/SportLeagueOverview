using System;
using System.ComponentModel;

namespace SportLeagueOverview.Core
{
  public class EntityBase : ICloneable
  {
    public EntityBase(string TableName = "", string PrimaryKeyColumn = "")
    {
      this.TableName = TableName;
      this.PrimaryKeyColumn = PrimaryKeyColumn;
    }

    [DisplayName("Tabelle")]
    public string TableName { get; set; }
    public string PrimaryKeyColumn { get; set; }
    public bool IsNew { get; set; }

    public object Clone()
    {
      return this;
    }
  }
}
