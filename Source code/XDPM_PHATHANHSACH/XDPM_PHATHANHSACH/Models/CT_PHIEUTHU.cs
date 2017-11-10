namespace XDPM_PHATHANHSACH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_PHIEUTHU
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MAPT { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MASACH { get; set; }

        public int? GIA { get; set; }

        public int? SOLUONG { get; set; }

        public int? THANHTIEN { get; set; }

        public virtual SACH SACH { get; set; }

        public virtual PHIEUTHU PHIEUTHU { get; set; }
    }
}
