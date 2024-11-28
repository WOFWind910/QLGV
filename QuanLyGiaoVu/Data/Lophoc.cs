using System;
using System.Collections.Generic;

namespace QuanLyGiaoVu.Data;

public partial class Lophoc
{
    public int Malophoc { get; set; }

    public int Magiaovien { get; set; }

    public string Tenlophoc { get; set; } = null!;

    public DateTime Ngaybatdau { get; set; }

    public DateTime Ngayketthuc { get; set; }

    public DateTime Lichhoc { get; set; }

    public virtual ICollection<Danhgiahocvien> Danhgiahocviens { get; set; } = new List<Danhgiahocvien>();

    public virtual ICollection<Loiquytrinh> Loiquytrinhs { get; set; } = new List<Loiquytrinh>();

    public virtual Giaovien? MagiaovienNavigation { get; set; } = null!;

    public virtual ICollection<Thongtinchamcong> Thongtinchamcongs { get; set; } = new List<Thongtinchamcong>();

    public virtual ICollection<Thongtinchitietlophoc> Thongtinchitietlophocs { get; set; } = new List<Thongtinchitietlophoc>();
}
