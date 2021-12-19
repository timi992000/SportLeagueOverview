using SportLeagueOverview.Core.Attributes;
using System.Windows;

namespace SportLeagueOverview.Core.Entitites
{
  public class AdressEntity : EntityBase
  {
    public AdressEntity()
      : base("Adresse", "AdressId", true)
    {
      Street = string.Empty;
      City = string.Empty;
      AdressAddition = string.Empty;
    }

    [ColumnName("AdressId")]
    public int AdressId { get; set; }

    [ColumnName("Straße")]
    public string Street { get; set; }

    [ColumnName("Hausnummer")]
    public int HouseNumber { get; set; }

    [ColumnName("AdressZusatz")]
    public string AdressAddition { get; set; }

    [ColumnName("PLZ")]
    public int ZipCode { get; set; }

    [ColumnName("Stadt")]
    public string City { get; set; }

    [IsDataColumn(false)]
    public bool IsAdressNew => AdressId == 0;
  }
}
