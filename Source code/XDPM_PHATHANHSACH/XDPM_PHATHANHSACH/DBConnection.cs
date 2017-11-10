using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDPM_PHATHANHSACH
{
    public class DBConnection
    {
        public static SqlConnection connectDB()
        {
            string str = @"data source=gamenet-pc\phuc1;initial catalog=QuanLyPhatHanhSach;user id=sa;password=123456789;MultipleActiveResultSets=True;App=EntityFramework";
            SqlConnection cn = new SqlConnection(str);
            cn.Open();
            return cn;
        }
    }
}
