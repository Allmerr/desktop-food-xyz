using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace food_xyz
{
    public partial class Kelola_User_Form : Form
    {
        public static string id_user;
        public static string username_sebelumnya;

        public Kelola_User_Form()
        {
            InitializeComponent();
            init();
        }

        public void clear()
        {
            txtNama.Clear();
            txtAlamat.Clear();
            txtTelpon.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
        }

        public void init()
        {
            clear();
            DataTable data = Config.query("usp_list_user ''");
            dgv.DataSource = data;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if(cmbTipeUser.Text != "" && txtNama.Text != "" && txtTelpon.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                DataTable isUsernameTaken = Config.query($"SELECT * FROM tbl_user WHERE username = '{txtUsername.Text}'");
                if (isUsernameTaken.Rows.Count > 0)
                {
                    MessageBox.Show("Gagal Menambah User");
                }
                else
                {
                    Config.query($"usp_insert_user '{cmbTipeUser.Text}', '{txtNama.Text}', '{txtTelpon.Text}', '${txtAlamat.Text}' ,'{txtUsername.Text}', '{txtPassword.Text}'");

                    MessageBox.Show("Berhasil Menambah User");

                    init();
                }
            }
            else
            {
                MessageBox.Show("Data Diri Kurang Lengkap");
            }
        }

        private void btnKelolaUser_Click(object sender, EventArgs e)
        {

        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                id_user = dgv.Rows[e.RowIndex].Cells["id_user"].Value.ToString();
                username_sebelumnya = dgv.Rows[e.RowIndex].Cells["username"].Value.ToString();

                cmbTipeUser.Text = dgv.Rows[e.RowIndex].Cells["tipe_user"].Value.ToString();
                txtNama.Text = dgv.Rows[e.RowIndex].Cells["nama"].Value.ToString();
                txtAlamat.Text = dgv.Rows[e.RowIndex].Cells["alamat"].Value.ToString();
                txtTelpon.Text = dgv.Rows[e.RowIndex].Cells["telpon"].Value.ToString();
                txtUsername.Text = dgv.Rows[e.RowIndex].Cells["username"].Value.ToString();
                txtPassword.Text = dgv.Rows[e.RowIndex].Cells["password"].Value.ToString();
            }
            catch 
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cmbTipeUser.Text != "" && txtNama.Text != "" && txtTelpon.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                Config.query($"usp_delete_user '${id_user}'");

                MessageBox.Show("Berhasil mengahapus user");
            }
            else
            {
                MessageBox.Show("Data Diri Kurang Lengkap");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbTipeUser.Text != "" && txtNama.Text != "" && txtTelpon.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {

                DataTable isUsernameTaken = Config.query($"SELECT * FROM tbl_user WHERE username = '{txtUsername.Text}'");

                if (isUsernameTaken.Rows.Count > 0 && username_sebelumnya != txtUsername.Text)
                {
                    MessageBox.Show("Gagal Menambah User");
                }
                else
                {
                    Config.query($"usp_update_user '{id_user}', '{cmbTipeUser.Text}', '{txtNama.Text}', '{txtTelpon.Text}', '{txtAlamat.Text}' ,'{txtUsername.Text}', '{txtPassword.Text }'");

                    MessageBox.Show("Berhasil Menambah User");

                    init();
                }
            }
            else
            {
                MessageBox.Show("Data Diri Kurang Lengkap");
            }
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            DataTable data = Config.query($"usp_list_user '{txtCari.Text}'");
            dgv.DataSource = data;
        }
    }
}
