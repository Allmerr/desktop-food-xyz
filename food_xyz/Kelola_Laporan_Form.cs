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
    public partial class Kelola_Laporan_Form : Form
    {
        public Kelola_Laporan_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable data = Config.query($"SELECT * FROM tbl_transaksi WHERE tgl_transaksi BETWEEN '{dtpFrom.Value.Date.ToString("yyyy-MM-dd")}' AND '{dtpTo.Value.Date.ToString("yyyy-MM-dd")}'");
            dgv.DataSource = data;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series["omset"].Points.Clear();

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                chart1.Series["omset"].Points.AddXY(dgv.Rows[i].Cells["tgl_transaksi"].Value.ToString(), dgv.Rows[i].Cells["total_bayar"].Value.ToString());
            }
        }
    }
}
