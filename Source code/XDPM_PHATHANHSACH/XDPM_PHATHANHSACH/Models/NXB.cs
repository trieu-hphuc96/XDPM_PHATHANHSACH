namespace XDPM_PHATHANHSACH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NXB")]
    public partial class NXB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NXB()
        {
            PHIEUCHIs = new HashSet<PHIEUCHI>();
            PHIEUNHAPs = new HashSet<PHIEUNHAP>();
            SACHes = new HashSet<SACH>();
        }

        [Display(Name = "Mã NXB")]
        [Key]
        public int MANXB { get; set; }

        [Display(Name = "Tên NXB")]
        [StringLength(50)]
        public string TENNXB { get; set; }

        [StringLength(50)]
        public string DIACHI { get; set; }

        [StringLength(15)]
        public string SDT { get; set; }

        [StringLength(20)]
        public string SOTK { get; set; }

        public int? TIENCONNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_NXB> CT_NXB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUCHI> PHIEUCHIs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAP> PHIEUNHAPs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SACH> SACHes { get; set; }


    }
}
