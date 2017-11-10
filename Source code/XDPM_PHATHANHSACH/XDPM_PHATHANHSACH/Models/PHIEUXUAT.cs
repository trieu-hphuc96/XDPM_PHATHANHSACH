namespace XDPM_PHATHANHSACH.Models
{
    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

    [Table("PHIEUXUAT")]
    public partial class PHIEUXUAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUXUAT()
        {
            CT_PHIEUXUAT = new HashSet<CT_PHIEUXUAT>();
            NGAYXUAT = DateTime.Today;
        }

        [Display(Name = "Mã PX")]
        [Key]
        public int MAPX { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn đại lý!")]
        [Display(Name = "Mã Đại Lý")]
        public int MADL { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày xuất!")]
        [Display(Name = "Ngày xuất")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NGAYXUAT { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập người nhận!")]
        [Display(Name = "Người nhận")]
        [StringLength(50)]
        public string NGUOINHAN { get; set; }

        [Display(Name = "Tổng tiền")]
        public int? TONGTIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUXUAT> CT_PHIEUXUAT { get; set; }

        public virtual DAILY DAILY { get; set; }
    }
}
