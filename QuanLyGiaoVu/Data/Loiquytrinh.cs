using System;
using System.Collections.Generic;

namespace QuanLyGiaoVu.Data;

public partial class Loiquytrinh
{
    public int Maloi { get; set; }

    public int Malophoc { get; set; }

    public int Magiaovien { get; set; }

    public string Tenloi { get; set; } = null!;

    public string Motaloi { get; set; } = null!;

    public DateTime Ngayvipham { get; set; }

    public virtual Giaovien? MagiaovienNavigation { get; set; } = null!;

    public virtual Lophoc? MalophocNavigation { get; set; } = null!;
}
