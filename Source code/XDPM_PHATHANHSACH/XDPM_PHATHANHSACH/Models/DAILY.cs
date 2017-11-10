namespace XDPM_PHATHANHSACH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DAILY")]
    public partial class DAILY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DAILY()
        {
            CT_DAILY = new HashSet<CT_DAILY>();
            PHIEUTHUs = new HashSet<PHIEUTHU>();
            PHIEUXUATs = new HashSet<PHIEUXUAT>();
        }

        [Key]
        public int MADL { get; set; }

        [StringLength(50)]
        public string TENDL { get; set; }

        [StringLength(50)]
        public string DIACHI { get; set; }

        public int? TONGSOSACH { get; set; }

        public int? TIENCONNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_DAILY> CT_DAILY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUTHU> PHIEUTHUs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUXUAT> PHIEUXUATs { get; set; }
    }
}
