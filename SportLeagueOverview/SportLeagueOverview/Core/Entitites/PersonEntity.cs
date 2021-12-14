using SportLeagueOverview.Core.Attributes;
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
    [ColumnName("PersonId")]
    public int PersonId { get; set; }


    [DisplayName("Name")]
    [ColumnName("Name")]
    public string Name { get; set; }


    [DisplayName("Mannschaft")]
    [ColumnName("AktuelleMannId")]
    public int AktuelleMannId { get; set; }


    [DisplayName("Rückennummer")]
    [ColumnName("Rückennummer")]
    public int Rückennummer { get; set; }


    [DisplayName("Trainer?")]
    [ColumnName("IsTrainer")]
    public bool IsTrainer { get; set; }


    [DisplayName("Geburtsdatum")]
    [ColumnName("Geburtsdatum")]
    public DateTime Geburtsdatum { get; set; }


    [ColumnName("Bild")]
    public string Bild { get; set; }


    [DisplayName("Adress Id")]
    [ColumnName("AdressId")]
    public int AdressId { get; set; }


    [DisplayName("Eintrittsdatum")]
    [ColumnName("Eintrittsdatum")]
    public DateTime Eintrittsdatum { get; set; }

  }
}
