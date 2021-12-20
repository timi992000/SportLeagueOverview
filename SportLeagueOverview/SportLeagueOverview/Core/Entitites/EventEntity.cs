using SportLeagueOverview.Core.Attributes;
using SportLeagueOverview.Core.Common;
using System.Collections.Generic;

namespace SportLeagueOverview.Core.Entitites
{
  public class EventEntity : EntityBase
  {
    public EventEntity() :
      base("Ereignis", "EreignisId")
    {
      Person = new PersonEntity();
    }

    [ColumnName("EreignisId")]
    public int EventId { get; set; }


    [ColumnName("SpielId")]
    public int MatchId { get; set; }

    [ColumnName("MannId")]
    public int TeamId { get; set; }

    [IsDataColumn(false)]
    public EventType EventType { get; set; }

    [ColumnName("EreignisTyp")]
    public int EventTypeId
    {
      get
      {
        return (int)EventType;
      }
      set
      {
        EventType = (EventType)value;
      }
    }

    [ColumnName("Minute")]
    public int Minute { get; set; }

    [IsDataColumn(false)]
    public PersonEntity Person { get; set; }

    [ColumnName("SpielerId")]
    public int PlayerId
    {
      get
      {
        return Person.PersonId;
      }
      set
      {
        Person = DBAccess.ReadEntity<PersonEntity>(new List<int> { value }, true)[0];
      }
    }
  }
}
