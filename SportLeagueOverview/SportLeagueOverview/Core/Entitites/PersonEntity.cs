namespace SportLeagueOverview.Core.Entitites
{
  public class PersonEntity : EntityBase
  {
    public PersonEntity()
      : base("Person")
    {
    }

    public int PersonId { get; set; }
    public string Name { get; set; }
    public int AktuelleMannId { get; set; }
    public int Rückennummer { get; set; }
    public int IsTrainer { get; set; }
    public string Geburtsdatum { get; set; }
    public string Bild { get; set; }
    public int AdressId { get; set; }
    public string Eintrittsdatum { get; set; }
  }
}
