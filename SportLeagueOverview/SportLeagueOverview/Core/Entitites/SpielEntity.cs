using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLeagueOverview.Core.Entitites
{
  public class SpielEntity : EntityBase
  {
        public int SpielID { get; set; }
        public SpielStatus Status { get; set; }
        public DateTime Anpfiff { get; set; }
        public MannschaftEntity HeimMannschaft { get; set; }
        public MannschaftEntity AuswärtsMannschaft { get; set; }
        public AdressEntity AustragungsOrt { get; set; }

        public void ÄndereStatus()
        { }
        public SpielEntity()
      : base("Spiel")
    {

    }
  }
}
