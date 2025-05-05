using System;
using System.Collections.Generic;

namespace Azure.Models;

public partial class Diziler
{
    public int Id { get; set; }

    public string DiziAdi { get; set; } = null!;

    public string DiziAciklamasi { get; set; } = null!;

    public DateOnly Tarih { get; set; }

    public string Sure { get; set; } = null!;
}
