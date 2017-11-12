use QuanLyPhatHanhSach

----------------------------------------------------------------------------------------------
go
--lấy thông tin công nợ đại lý theo ngày
IF object_id('sp_thongkeCongNoDaiLy') IS NOT NULL drop proc sp_thongkeCongNoDaiLy

go
create proc sp_thongkeCongNoDaiLy
			@ngay date
as
	select distinct dl.MADL,dl.TENDL,dl.DIACHI,COALESCE(TongNo.TienNo,0) as TienNo,COALESCE(TongCong.TienCong,0) as TienCong
	from DAILY dl 
	left join (select MADL,COALESCE(SUM(TONGTIEN),0) as TienNo
					from PHIEUXUAT px 
					where NGAYXUAT < dateadd(day,1,@ngay)
					group by MADL) TongNo
	on dl.MADL = TongNo.MADL
	left join (select MADL,sum(TONGTIEN) as TienCong
				from PHIEUTHU
				where NGAY < dateadd(day,1,@ngay)
				group by MADL) TongCong
	on dl.MADL = TongCong.MADL


----------------------------------------------------------------------------------------------
go
--lấy thống kê tồn kho sách theo ngày
IF object_id('sp_layThongKeTonKho') IS NOT NULL drop proc sp_layThongKeTonKho

go
create proc sp_layThongKeTonKho
			@ngay date
as
	select s.*, COALESCE(soluongnhap- soluongxuat,0) as soluongton
	from SACH s
	left join (select s.MASACH, sum(ctpn.SOLUONG) as soluongnhap
				from sach s, PHIEUNHAP pn, CT_PHIEUNHAP ctpn
				where NGAYNHAP < dateadd(day,1,@ngay) and pn.MAPN=ctpn.MAPN and ctpn.MASACH = s.MASACH
				group by s.MASACH) as slnhap
	on s.MASACH = slnhap.MASACH
	left join (select s.MASACH, sum(ctpn.SOLUONG) as soluongxuat
				from sach s, PHIEUXUAT pn, CT_PHIEUXUAT ctpn
				where NGAYXUAT < dateadd(day,1,@ngay) and pn.MAPX=ctpn.MAPX and ctpn.MASACH = s.MASACH
				group by s.MASACH) as slxuat
	on s.MASACH = slxuat.MASACH

----------------------------------------------------------------------------------------------
go
--lấy thống kê công nợ nxb theo ngày
IF object_id('sp_thongkeCongNoNXB') IS NOT NULL drop proc sp_thongkeCongNoNXB

go
create proc sp_thongkeCongNoNXB
			@ngay date
as
	select distinct nxb.MANXB,nxb.TENNXB,nxb.DIACHI,nxb.SDT,COALESCE(TongNo.TienNo,0) as TienNo,COALESCE(TongCong.TienCong,0) as TienCong
	from NXB nxb 
	left join (select MANXB,COALESCE(SUM(TONGTIEN),0) as TienNo
				from PHIEUNHAP px
				where NGAYNHAP < dateadd(day,1,@ngay)
				group by MANXB) TongNo
	on nxb.MANXB = TongNo.MANXB
	left join (select MANXB,sum(TONGTIEN) as TienCong
				from PHIEUCHI
				where NGAY < dateadd(day,1,@ngay)
				group by MANXB) TongCong
	on nxb.MANXB = TongCong.MANXB
----------------------------------------------------------------------------------------------
go
--lấy phiếu thu theo khoảng thời gian
IF object_id('sp_layPhieuThu') IS NOT NULL drop proc sp_layPhieuThu

go
create proc sp_layPhieuThu
			@tungay date,
			@denngay date
as
	select dl.MADL,dl.TENDL,COALESCE(pt.TienThu,0) as TienThu
	from DAILY dl
	left join (select MADL, sum(TONGTIEN) as TienThu
				from PHIEUTHU
				where Ngay >= @tungay and Ngay < dateadd(day,1,@denngay)
				group by MADL) pt
	on dl.MADL = pt.MADL
	order by TienThu desc

----------------------------------------------------------------------------------------------
go
--lấy phiếu chi theo khoảng thời gian
IF object_id('sp_layPhieuChi') IS NOT NULL drop proc sp_layPhieuChi

go
create proc sp_layPhieuChi
			@tungay date,
			@denngay date
as
	select nxb.MANXB,nxb.TENNXB,COALESCE(pc.TienChi,0) as TienChi
	from NXB nxb
	left join (select MANXB, sum(TONGTIEN) as TienChi
				from PHIEUCHI
				where Ngay >= @tungay and Ngay < dateadd(day,1,@denngay)
				group by MANXB) pc
	on nxb.MANXB = pc.MANXB
	order by TienChi desc