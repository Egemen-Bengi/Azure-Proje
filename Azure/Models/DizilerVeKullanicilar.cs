using System;
using System.Collections.Generic;

namespace Azure.Models;

public partial class DizilerVeKullanicilar
{
    public string? KullaniciId { get; set; }

    public int? DiziId { get; set; }

    public virtual Diziler? Dizi { get; set; }

    public virtual Kullanicilar? Kullanici { get; set; }
}
