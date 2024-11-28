using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiaoVu.Data;

public partial class QlgvContext : DbContext
{
    public QlgvContext()
    {
    }

    public QlgvContext(DbContextOptions<QlgvContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Danhgiahocvien> Danhgiahocviens { get; set; }

    public virtual DbSet<Giaovien> Giaoviens { get; set; }

    public virtual DbSet<Hocvien> Hocviens { get; set; }

    public virtual DbSet<Loiquytrinh> Loiquytrinhs { get; set; }

    public virtual DbSet<Lophoc> Lophocs { get; set; }

    public virtual DbSet<Thongtinchamcong> Thongtinchamcongs { get; set; }

    public virtual DbSet<Thongtinchitietlophoc> Thongtinchitietlophocs { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=HUY\\SQL1;Database=QLGV;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Danhgiahocvien>(entity =>
        {
            entity.HasKey(e => e.Stthv).IsClustered(false);

            entity.ToTable("DANHGIAHOCVIEN");

            entity.HasIndex(e => e.Malophoc, "CO_FK");

            entity.HasIndex(e => e.Magiaovien, "GHI_FK");

            entity.HasIndex(e => e.Mahocvien, "SOHUU_FK");

            entity.Property(e => e.Stthv).HasColumnName("STTHV");
            entity.Property(e => e.Diemso).HasColumnName("DIEMSO");
            entity.Property(e => e.Magiaovien).HasColumnName("MAGIAOVIEN");
            entity.Property(e => e.Mahocvien).HasColumnName("MAHOCVIEN");
            entity.Property(e => e.Malophoc).HasColumnName("MALOPHOC");
            entity.Property(e => e.Nhanxet)
                .HasMaxLength(100)
                .HasColumnName("NHANXET");

            entity.HasOne(d => d.MagiaovienNavigation).WithMany(p => p.Danhgiahocviens)
                .HasForeignKey(d => d.Magiaovien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANHGIAH_GHI_GIAOVIEN");

            entity.HasOne(d => d.MahocvienNavigation).WithMany(p => p.Danhgiahocviens)
                .HasForeignKey(d => d.Mahocvien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANHGIAH_SOHUU_HOCVIEN");

            entity.HasOne(d => d.MalophocNavigation).WithMany(p => p.Danhgiahocviens)
                .HasForeignKey(d => d.Malophoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANHGIAH_CO_LOPHOC");
        });

        modelBuilder.Entity<Giaovien>(entity =>
        {
            entity.HasKey(e => e.Magiaovien).IsClustered(false);

            entity.ToTable("GIAOVIEN");

            entity.Property(e => e.Magiaovien).HasColumnName("MAGIAOVIEN");
            entity.Property(e => e.Gioitinh).HasColumnName("GIOITINH");
            entity.Property(e => e.Hogv)
                .HasMaxLength(50)
                .HasColumnName("HOGV");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("datetime")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Sodienthoai)
                .HasMaxLength(10)
                .HasColumnName("SODIENTHOAI");
            entity.Property(e => e.Tengv)
                .HasMaxLength(50)
                .HasColumnName("TENGV");
        });

        modelBuilder.Entity<Hocvien>(entity =>
        {
            entity.HasKey(e => e.Mahocvien).IsClustered(false);

            entity.ToTable("HOCVIEN");

            entity.Property(e => e.Mahocvien).HasColumnName("MAHOCVIEN");
            entity.Property(e => e.Gioitinh).HasColumnName("GIOITINH");
            entity.Property(e => e.Hohv)
                .HasMaxLength(50)
                .HasColumnName("HOHV");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("datetime")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Sodienthoaiphuhuynh).HasColumnName("SODIENTHOAIPHUHUYNH");
            entity.Property(e => e.Tenhv)
                .HasMaxLength(50)
                .HasColumnName("TENHV");
        });

        modelBuilder.Entity<Loiquytrinh>(entity =>
        {
            entity.HasKey(e => e.Maloi).IsClustered(false);

            entity.ToTable("LOIQUYTRINH");

            entity.HasIndex(e => e.Malophoc, "CHUA_FK");

            entity.HasIndex(e => e.Magiaovien, "CUA_FK");

            entity.Property(e => e.Maloi).HasColumnName("MALOI");
            entity.Property(e => e.Magiaovien).HasColumnName("MAGIAOVIEN");
            entity.Property(e => e.Malophoc).HasColumnName("MALOPHOC");
            entity.Property(e => e.Motaloi)
                .HasMaxLength(100)
                .HasColumnName("MOTALOI");
            entity.Property(e => e.Ngayvipham)
                .HasColumnType("datetime")
                .HasColumnName("NGAYVIPHAM");
            entity.Property(e => e.Tenloi)
                .HasMaxLength(50)
                .HasColumnName("TENLOI");

            entity.HasOne(d => d.MagiaovienNavigation).WithMany(p => p.Loiquytrinhs)
                .HasForeignKey(d => d.Magiaovien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOIQUYTR_CUA_GIAOVIEN");

            entity.HasOne(d => d.MalophocNavigation).WithMany(p => p.Loiquytrinhs)
                .HasForeignKey(d => d.Malophoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOIQUYTR_CHUA_LOPHOC");
        });

        modelBuilder.Entity<Lophoc>(entity =>
        {
            entity.HasKey(e => e.Malophoc).IsClustered(false);

            entity.ToTable("LOPHOC");

            entity.HasIndex(e => e.Magiaovien, "XEM_FK");

            entity.Property(e => e.Malophoc).HasColumnName("MALOPHOC");
            entity.Property(e => e.Lichhoc)
                .HasColumnType("datetime")
                .HasColumnName("LICHHOC");
            entity.Property(e => e.Magiaovien).HasColumnName("MAGIAOVIEN");
            entity.Property(e => e.Ngaybatdau)
                .HasColumnType("datetime")
                .HasColumnName("NGAYBATDAU");
            entity.Property(e => e.Ngayketthuc)
                .HasColumnType("datetime")
                .HasColumnName("NGAYKETTHUC");
            entity.Property(e => e.Tenlophoc)
                .HasMaxLength(100)
                .HasColumnName("TENLOPHOC");

            entity.HasOne(d => d.MagiaovienNavigation).WithMany(p => p.Lophocs)
                .HasForeignKey(d => d.Magiaovien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOPHOC_XEM_GIAOVIEN");
        });

        modelBuilder.Entity<Thongtinchamcong>(entity =>
        {
            entity.HasKey(e => e.Sott).IsClustered(false);

            entity.ToTable("THONGTINCHAMCONG");

            entity.HasIndex(e => e.Magiaovien, "THUCHIEN_FK");

            entity.HasIndex(e => e.Malophoc, "THUOCVE_FK");

            entity.Property(e => e.Sott).HasColumnName("SOTT");
            entity.Property(e => e.Magiaovien).HasColumnName("MAGIAOVIEN");
            entity.Property(e => e.Malophoc).HasColumnName("MALOPHOC");
            entity.Property(e => e.Thoigianchamcong)
                .HasColumnType("datetime")
                .HasColumnName("THOIGIANCHAMCONG");

            entity.HasOne(d => d.MagiaovienNavigation).WithMany(p => p.Thongtinchamcongs)
                .HasForeignKey(d => d.Magiaovien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_THONGTIN_THUCHIEN_GIAOVIEN");

            entity.HasOne(d => d.MalophocNavigation).WithMany(p => p.Thongtinchamcongs)
                .HasForeignKey(d => d.Malophoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_THONGTIN_THUOCVE_LOPHOC");
        });

        modelBuilder.Entity<Thongtinchitietlophoc>(entity =>
        {
            entity.HasKey(e => e.Stt).IsClustered(false);

            entity.ToTable("THONGTINCHITIETLOPHOC");

            entity.HasIndex(e => e.Malophoc, "THUOC_FK");

            entity.HasIndex(e => e.Mahocvien, "TRUYXUAT_FK");

            entity.Property(e => e.Stt).HasColumnName("STT");
            entity.Property(e => e.Mahocvien).HasColumnName("MAHOCVIEN");
            entity.Property(e => e.Malophoc).HasColumnName("MALOPHOC");

            entity.HasOne(d => d.MahocvienNavigation).WithMany(p => p.Thongtinchitietlophocs)
                .HasForeignKey(d => d.Mahocvien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_THONGTIN_TRUYXUAT_HOCVIEN");

            entity.HasOne(d => d.MalophocNavigation).WithMany(p => p.Thongtinchitietlophocs)
                .HasForeignKey(d => d.Malophoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_THONGTIN_THUOC_LOPHOC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
