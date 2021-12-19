namespace SportLeagueOverview.Core.Entitites
{
    public class EventEntity : EntityBase
    {
        public EventEntity() :
          base("Ereignis", "EreignisId")
        {
        }

        public int EventId { get; set; }

        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public EventType EventType { get; set; }
        public int Minute { get; set; }
        public PersonEntity Player { get; set; }
    }
}
