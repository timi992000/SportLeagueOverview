﻿using MahApps.Metro.Controls;
using System.Windows.Media;

namespace SportLeagueOverview.Core.Controls
{
  public class OwnGlyphItem : HamburgerMenuGlyphItem
  {
    public Geometry PathData
    {
      get
      {
        switch (Label)
        {
          case "Mannschaft":
            return Geometry.Parse("M16 12.999c0 .439-.45 1-1 1H7.995c-.539 0-.994-.447-.995-.999H1c-.54 0-1-.561-1-1 0-2.634 3-4 3-4s.229-.409 0-1c-.841-" +
              ".621-1.058-.59-1-3 .058-2.419 1.367-3 2.5-3s2.442.58 2.5 3c.058 2.41-.159 2.379-1 3-.229.59 0 1 0 1s1.549.711 2.42 2.088C9.196 9.369 10 8.999 10" +
              " 8.999s.229-.409 0-1c-.841-.62-1.058-.59-1-3 .058-2.419 1.367-3 2.5-3s2.437.581 2.495 3c.059 2.41-.158 2.38-1 3-.229.59 0 1 0 1s3.005 1.366 3.005 4z");
          case "Spiele":
            return Geometry.Parse("M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2M12,3C13.76,3 15.4,3.53 16.78,4.41L16.5,5H13L12,5L10.28," +
              "4.16L10.63,3.13C11.08,3.05 11.53,3 12,3M9.53,3.38L9.19,4.41L6.63,5.69L5.38,5.94C6.5,4.73 7.92,3.84 9.53,3.38M13,6H16L18.69,9.59L17.44,12.16L14.81,12.78L11.53," +
              "8.94L13,6M6.16,6.66L7,10L5.78,13.06L3.22,13.94C3.08,13.31 3,12.67 3,12C3,10.1 3.59,8.36 4.59,6.91L6.16,6.66M20.56,9.22C20.85,10.09 21,11.03 21,12C21,13.44 20.63," +
              "14.79 20.03,16H19L18.16,12.66L19.66,9.66L20.56,9.22M8,10H11L13.81,13.28L12,16L8.84,16.78L6.53,13.69L8,10M12,17L15,19L14.13,20.72C13.44,20.88 12.73,21 12,21C10.25," +
              "21 8.63,20.5 7.25,19.63L8.41,17.91L12,17M19,17H19.5C18.5,18.5 17,19.67 15.31,20.34L16,19L19,17Z");
          case "Statistiken":
            return Geometry.Parse("M16 14v1H0V0h1v14h15zM5 13H3V8h2v5zm4 0H7V3h2v10zm4 0h-2V6h2v7z");
          case "Spieler":
            return Geometry.Parse("M12,4A4,4 0 0,1 16,8A4,4 0 0,1 12,12A4,4 0 0,1 8,8A4,4 0 0,1 12,4M12,14C16.42,14 20,15.79 20,18V20H4V18C4,15.79 7.58,14 12,14Z");

          default:
            return Geometry.Parse("");
        };
      }
    }

  }
}
