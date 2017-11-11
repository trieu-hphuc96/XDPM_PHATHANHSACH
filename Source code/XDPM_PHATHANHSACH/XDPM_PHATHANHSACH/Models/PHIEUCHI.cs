namespace XDPM_PHATHANHSACH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUCHI")]
    public partial class PHIEUCHI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUCHI()
        {
            CT_PHIEUCHI = new HashSet<CT_PHIEUCHI>();
        }

        [Display(Name = "MÃ PC")]
        [Key]
        public int MAPC { get; set; }

        [Display(Name = "MÃ NXB")]
        [Required(ErrorMessage = "Vui lòng chọn NXB!")]
        public int MANXB { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày chi!")]
        [Display(Name = "Ngày chi")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NGAY { get; set; }

        [Display(Name = "Tổng tiền")]
        public int? TONGTIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUCHI> CT_PHIEUCHI { get; set; }

        public virtual NXB NXB { get; set; }
    }
}
