using SportLeagueOverview.Core.Attributes;
using System;
using System.ComponentModel;

namespace SportLeagueOverview.Core.Entitites
{
    public class TeamEntity : EntityBase
    {
        public TeamEntity()
          : base("Mannschaft", "MannschaftId")
        {
        }

        [ColumnName("MannschaftId")]
        [DisplayName("Mannschaftsnummer")]
        public int TeamId { get; set; }

        [ColumnName("Name")]
        [DisplayName("Mannschaftsname")]
        public string TeamName { get; set; }

        [ColumnName("Gruendungsjahr")]
        [DisplayName("Gründung")]
        public int FoundingYear { get; set; }

        [ColumnName("Wappen")]
        [DisplayName("Wappen")]
        public string CoatOfArms { get; set; }

    [ColumnName("TrainerId")]
    public int CoachId { get; set; }

    [DisplayName("Adress Id")]
    [ColumnName("AdressId")]
    public int AdressId { get; set; }

        public override string ToString()
        {
            return TeamName;
        }
    }
}
