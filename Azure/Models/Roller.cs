using System;
using System.Collections.Generic;

namespace Azure.Models;

public partial class Roller
{
    public int Id { get; set; }

    public string RolAdi { get; set; } = null!;

    public virtual ICollection<Kullanicilar> Kullanicilars { get; set; } = new List<Kullanicilar>();
}
