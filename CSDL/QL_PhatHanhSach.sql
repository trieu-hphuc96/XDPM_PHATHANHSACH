create database QuanLiPhatHanhSach

use QuanLiPhatHanhSach
go


create table NXB
(
	MANXB int identity primary key not null,
	TENNXB nvarchar(50),
	DIACHI nvarchar(50),
	SDT nvarchar(15),
	SOTK nvarchar(20)
)
go

insert into dbo.NXB(TENNXB,DIACHI,SDT,SOTK)
values(N'Nhà xuất bản A','Q6,HCM','0808123486','160620101234')
insert into dbo.NXB(TENNXB,DIACHI,SDT,SOTK)
values(N'Nhà xuất bản B','Q7,HCM','0808124785','160620105678')
insert into dbo.NXB(TENNXB,DIACHI,SDT,SOTK)
values(N'Nhà xuất bản C','Q8,HCM','0808127823','160620101234')
insert into dbo.NXB(TENNXB,DIACHI,SDT,SOTK)
values(N'Nhà xuất bản D','Q1,HCM','0808123428','160620109101')
insert into dbo.NXB(TENNXB,DIACHI,SDT,SOTK)
values(N'Nhà xuất bản E','Q2,HCM','0808124839','160620101213')

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

insert into dbo.SACH(TENSACH,MANXB,THELOAI,GIA)
values(N'Thay thái độ đổi cuộc đời',1,N'Văn học',58000)
insert into dbo.SACH(TENSACH,MANXB,THELOAI,GIA)
values(N'Tony buổi sáng',2,N'Văn học',68000)
insert into dbo.SACH(TENSACH,MANXB,THELOAI,GIA)
values(N'Nhà giả kim',3,N'Văn học',53000)
insert into dbo.SACH(TENSACH,MANXB,THELOAI,GIA)
values(N'Cô phù thủy kiki',4,N'Văn học',77000)
insert into dbo.SACH(TENSACH,MANXB,THELOAI,GIA)
values(N'Giai toán 5',5,N'Tham khảo',24000)
insert into dbo.SACH(TENSACH,MANXB,THELOAI,GIA)
values(N'Ngữ văn 10',1,N'Giao khoa',24000)
insert into dbo.SACH(TENSACH,MANXB,THELOAI,GIA)
values(N'Mac-Lenin',2,N'Giao khoa',42000)
insert into dbo.SACH(TENSACH,MANXB,THELOAI,GIA)
values(N'Clean code',3,N'Tham khảo',320000)
insert into dbo.SACH(TENSACH,MANXB,THELOAI,GIA)
values(N'Harry Potter',4,N'Tiểu thuyết',240000)
insert into dbo.SACH(TENSACH,MANXB,THELOAI,GIA)
values(N'ABCDEF',5,N'Khác',80000)
go

create table DAILY
(
	MADL int identity primary key not null,
	TENDL nvarchar(50),
	DIACHI nvarchar(50),
	TONGSOSACH int,
	TIENDATRA int,
	TIENCONNO int
)
go
alter table DAILY add NOCHOPHEP float default 800000

insert into dbo.DAILY(TENDL,DIACHI,TIENDATRA,TIENCONNO)
values (N'Đại lý A','Q1,HCM',0,0)
insert into dbo.DAILY(TENDL,DIACHI,TIENDATRA,TIENCONNO)
values (N'Đại lý B','Q2,HCM',0,0)
insert into dbo.DAILY(TENDL,DIACHI,TIENDATRA,TIENCONNO)
values (N'Đại lý C','Q3,HCM',0,0)
insert into dbo.DAILY(TENDL,DIACHI,TIENDATRA,TIENCONNO)
values (N'Đại lý D','Q4,HCM',0,0)
insert into dbo.DAILY(TENDL,DIACHI,TIENDATRA,TIENCONNO)
values (N'Đại lý E','Q5,HCM',0,0)
go

create table CT_DAILY
(
	MADL int,
	MASACH int,
	GIA int,
	SOLUONGLAY int,
	SOLUONGCON int
			
	foreign key (MADL) references dbo.DAILY(MADL),
	foreign key (MASACH) references dbo.SACH(MASACH)
)

go

create table PHIEUXUAT
(
	MAPX int identity primary key not null,
	MADL int,
	NGAYXUAT date default getdate(),
	NGUOINHAN nvarchar(50),	
	TONGTIEN int 
	
	foreign key (MADL) references dbo.DAILY(MADL)	
)
go

create table CT_PHIEUXUAT
(
	MAPX int,
	MASACH int,
	GIA int,
	SOLUONG int,
	THANHTIEN int
	
	foreign key (MAPX) references dbo.PHIEUXUAT(MAPX),
	foreign key (MASACH) references dbo.SACH(MASACH)
)
go

create table PHIEUNHAP
(
	MAPN int identity primary key not null,
	MANXB int,
	NGAYNHAP date default getdate(),
	NGUOIGIAO nvarchar(50),
	TONGTIEN int
	
	foreign key (MANXB) references dbo.NXB(MANXB)
)
go

create table CT_PHIEUNHAP
(
	MAPN int,
	MASACH int
	GIA int,
	SOLUONG int,
	THANHTIEN int
	
	foreign key (MAPN) references dbo.PHIEUNHAP(MAPN),
	foreign key (MASACH) references dbo.SACH(MASACH)
)
go

create table PHIEUTHANHTOAN
(
	MAPTT int identity primary key not null,
	NGAYTT date default getdate(),
	TONGTIEN int
)
go

create table CT_PHIEUTHANHTOAN
(
	MAPTT int,
	MASACH int,
	GIA int,
	SOLUONG int,
	THANHTIEN int
	
	foreign key(MAPTT) references dbo.PHIEUTHANHTOAN(MAPTT),
	foreign key (MASACH) references dbo.SACH(MASACH)
)
go