using System;
using System.Collections.Generic;

namespace QuanLyGiaoVu.Data;

public partial class Giaovien
{
    public int Magiaovien { get; set; }

    public string Hogv { get; set; } = null!;

    public string Tengv { get; set; } = null!;

    public DateTime Ngaysinh { get; set; }

    public bool Gioitinh { get; set; }

    public string Sodienthoai { get; set; } = null!;

    public virtual ICollection<Danhgiahocvien> Danhgiahocviens { get; set; } = new List<Danhgiahocvien>();

    public virtual ICollection<Loiquytrinh> Loiquytrinhs { get; set; } = new List<Loiquytrinh>();

    public virtual ICollection<Lophoc> Lophocs { get; set; } = new List<Lophoc>();

    public virtual ICollection<Thongtinchamcong> Thongtinchamcongs { get; set; } = new List<Thongtinchamcong>();
}
