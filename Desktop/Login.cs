using Desktop.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            Connection con = new Connection();
            con.dataGet("select * from UserAccounts where UserLoginID='"+ txtname.Text +"'");
            DataTable dt = new DataTable();
            con.sda.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                this.Hide();
                frmMainForm frmMain = new frmMainForm();
                frmMain.Show();
            }
            else
            {
                MessageBox.Show("Invalid UserName & Password ..!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }
    }
}
