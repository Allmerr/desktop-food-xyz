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
    public partial class Form_Kelola_Barang : Form
    {
        public static string id_barang;
        public static string kode_barang_sebelumnya;
        public Form_Kelola_Barang()
        {
            InitializeComponent();
            init();
        }

        public void clear()
        {
            dtp.Value = DateTime.Now;
            txtKodeBarang.Clear();
            txtNamaBarang.Clear();
            txtJumlahBarang.Clear();
            cmbSatuan.Text = "";
            txtHargaPerSatuan.Clear();
        }

        public void init()
        {
            clear();
            DataTable data = Config.query("usp_list_barang ''");
            dgv.DataSource = data;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (txtKodeBarang.Text != "" && txtNamaBarang.Text != "" && txtJumlahBarang.Text != "" && cmbSatuan.Text != "" && txtHargaPerSatuan.Text != "")
            {
                DataTable isKodeBarangtaken = Config.query($"SELECT * FROM tbl_barang WHERE kode_barang = '{txtKodeBarang.Text}'");
                if (isKodeBarangtaken.Rows.Count > 0)
                {
                    MessageBox.Show("Gagal Menambah barang");
                }
                else
                {
                    Config.query($"usp_insert_barang '{txtKodeBarang.Text}', '{txtNamaBarang.Text}', '{dtp.Value.Date.ToString("yyyy-MM-dd")}', '{txtJumlahBarang.Text}', '{cmbSatuan.Text}', '{txtHargaPerSatuan.Text}'");

                    MessageBox.Show("Berhasil Menambah barang");

                    init();
                }
            }
            else
            {
                MessageBox.Show("Data Diri Kurang Lengkap");
            }
        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id_barang = dgv.Rows[e.RowIndex].Cells["id_barang"].Value.ToString();
            kode_barang_sebelumnya = dgv.Rows[e.RowIndex].Cells["kode_barang"].Value.ToString();

            txtKodeBarang.Text = dgv.Rows[e.RowIndex].Cells["kode_barang"].Value.ToString();
            txtNamaBarang.Text = dgv.Rows[e.RowIndex].Cells["nama_barang"].Value.ToString();
            dtp.Value = DateTime.Parse(dgv.Rows[e.RowIndex].Cells["expired_date"].Value.ToString());
            txtJumlahBarang.Text = dgv.Rows[e.RowIndex].Cells["jumlah_barang"].Value.ToString();
            cmbSatuan.Text = dgv.Rows[e.RowIndex].Cells["satuan"].Value.ToString();
            txtHargaPerSatuan.Text = dgv.Rows[e.RowIndex].Cells["harga_satuan"].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtKodeBarang.Text != "" && txtNamaBarang.Text != "" && txtJumlahBarang.Text != "" && cmbSatuan.Text != "" && txtHargaPerSatuan.Text != "")
            {
                Config.query($"usp_delete_barang '{id_barang}'");

                MessageBox.Show("Berhasil mengahapus barang");
                init();
            }
            else
            {
                MessageBox.Show("Data Diri Kurang Lengkap");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtKodeBarang.Text != "" && txtNamaBarang.Text != "" && txtJumlahBarang.Text != "" && cmbSatuan.Text != "" && txtHargaPerSatuan.Text != "")
            {

                DataTable isKodeBarangtaken = Config.query($"SELECT * FROM tbl_barang WHERE kode_barang = '{txtKodeBarang.Text}'");


                if (isKodeBarangtaken.Rows.Count > 0 && kode_barang_sebelumnya != txtKodeBarang.Text)
                {
                    MessageBox.Show("Gagal Menambah User");
                }
                else
                {
                    Config.query($"usp_update_barang '{id_barang}', '{txtKodeBarang.Text}', '{txtNamaBarang.Text}', '{dtp.Value.Date.ToString("yyyy-MM-dd")}', '{txtJumlahBarang.Text}' ,'{cmbSatuan.Text}', '{txtHargaPerSatuan.Text }'");

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
            DataTable data = Config.query($"usp_list_barang '{txtCari.Text}'");
            dgv.DataSource = data;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Config.query($"usp_insert_log 'logout', '{Login_Form.id_user}'");
            this.Hide();
            Login_Form nPage = new Login_Form();
            nPage.Show();
        }
    }
}
