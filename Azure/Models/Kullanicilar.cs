using System;
using System.Collections.Generic;

namespace Azure.Models;

public partial class Kullanicilar
{
    public string Id { get; set; } = null!;

    public string KullaniciAdi { get; set; } = null!;

    public string ParolaH { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RolId { get; set; }

    public virtual Roller Rol { get; set; } = null!;

    public virtual ICollection<Filmler> Films { get; set; } = new List<Filmler>();

    public virtual ICollection<Filmler> FilmsNavigation { get; set; } = new List<Filmler>();
}
