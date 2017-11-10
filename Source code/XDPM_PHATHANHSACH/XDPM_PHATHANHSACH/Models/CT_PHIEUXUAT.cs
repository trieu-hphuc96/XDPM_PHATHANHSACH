namespace XDPM_PHATHANHSACH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_PHIEUXUAT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MAPX { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MASACH { get; set; }

        [Display(Name = "Giá")]
        public int? GIA { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng!")]
        [Display(Name = "Số lượng")]
        public int SOLUONG { get; set; }

        public int? THANHTIEN { get; set; }

        public virtual SACH SACH { get; set; }

        public virtual PHIEUXUAT PHIEUXUAT { get; set; }
    }
}
