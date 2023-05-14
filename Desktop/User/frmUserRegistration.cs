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

namespace Desktop.User
{
    public partial class frmUserRegistration : Form
    {
        Connection con = new Connection();
        public frmUserRegistration()
        {
            InitializeComponent();
        }

        private void txtname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtname.Text.Length > 0)
                    txtusername.Focus();
                else
                    txtname.Focus();
            }
        }

        private void frmUserRegistration_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtname;
            btndelete.Enabled = false;
            btnupdate.Enabled = false;
            loadData();
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtusername.Text.Length > 0)
                    txtpassword.Focus();
                else
                    txtusername.Focus();
            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (txtpassword.Text.Length > 0)
                    txtaddress.Focus();
                else
                    txtpassword.Focus();
            }
        }

        private void txtaddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtaddress.Text.Length > 0)
                    txtemail.Focus();
                else
                    txtaddress.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtemail.Text.Length > 0)
                    txtdate.Focus();
                else
                    txtemail.Focus();
            }
        }

        private void cborole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbrole.Text.Length > 0)
                    btnsave.Focus();
                else
                    cmbrole.Focus();
            }
        }
        private void ClearData()
        {
            txtname.Clear();
            txtusername.Clear();
            txtpassword.Clear();
            txtemail.Clear();
            txtaddress.Clear();
            txtdate.Value = DateTime.Now;
            cmbrole.SelectedIndex = -1;
            btnsave.Enabled = true;
            btndelete.Enabled = false;
            btnupdate.Enabled = false;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                if (IfUserNameExists(txtusername.Text))
                {
                    MessageBox.Show("User Name Already Exist");
                }
                else
                {

                    con.dataSend("SET DATEFORMAT DMY Insert Into UserAccounts(UserName, UserLoginID, Passwords, UserGroup, PassExpire, DateCreated, SUPERUSER)" +
                        " Values ('" + txtname.Text + "','" + txtusername.Text + "','" + txtpassword.Text + "','" + cmbrole.Text + "','90','" + txtdate.Value.ToString("dd/MM/yyy") + "','1')");
                    MessageBox.Show("Record Saved Succewssfully....!");
                    ClearData();
                    loadData();
                }
            }
        }
        private bool validation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(txtname.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtname, "Name Required");
            }
            else if (string.IsNullOrEmpty(txtusername.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtusername, "User Name Required");
            }
            else if (string.IsNullOrEmpty(txtpassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtpassword, "Password Required");
            }
            else if (txtpassword.Text.Length < 4)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtpassword, "Password Minimum of 4 Characters Required");
            }
            else if (string.IsNullOrEmpty(txtemail.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtemail, "Email Required");
            }
            else if (cmbrole.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cmbrole, "Select Role is Required");
            }
            else
            {
                errorProvider1.Clear();
                result = true;
            }
            return result;
        }
        private bool IfUserNameExists(string userName)
        {
            con.dataGet("select 1 from UserAccounts where UserLoginID='" + userName + "'");
            DataTable dt = new DataTable();
            con.sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;

        }
        private void loadData()
        {
            con.dataGet("select * from UserAccounts order by USERid desc");
            DataTable dt = new DataTable();
            con.sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["dgSno"].Value = n + 1;
                //, , Passwords, , PassExpire, , SUPERUSER
                dataGridView1.Rows[n].Cells["dgName"].Value = row["UserName"].ToString();
                dataGridView1.Rows[n].Cells["dgDob"].Value = Convert.ToDateTime(row["DateCreated"].ToString());
                dataGridView1.Rows[n].Cells["dgEmail"].Value = row["UserGroup"].ToString(); ;
                dataGridView1.Rows[n].Cells["dgUserName"].Value = row["UserLoginID"].ToString(); ;
                dataGridView1.Rows[n].Cells["dgRole"].Value = row["UserGroup"].ToString(); ;
                dataGridView1.Rows[n].Cells["dgAddress"].Value = row["UserGroup"].ToString(); ;

            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            txtname.Text = dataGridView1.SelectedRows[0].Cells["dgName"].Value.ToString();
            txtusername.Text = dataGridView1.SelectedRows[0].Cells["dgUserName"].Value.ToString();
            txtemail.Text = dataGridView1.SelectedRows[0].Cells["dgEmail"].Value.ToString();
            txtaddress.Text = dataGridView1.SelectedRows[0].Cells["dgAddress"].Value.ToString();
            cmbrole.Text = dataGridView1.SelectedRows[0].Cells["dgRole"].Value.ToString();
            txtdate.Text = dataGridView1.SelectedRows[0].Cells["dgDob"].Value.ToString();

            btnsave.Enabled = false;
            btndelete.Enabled = true;
            btnupdate.Enabled = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure You Want To Update", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                con.dataSend("SET DATEFORMAT DMY Update UserAccounts set UserName='" + txtname.Text + "', Passwords='" + txtpassword.Text + "', UserGroup='" + cmbrole.Text + "', DateCreated='" + txtdate.Value.ToString("dd/MM/yyy") + "' where UserLoginID='" + txtusername.Text + "'");
                MessageBox.Show("Updated Succewssfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearData();
                loadData();
                btnsave.Enabled = true;
                btndelete.Enabled = false;
                btnupdate.Enabled = false;
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure You Want To Delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                con.dataSend("SET DATEFORMAT DMY Delete from UserAccounts  where UserLoginID='" + txtusername.Text + "'");
                MessageBox.Show("Delete Succewssfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                ClearData();
                loadData();
                btnsave.Enabled = true;
                btndelete.Enabled = false;
                btnupdate.Enabled = false;
            }
        }
    }
}
