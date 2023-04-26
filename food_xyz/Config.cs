using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace food_xyz
{
    class Config
    {
        public static SqlConnection conn = new SqlConnection("Data Source=DESKTOP-5U91FSN;Initial Catalog=food_xyz;Integrated Security=True;");
        public static DataTable query(string perintah)
        {
            SqlCommand cmd = new SqlCommand(perintah, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
    }
}
