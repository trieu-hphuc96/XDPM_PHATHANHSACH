namespace XDPM_PHATHANHSACH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUTHU")]
    public partial class PHIEUTHU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUTHU()
        {
            CT_PHIEUTHU = new HashSet<CT_PHIEUTHU>();
        }

        [Display(Name = "Mã PT")]
        [Key]
        public int MAPT { get; set; }

        [Display(Name = "Mã Đại Lý")]
        [Required(ErrorMessage = "Vui lòng chọn đại lý!")]
        public int MADL { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày thu!")]
        [Display(Name = "Ngày thu")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NGAY { get; set; }

        [Display(Name = "Tổng tiền")]
        public int? TONGTIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUTHU> CT_PHIEUTHU { get; set; }

        public virtual DAILY DAILY { get; set; }
    }
}
