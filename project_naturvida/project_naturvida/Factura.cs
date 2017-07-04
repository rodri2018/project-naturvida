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

    public partial class Factura : Form
    {
        Conexion conMysql = new Conexion();
        
        public Factura()
        {
            InitializeComponent();
        }

        public void addProducto()
        {
            String sql3 = "select valor from productos where id = " + comboBox2.SelectedValue;
            DataRow valor_unit = conMysql.getRow(sql3);
            
            int valor_producto = 0;
            int cantidad = int.Parse(textBox1.Text);
            var v_unit = (int)valor_unit[0];

            valor_producto = v_unit * cantidad;

            dataGridView1.Rows.Add(comboBox1.SelectedValue, comboBox2.SelectedValue, v_unit, cantidad, valor_producto);
        }

        public void totalFactura()
        {
            //Variable donde almacenaremos el resultado de la sumatoria.
            double sumatoria = 0;
            //Método con el que recorreremos todas las filas de nuestro Datagridview
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //Aquí seleccionaremos la columna que contiene los datos numericos.
                sumatoria += Convert.ToDouble(row.Cells["Total_producto"].Value);
            }
            //Por ultimo asignamos el resultado a el texto de nuestro Label
            textBox2.Text = Convert.ToString(sumatoria);
        }

        public void limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            dataGridView1.DataSource = "";
        }

        public void guardar()
        {
            if (textBox1.Text.Trim() == String.Empty && textBox2.Text.Trim() == String.Empty )
            {
                MessageBox.Show("!!!... Error, los campos no pueden estar vacios ...!!!");
                return;
            }

            if (textBox1.Text.Trim() == String.Empty )
            {
                MessageBox.Show("!!!... Error, el campo cantidad no puede estar vacio ...!!!");
                return;
            }

            if (textBox2.Text.Trim() == String.Empty)
            {
                MessageBox.Show("!!!... Error, el campo total factura no puede estar vacio ...!!!");
                return;
            }

            dateTimePicker1.Value = DateTime.Now;
            
            String sql = String.Format("insert into facturas (fecha,cliente_id,valor_total)" +
                          " values('{0}','{1}','{2}')",
                          dateTimePicker1.Value.ToString("yyyy-MM-dd"), comboBox1.SelectedValue, textBox2.Text.Trim());
            try
            {
                if (conMysql.Query(sql) == 1)
                {
                    MessageBox.Show("!!!... Registro de Factura éxitoso ...!!!");
                }
                else
                {
                    MessageBox.Show("!!!... ERROR, NO se pudo registar ...!!!");
                }
                
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            DataRow busq1 = conMysql.getRow("select max(id) from facturas");
            String sql2 = String.Format("insert into factura_detalle (factura_id,producto_id,cantidad)" +
                          " values('{0}','{1}','{2}')",
                          busq1[0], comboBox2.SelectedValue, textBox1.Text.Trim());
            try
            {
                if (conMysql.Query(sql2) == 1)
                {
                    MessageBox.Show("!!!... Registro de Factura-Detalle éxitoso \n" +
                                    " Número Factura: " + busq1[0] + "\n" +
                                    " Cliente: " + comboBox1.Text + "\n" +
                                    " Producto: " + comboBox2.Text + "\n" +
                                    " Cantidad: " + textBox1.Text + "\n" +
                                    " Total Factura: " + textBox2.Text + "\n" +
                                    " ...!!!");
                }
                else
                {
                    MessageBox.Show("!!!... ERROR, NO se pudo registar ...!!!");
                }
                limpiar();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Factura_Load(object sender, EventArgs e)
        {
            // Create an unbound DataGridView by declaring a column count.
            dataGridView1.ColumnCount = 5;
            dataGridView1.ColumnHeadersVisible = true;
            

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Set the column header names.
            dataGridView1.Columns[0].Name = "Id_Cliente";
            dataGridView1.Columns[1].Name = "Id_Producto";
            dataGridView1.Columns[2].Name = "Valor_Unit.";
            dataGridView1.Columns[3].Name = "Cant.";
            dataGridView1.Columns[4].Name = "Total_producto";

            // Set the column header width.
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[4].Width = 140;
            
            //cargar comboBox
            conMysql.Connect();
            String sql = "select id, nombre from clientes";
            String sql2 = "select id,descripcion from productos";
            conMysql.CargarCombo(comboBox1, sql, "nombre", "id");
            conMysql.CargarCombo(comboBox2, sql2, "descripcion", "id");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addProducto();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            totalFactura();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            guardar();
        }
    }
}
