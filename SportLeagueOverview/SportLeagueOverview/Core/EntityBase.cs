using System.ComponentModel;

namespace SportLeagueOverview.Core
{
  public class EntityBase
  {
    public EntityBase(string TableName = "")
    {
      this.TableName = TableName;
    }

    [DisplayName("Tabelle")]
    public string TableName { get; set; }
  }
}
