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
    public partial class Login_Form : Form
    {
        public static string id_user;
        public Login_Form()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            DataTable data = Config.query($"usp_login '{txtUsername.Text}', '{txtPassword.Text}' ");

            if(data.Rows.Count > 0)
            {
                id_user = data.Rows[0]["id_user"].ToString();

                switch (data.Rows[0]["tipe_user"].ToString())
                {
                    case "admin":
                        this.Hide();
                        Admin_Form nAdmin = new Admin_Form();
                        nAdmin.Show();
                        break;
                    case "gudang":
                        this.Hide();
                        Form_Kelola_Barang nGudang = new Form_Kelola_Barang();
                        nGudang.Show();
                        break;
                    case "kasir":
                        this.Hide();
                        Kelola_Transakis_Form nKasir = new Kelola_Transakis_Form();
                        nKasir.Show();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
