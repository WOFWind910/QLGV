using System;
using System.Collections.Generic;

namespace QuanLyGiaoVu.Data;

public partial class Thongtinchitietlophoc
{
    public int Stt { get; set; }

    public int Mahocvien { get; set; }

    public int Malophoc { get; set; }

    public virtual Hocvien? MahocvienNavigation { get; set; } = null!;

    public virtual Lophoc? MalophocNavigation { get; set; } = null!;
}
