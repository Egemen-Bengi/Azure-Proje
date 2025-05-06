using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Azure.Models;

public partial class Diziler
{
    public int Id { get; set; }

    public string DiziAdi { get; set; } = null!;

    public string DiziAciklamasi { get; set; } = null!;

    public DateOnly Tarih { get; set; }

    public int Sure { get; set; }

    public int SezonSayisi { get; set; }

    [JsonIgnore]
    public virtual ICollection<Kullanicilar> Kullanicis { get; set; } = new List<Kullanicilar>();
}
