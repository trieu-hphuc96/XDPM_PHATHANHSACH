using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XDPM_PHATHANHSACH.Models
{
    public class CT_NXB
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MANXB { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MASACH { get; set; }

        public int? GIA { get; set; }

        public int? SLLAY { get; set; }

        public int? SLDABAN { get; set; }

        public virtual SACH SACH { get; set; }

        public virtual NXB NXB { get; set; }
    }
}