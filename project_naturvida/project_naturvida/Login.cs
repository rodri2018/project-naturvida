using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace project_naturvida
{
    public partial class Login : Form
    {
        Conexion conMysql = new Conexion();

        public Login()
        {
            InitializeComponent();
            try
            {
                conMysql.Connect();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        public void ingresar()
        {
            String sql1 = String.Format(@"select usuario,contraseña from users where usuario='" + textBox1.Text + "' and contraseña='" + textBox2.Text + "'");
            DataRow fila = conMysql.getRow(sql1);
            if (fila != null)
            {
                MessageBox.Show("!!!... Bienvenido a NaturVida " + textBox1.Text + " ...!!!");
                frmPrincipal abrir = new frmPrincipal();
                abrir.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("!!!... Error, usuario y/o contraseña invalidos, verifique ...!!!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ingresar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("!!!... Gracias por usar este software - Te esperamos pronto - NaturVida ...!!!");
            this.Close();
        }
    }
}
