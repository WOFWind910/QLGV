using System;
using System.Collections.Generic;

namespace QuanLyGiaoVu.Data;

public partial class Hocvien
{
    public int Mahocvien { get; set; }

    public string Hohv { get; set; } = null!;

    public string Tenhv { get; set; } = null!;

    public DateTime Ngaysinh { get; set; }

    public bool Gioitinh { get; set; }

    public int Sodienthoaiphuhuynh { get; set; }

    public virtual ICollection<Danhgiahocvien> Danhgiahocviens { get; set; } = new List<Danhgiahocvien>();

    public virtual ICollection<Thongtinchitietlophoc> Thongtinchitietlophocs { get; set; } = new List<Thongtinchitietlophoc>();
}
