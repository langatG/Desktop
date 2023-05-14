using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.db
{
    class Connection
    {
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataAdapter sda;
        public string pkk;

        public void connection()
        {
            con = new SqlConnection(@"Data Source=.;Initial Catalog=Maziwa;Integrated Security=true");
            //con = new SqlConnection(@"Data Source=198.38.85.55;Initial Catalog=MORINGA;Integrated Security=false;User Id=atm;Password=atm;MultipleActiveResultSets=true");
            con.Open();
        }
        public void dataSend(string sql)
        {
            try {
                connection();
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                pkk = "";
            }
            catch(Exception) {
                pkk = "Please check your data";
            }   
            con.Close();
        }
        public void dataGet(string sql)
        {
            try
            {
                connection();
                sda = new SqlDataAdapter(sql, con);
            }
            catch (Exception)
            {
            }
        }
    }
}
