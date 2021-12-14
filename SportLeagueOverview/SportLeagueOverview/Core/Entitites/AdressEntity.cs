namespace SportLeagueOverview.Core.Entitites
{
  public class AdressEntity : EntityBase
  {
    public AdressEntity()
      : base("Adresse", "AdressId")
    {
    }

    public int AdressId { get; set; }
    public string Street { get; set; }
    public string AdressAddition { get; set; }
    public int ZipCode { get; set; }
    public string City { get; set; }
  }
}
