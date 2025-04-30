using System;
using System.Collections.Generic;
using Azure.Models;

namespace Azure.Models;

public partial class Kullanicilar
{
    public string Id { get; set; } = null!;

    public string KullaniciAdi { get; set; } = null!;

    public string ParolaH { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RolId { get; set; }

    public virtual Roller Rol { get; set; } = null!;
}
