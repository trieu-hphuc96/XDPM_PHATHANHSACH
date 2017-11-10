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

        [Key]
        public int MAPC { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn NXB!")]
        public int MANXB { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAY { get; set; }

        public int? TONGTIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUCHI> CT_PHIEUCHI { get; set; }

        public virtual NXB NXB { get; set; }
    }
}
