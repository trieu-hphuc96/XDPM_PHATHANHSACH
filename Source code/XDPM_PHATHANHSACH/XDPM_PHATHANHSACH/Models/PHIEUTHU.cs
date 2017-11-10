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

        [Key]
        public int MAPT { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn đại lý!")]
        public int MADL { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAY { get; set; }

        public int? TONGTIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUTHU> CT_PHIEUTHU { get; set; }

        public virtual DAILY DAILY { get; set; }
    }
}
