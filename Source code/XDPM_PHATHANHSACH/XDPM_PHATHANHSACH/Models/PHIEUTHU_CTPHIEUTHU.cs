using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XDPM_PHATHANHSACH.Models
{
    public class PHIEUTHU_CTPHIEUTHU
    {
        public PHIEUTHU pt { get; set; }
        public CT_PHIEUTHU ctpt { get; set; }
        public List<CT_DAILY> ctdls { get; set; }
    }
}