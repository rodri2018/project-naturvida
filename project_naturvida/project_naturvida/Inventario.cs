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
    public partial class Inventario : Form
    {
        Conexion conMysql = new Conexion();

        public Inventario()
        {
            InitializeComponent();
        }

        public void addProducto()
        {
            String sql2 = "select codigo,valor,cantidad_inicial from productos where id=" + comboBox1.SelectedValue;
            DataRow fila = conMysql.getRow(sql2);

            String sql3 = (@"select cantidad as can_Vendidas from factura_detalle inner join productos on factura_detalle.producto_id=productos.id where productos.id="+comboBox1.SelectedValue);
            DataRow cant_vendidas = conMysql.getRow(sql3);

            if (sql3 == null)
            {
                MessageBox.Show("No hay ventas de este producto");
            }
            else
            {
                int canVen = (int)cant_vendidas[0];
                var canIni = (int)fila[2];
                int canFinal = canIni - canVen;
                dataGridView1.Rows.Add(comboBox1.SelectedValue, fila[0], comboBox1.Text, fila[1], fila[2], cant_vendidas[0], canFinal);
            }   
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            // Create an unbound DataGridView by declaring a column count.
            dataGridView1.ColumnCount = 7;
            dataGridView1.ColumnHeadersVisible = true;


            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Set the column header names.
            dataGridView1.Columns[0].Name = "Id_Producto";
            dataGridView1.Columns[1].Name = "Codigo";
            dataGridView1.Columns[2].Name = "Descripcion";
            dataGridView1.Columns[3].Name = "Valor";
            dataGridView1.Columns[4].Name = "Cant. Inicial";
            dataGridView1.Columns[5].Name = "Cant. Vendidas";
            dataGridView1.Columns[6].Name = "Cant. Final";


            // Set the column header width.
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 120;

            //cargar comboBox
            conMysql.Connect();
            String sql = "select id,descripcion from productos";
            conMysql.CargarCombo(comboBox1, sql, "descripcion", "id");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addProducto();
        }
    }
}
