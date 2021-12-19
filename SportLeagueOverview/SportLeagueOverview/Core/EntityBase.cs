using SportLeagueOverview.Core.Attributes;
using SportLeagueOverview.Core.Entitites;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SportLeagueOverview.Core
{
  public class EntityBase : ICloneable
  {
    public EntityBase(string TableName = "", string PrimaryKeyColumn = "", bool IsAdress = false)
    {
      this.TableName = TableName;
      this.PrimaryKeyColumn = PrimaryKeyColumn;
      if(!IsAdress)
        Adress = new AdressEntity();
    }

    [DisplayName("Tabelle")]
    [IsDataColumn(false)]
    public string TableName { get; set; }

    [IsDataColumn(false)]
    public string PrimaryKeyColumn { get; set; }

    [IsDataColumn(false)]
    public bool IsNew { get; set; }

    public AdressEntity Adress { get; set; }

    public object Clone()
    {
      return MemberwiseClone();
    }
  }
}
