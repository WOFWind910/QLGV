using System;
using System.Collections.Generic;

namespace QuanLyGiaoVu.Data;

public partial class Danhgiahocvien
{
    public int Stthv { get; set; }

    public int Malophoc { get; set; }

    public int Mahocvien { get; set; }

    public int Magiaovien { get; set; }

    public string Nhanxet { get; set; } = null!;

    public double Diemso { get; set; }

    public virtual Giaovien? MagiaovienNavigation { get; set; } = null!;

    public virtual Hocvien? MahocvienNavigation { get; set; } = null!;

    public virtual Lophoc? MalophocNavigation { get; set; } = null!;
}
