namespace XDPM_PHATHANHSACH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SACH")]
    public partial class SACH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SACH()
        {
            CT_DAILY = new HashSet<CT_DAILY>();
            CT_PHIEUCHI = new HashSet<CT_PHIEUCHI>();
            CT_PHIEUNHAP = new HashSet<CT_PHIEUNHAP>();
            CT_PHIEUTHU = new HashSet<CT_PHIEUTHU>();
            CT_PHIEUXUAT = new HashSet<CT_PHIEUXUAT>();
        }

        [Display(Name = "Mã Sách")]
        [Key]
        public int MASACH { get; set; }

        [Display(Name = "Mã NXB")]
        public int? MANXB { get; set; }

        [Display(Name = "Tên sách")]
        [StringLength(50)]
        public string TENSACH { get; set; }

        [Display(Name = "Tác giả")]
        [StringLength(50)]
        public string TACGIA { get; set; }

        [Display(Name = "Thể loại")]
        [StringLength(50)]
        public string THELOAI { get; set; }

        [Display(Name = "Số lượng")]
        public int SOLUONG { get; set; }

        [Display(Name = "Giá")]
        public int GIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_DAILY> CT_DAILY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_NXB> CT_NXB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUCHI> CT_PHIEUCHI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUNHAP> CT_PHIEUNHAP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUTHU> CT_PHIEUTHU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUXUAT> CT_PHIEUXUAT { get; set; }

        public virtual NXB NXB { get; set; }
    }
}
