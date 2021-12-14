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
    public string PlayerName { get; set; }


    [DisplayName("Mannschaft")]
    [ColumnName("AktuelleMannId")]
    public int TeamId { get; set; }


    [DisplayName("Rückennummer")]
    [ColumnName("Rückennummer")]
    public int BackNumber { get; set; }


    [DisplayName("Trainer?")]
    [ColumnName("IsTrainer")]
    public bool IsCoach { 
      get;
      set; 
    }


    [DisplayName("Geburtsdatum")]
    [ColumnName("Geburtsdatum")]
    public DateTime BirthDate { get; set; }


    [ColumnName("Bild")]
    public string ProfilePicture { get; set; }


    [DisplayName("Adress Id")]
    [ColumnName("AdressId")]
    public int AdressId { get; set; }


    [DisplayName("Eintrittsdatum")]
    [ColumnName("Eintrittsdatum")]
    public DateTime EntryDate { get; set; }

  }
}
