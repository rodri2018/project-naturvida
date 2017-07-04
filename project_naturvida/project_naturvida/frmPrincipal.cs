using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_naturvida
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        public void viewProductos()
        {
            Productos frmProductos = new Productos();
            frmProductos.MdiParent = this;
            frmProductos.Show();
        }

        public void viewClientes()
        {
            Clientes frmClientes = new Clientes();
            frmClientes.MdiParent = this;
            frmClientes.Show();
        }

        public void viewFactura()
        {
            Factura frmFactura = new Factura();
            frmFactura.MdiParent = this;
            frmFactura.Show();
        }

        public void viewInventario()
        {
            Inventario frmInventario = new Inventario();
            frmInventario.MdiParent = this;
            frmInventario.Show();
        }

        public void ExitApplication()
        {
            // Display a message box asking users if they
            // want to exit the application.
            if (MessageBox.Show("Seguro que deseas salir?", "NaturVida",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                  == DialogResult.Yes)
            {
                MessageBox.Show("!!!... Gracias vuelva pronto ...!!!");
                Application.Exit();
            }
        }


        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewProductos();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewClientes();
        }

        private void facturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewFactura();
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewInventario();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("!!!... Gracias por usar este software - \n CopyRights 2017 © \n Marca Registrada ™ ® - NaturVida \n Created by: ROD Software ...!!!");
        }
    }
}
