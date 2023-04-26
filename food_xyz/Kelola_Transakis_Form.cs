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
    public partial class Kelola_Transakis_Form : Form
    {
        DataTable dt = new DataTable();
        int selisih;
        public Kelola_Transakis_Form()
        {
            InitializeComponent();
            initCmbMenu();
            init();
        }

        public void initCmbMenu()
        {
            DataTable data = Config.query("usp_list_barang ''");
            for (int i = 0; i < data.Rows.Count; i++)
            {
                cmbNamaMenu.Items.Add(data.Rows[i]["Nama_Barang"].ToString());
            }
        }

        public void setNamaKasir()
        {
            DataTable data = Config.query($"SELECT * FROM tbl_user WHERE id_user = '{Login_Form.id_user}'");
            lblNamaKasir.Text = data.Rows[0]["nama"].ToString();
        }

        public void setDgv()
        {
            dt.Columns.Add("no_transaksi");
            dt.Columns.Add("kode_barang");
            dt.Columns.Add("nama_barang");
            dt.Columns.Add("harga_satuan");
            dt.Columns.Add("quantitas");
            dt.Columns.Add("subtotal");

            dgv.DataSource = dt;
        }

        public void init()
        {
            setDgv();
            setNamaKasir();
            clear();
        }

        public void clear()
        {
            cmbNamaMenu.Text = "";
            txtHargaPerSatuan.Clear();
            txtQuantity.Clear();
            txtTotalHarga.Clear();
        }

        public void kalkulasi()
        {
            int total = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                total += Convert.ToInt32( dgv.Rows[i].Cells["subtotal"].Value.ToString() );
            }

            lblTotalHarga.Text = Convert.ToString(total);
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // validasi stok
            DataTable dataStok = Config.query($"SELECT * FROM tbl_barang WHERE nama_barang = '{cmbNamaMenu.Text}'");
            int stokSaatIni = Convert.ToInt32(dataStok.Rows[0]["jumlah_barang"].ToString());
            int jumlahYangAkanDibeli = Convert.ToInt32( txtQuantity.Text );
            if(stokSaatIni > jumlahYangAkanDibeli)
            {
                // check apakah barang sudah dimasukan keranjang sebelumnya jika iya hapus yang sebelumnya
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if(dgv.Rows[i].Cells["nama_barang"].Value.ToString() == cmbNamaMenu.Text)
                    {
                        dgv.Rows.RemoveAt(i);
                    }
                }

                DataTable dataBarang = Config.query($"SELECT * FROM tbl_barang WHERE nama_barang = '{cmbNamaMenu.Text}'");

                string kode_barang = dataBarang.Rows[0]["kode_barang"].ToString();
                string nama_barang = dataBarang.Rows[0]["nama_barang"].ToString();

                int total = Convert.ToInt32(txtHargaPerSatuan.Text) * Convert.ToInt32(txtQuantity.Text);

                dt.Rows.Add(DateTime.Now.ToString("yyyyMMddhhmm"), kode_barang, nama_barang, txtHargaPerSatuan.Text, txtQuantity.Text, total);
                dgv.DataSource = dt;

                clear();

                kalkulasi();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            setDgv();
        }

        private void cmbNamaMenu_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable data = Config.query($"SELECT * FROM tbl_barang WHERE nama_barang = '{cmbNamaMenu.Text}'");
            txtHargaPerSatuan.Text = data.Rows[0]["harga_satuan"].ToString();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtQuantity_MouseLeave(object sender, EventArgs e)
        {
            if(txtQuantity.Text != "")
            {
                int total = Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtHargaPerSatuan.Text);
                txtTotalHarga.Text = total.ToString();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(lblTotalHarga.Text);

            int bayar = Convert.ToInt32(txtBayar.Text);
            selisih = bayar - total;
            if(selisih >= 0)
            {
                lblKembali.ForeColor = Color.Green;
                lblKembali.Text = Convert.ToString( selisih );
            }
            else
            {
                lblKembali.ForeColor = Color.Green;
                lblKembali.Text = Convert.ToString(selisih);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(selisih >= 0)
            {
                /*dt.Columns.Add("id_transaksi");
                dt.Columns.Add("kode_barang");
                dt.Columns.Add("nama_barang");
                dt.Columns.Add("harga_satuan");
                dt.Columns.Add("quantitas");
                dt.Columns.Add("subtotal")*/

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    DataTable getIdBarang = Config.query($"SELECT * FROM tbl_barang WHERE nama_barang = '{dgv.Rows[i].Cells["nama_barang"].Value.ToString()}'");
                    string id_barang = getIdBarang.Rows[0]["id_barang"].ToString();

                    Config.query($"usp_insert_transaksi '{dgv.Rows[i].Cells["no_transaksi"].Value.ToString()}', '{DateTime.Now.ToString("yyyy-MM-dd")}', '{dgv.Rows[i].Cells["quantitas"].Value.ToString()}', '{dgv.Rows[i].Cells["subtotal"].Value.ToString()}', '{Login_Form.id_user}', '{id_barang}'");
                }

                MessageBox.Show("Berhasil membayar");
            }
            else
            {
                MessageBox.Show("Data Tidak Cukup");
            }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtHargaPerSatuan.Text);
            txtTotalHarga.Text = total.ToString();
        }
    }
}
