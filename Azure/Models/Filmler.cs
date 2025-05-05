using System;
using System.Collections.Generic;

namespace Azure.Models;

public partial class Filmler
{
    public int Id { get; set; }

    public string FilmAdi { get; set; } = null!;

    public string FilmAciklamasi { get; set; } = null!;

    public DateOnly Tarih { get; set; }

    public int Sure { get; set; }

    public virtual ICollection<Kullanicilar> Kullanicis { get; set; } = new List<Kullanicilar>();

    public virtual ICollection<Kullanicilar> KullanicisNavigation { get; set; } = new List<Kullanicilar>();
}
