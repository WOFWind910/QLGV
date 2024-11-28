using System;
using System.Collections.Generic;

namespace QuanLyGiaoVu.Data;

public partial class Thongtinchamcong
{
    public int Sott { get; set; }

    public int? Magiaovien { get; set; }

    public int? Malophoc { get; set; }

    public DateTime Thoigianchamcong { get; set; }

    public virtual Giaovien? MagiaovienNavigation { get; set; } = null!;

    public virtual Lophoc? MalophocNavigation { get; set; } = null!;
}
