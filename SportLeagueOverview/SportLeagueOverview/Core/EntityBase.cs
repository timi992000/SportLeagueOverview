namespace SportLeagueOverview.Core
{
  public class EntityBase
  {
    public EntityBase(string TableName)
    {
      this.TableName = TableName;
    }

    public string TableName { get; set; }
  }
}
