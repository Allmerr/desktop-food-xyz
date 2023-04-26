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
    public partial class Admin_Form : Form
    {
        public Admin_Form()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            DataTable data = Config.query($"SELECT * FROM tbl_log");
            dgv.DataSource = data;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Config.query($"usp_insert_log 'logout', '{Login_Form.id_user}'");
            this.Hide();
            Login_Form nPage = new Login_Form();
            nPage.Show();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DataTable data = Config.query($"SELECT * FROM tbl_log WHERE waktu BETWEEN '{dtp.Value.Date.ToString("yyyy-MM-dd")}' AND '{dtp.Value.Date.ToString("yyyy-MM-dd")}'");
            dgv.DataSource = data;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnKelolaUser_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kelola_User_Form nPage = new Kelola_User_Form();
            nPage.Show();
        }

        private void btnKelolaLaporan_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kelola_Laporan_Form nPage = new Kelola_Laporan_Form();
            nPage.Show();
        }
    }
}
