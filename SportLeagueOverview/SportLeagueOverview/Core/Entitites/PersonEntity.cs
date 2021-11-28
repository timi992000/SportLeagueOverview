using System;
using System.ComponentModel;

namespace SportLeagueOverview.Core.Entitites
{
  public class PersonEntity : EntityBase
  {
    public PersonEntity()
      : base("Person", "PersonId")
    {
    }

    [DisplayName("Spieler Id")]
    public int PersonId { get; set; }

    [DisplayName("Name")]
    public string Name { get; set; }

    [DisplayName("Mannschaft")]
    public int AktuelleMannId { get; set; }

    [DisplayName("Rückennummer")]
    public int Rückennummer { get; set; }

    [DisplayName("Trainer?")]
    public bool IsTrainer { get; set; }

    [DisplayName("Geburtsdatum")]
    public DateTime Geburtsdatum { get; set; }

    public string Bild { get; set; }

    [DisplayName("Adress Id")]
    public int AdressId { get; set; }

    [DisplayName("Eintrittsdatum")]
    public DateTime Eintrittsdatum { get; set; }
  }
}
