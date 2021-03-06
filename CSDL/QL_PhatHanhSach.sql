create database QuanLyPhatHanhSach
go

use QuanLyPhatHanhSach
go


create table NXB
(
	MANXB int identity primary key not null,
	TENNXB nvarchar(50),
	DIACHI nvarchar(50),
	SDT nvarchar(15),
	SOTK nvarchar(20),
	TIENCONNO int
)
go

create table CT_NXB
(
	MANXB int,
	MASACH int,
	GIA int,
	SLLAY int,
	SLDABAN int
	
	primary key (MANXB, MASACH)		
	foreign key (MANXB) references dbo.NXB(MANXB),
	foreign key (MASACH) references dbo.SACH(MASACH)
)

go

insert into dbo.NXB(TENNXB,DIACHI,SDT,SOTK,TIENCONNO)
values(N'Nhà xuất bản A','Q6,HCM','0808123486','160620101234',0)
insert into dbo.NXB(TENNXB,DIACHI,SDT,SOTK,TIENCONNO)
values(N'Nhà xuất bản B','Q7,HCM','0808124785','160620105678',0)
insert into dbo.NXB(TENNXB,DIACHI,SDT,SOTK,TIENCONNO)
values(N'Nhà xuất bản C','Q8,HCM','0808127823','160620101234',0)
insert into dbo.NXB(TENNXB,DIACHI,SDT,SOTK,TIENCONNO)
values(N'Nhà xuất bản D','Q1,HCM','0808123428','160620109101',0)
insert into dbo.NXB(TENNXB,DIACHI,SDT,SOTK,TIENCONNO)
values(N'Nhà xuất bản E','Q2,HCM','0808124839','160620101213',0)

go

create table SACH
(
	MASACH int identity primary key not null,
	MANXB int,
	TENSACH nvarchar(50),
	TACGIA nvarchar(50),	
	THELOAI NVARCHAR(50),
	SOLUONG int default 0,
	GIA int
	
	foreign key(MANXB) references dbo.NXB(MANXB)
)
go

insert into dbo.SACH(TENSACH,MANXB,TACGIA,THELOAI,GIA)
values(N'Thay thái độ đổi cuộc đời',1,N'A',N'Văn học',58000)
insert into dbo.SACH(TENSACH,MANXB,TACGIA,THELOAI,GIA)
values(N'Tony buổi sáng',2,N'A',N'Văn học',68000)
insert into dbo.SACH(TENSACH,MANXB,TACGIA,THELOAI,GIA)
values(N'Nhà giả kim',3,N'A',N'Văn học',53000)
insert into dbo.SACH(TENSACH,MANXB,TACGIA,THELOAI,GIA)
values(N'Cô phù thủy kiki',4,N'A',N'Văn học',77000)
insert into dbo.SACH(TENSACH,MANXB,TACGIA,THELOAI,GIA)
values(N'Giai toán 5',5,N'B',N'Tham khảo',24000)
insert into dbo.SACH(TENSACH,MANXB,TACGIA,THELOAI,GIA)
values(N'Ngữ văn 10',1,N'B',N'Giao khoa',24000)
insert into dbo.SACH(TENSACH,MANXB,TACGIA,THELOAI,GIA)
values(N'Mac-Lenin',2,N'B',N'Giao khoa',42000)
insert into dbo.SACH(TENSACH,MANXB,TACGIA,THELOAI,GIA)
values(N'Clean code',3,N'C',N'Tham khảo',320000)
insert into dbo.SACH(TENSACH,MANXB,TACGIA,THELOAI,GIA)
values(N'Harry Potter',4,N'D',N'Tiểu thuyết',240000)
insert into dbo.SACH(TENSACH,MANXB,TACGIA,THELOAI,GIA)
values(N'ABCDEF',5,N'D',N'Khác',80000)
go

create table DAILY
(
	MADL int identity primary key not null,
	TENDL nvarchar(50),
	DIACHI nvarchar(50),
	TONGSOSACH int,
	TIENCONNO int
)
go

insert into dbo.DAILY(TENDL,DIACHI,TIENCONNO)
values (N'Đại lý A','Q1,HCM',0)
insert into dbo.DAILY(TENDL,DIACHI,TIENCONNO)
values (N'Đại lý B','Q2,HCM',0)
insert into dbo.DAILY(TENDL,DIACHI,TIENCONNO)
values (N'Đại lý C','Q3,HCM',0)
insert into dbo.DAILY(TENDL,DIACHI,TIENCONNO)
values (N'Đại lý D','Q4,HCM',0)
insert into dbo.DAILY(TENDL,DIACHI,TIENCONNO)
values (N'Đại lý E','Q5,HCM',0)
go

create table CT_DAILY
(
	MADL int,
	MASACH int,
	GIA int,
	SLLAY int,
	SLDABAN int
	
	primary key (MADL, MASACH)		
	foreign key (MADL) references dbo.DAILY(MADL),
	foreign key (MASACH) references dbo.SACH(MASACH)
)

go

create table PHIEUXUAT
(
	MAPX int identity primary key not null,
	MADL int not null,
	NGAYXUAT date,
	NGUOINHAN nvarchar(50),	
	TONGTIEN int
	
	foreign key (MADL) references dbo.DAILY(MADL)	
)

go
insert into dbo.PHIEUXUAT(MADL,NGAYXUAT,NGUOINHAN,TONGTIEN)
values (1,'10-30-2017',N'Triệu Hoàng Phúc',50000)

go

create table CT_PHIEUXUAT
(
	MAPX int,
	MASACH int,
	GIA int,
	SOLUONG int,
	THANHTIEN int
	
	primary key (MAPX, MASACH)	
	foreign key (MAPX) references dbo.PHIEUXUAT(MAPX),
	foreign key (MASACH) references dbo.SACH(MASACH)
)

go
insert into dbo.CT_PHIEUXUAT(MAPX,MASACH,GIA,SOLUONG,THANHTIEN)
values (1,1,58000,2,116000)

go


create table PHIEUNHAP
(
	MAPN int identity primary key not null,
	MANXB int not null,
	NGAYNHAP date,
	NGUOIGIAO nvarchar(50),
	TONGTIEN int
	
	foreign key (MANXB) references dbo.NXB(MANXB)
)

go
insert into dbo.PHIEUNHAP(MANXB,NGAYNHAP,NGUOIGIAO,TONGTIEN)
values (1,'10-30-2017',N'Triệu Hoàng Phúc',50000)

go

create table CT_PHIEUNHAP
(
	MAPN int,
	MASACH int,
	GIA int,
	SOLUONG int,
	THANHTIEN int
	
	primary key (MAPN, MASACH)	
	foreign key (MAPN) references dbo.PHIEUNHAP(MAPN),
	foreign key (MASACH) references dbo.SACH(MASACH)
)

go
insert into dbo.CT_PHIEUNHAP(MAPN,MASACH,GIA,SOLUONG,THANHTIEN)
values (1,1,58000,10,116000)

go

create table PHIEUTHU
(
	MAPT int identity primary key not null,
	MADL int not null,
	NGAY date,
	TONGTIEN int

	foreign key (MADL) references dbo.DAILY(MADL)
)
go

create table CT_PHIEUTHU
(
	MAPT int,
	MASACH int,
	GIA int,
	SOLUONG int,
	THANHTIEN int
	
	primary key (MAPT, MASACH)
	foreign key(MAPT) references dbo.PHIEUTHU(MAPT),
	foreign key (MASACH) references dbo.SACH(MASACH)
)

go

create table PHIEUCHI
(
	MAPC int identity primary key not null,
	MANXB int not null,
	NGAY date,
	TONGTIEN int

	foreign key (MANXB) references dbo.NXB(MANXB)
)
go

create table CT_PHIEUCHI
(
	MAPC int,
	MASACH int,
	GIA int,
	SOLUONG int,
	THANHTIEN int
	
	primary key (MAPC, MASACH)
	foreign key(MAPC) references dbo.PHIEUCHI(MAPC),
	foreign key (MASACH) references dbo.SACH(MASACH)
)