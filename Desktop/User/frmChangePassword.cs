using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.User
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void txtname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtname.Text.Length > 0)
                    txtoldpassword.Focus();
                else
                    txtname.Focus();
            }
        }
        private void txtoldpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtoldpassword.Text.Length > 0)
                    txtnewpassword.Focus();
                else
                    txtoldpassword.Focus();
            }
        }
        private void txtnewpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtnewpassword.Text.Length > 0)
                    txtconfirmpassword.Focus();
                else
                    txtnewpassword.Focus();
            }
        }
        private void txtconfirmpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtconfirmpassword.Text.Length > 0)
                    btnsave.Focus();
                else
                    txtconfirmpassword.Focus();
            }
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtname;
        }
    }
}
