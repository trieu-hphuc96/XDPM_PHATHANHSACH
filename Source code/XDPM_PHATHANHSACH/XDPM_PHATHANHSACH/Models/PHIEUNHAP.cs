namespace XDPM_PHATHANHSACH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUNHAP")]
    public partial class PHIEUNHAP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUNHAP()
        {
            CT_PHIEUNHAP = new HashSet<CT_PHIEUNHAP>();
            NGAYNHAP = DateTime.Today;
        }

        [Display(Name = "Mã PN")]
        [Key]
        public int MAPN { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn NXB!")]
        [Display(Name = "Mã NXB")]
        public int MANXB { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày nhập!")]
        [Display(Name = "Ngày nhập")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NGAYNHAP { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập người giao hàng!")]
        [Display(Name = "Người giao")]
        [StringLength(50)]
        public string NGUOIGIAO { get; set; }

        [Display(Name = "Tổng tiền")]
        public int? TONGTIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUNHAP> CT_PHIEUNHAP { get; set; }

        public virtual NXB NXB { get; set; }
    }
}
