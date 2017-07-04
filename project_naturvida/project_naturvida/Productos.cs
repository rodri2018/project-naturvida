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
    public partial class Productos : Form
    {
        //instancia la clase conexion
        Conexion conMysql = new Conexion();
        DataRow lstProducto = null;

        public Productos()
        {
            InitializeComponent();
        }

        public void limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            listView1.Clear();
        }

        public void guardar()
        {
            if (textBox1.Text.Trim() == String.Empty && textBox2.Text.Trim() == String.Empty && textBox3.Text.Trim() == String.Empty
                && textBox4.Text.Trim() == String.Empty)
            {
                MessageBox.Show("!!!... Error, los campos no pueden estar vacios ...!!!");
                return;
            }

            if (textBox1.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debes ingresar una código");
                return;
            }

            if (textBox2.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debes ingresar un descripción");
                return;
            }

            if (textBox3.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debes ingresar una valor");
                return;
            }

            if (textBox4.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debes ingresar un cantidad");
                return;
            }


            String sql = String.Format("insert into productos(codigo,descripcion,valor,cantidad_inicial)" +
                          " values('{0}','{1}','{2}','{3}')",
                          textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim());

            try
            {

                if (conMysql.Query(sql) == 1)
                {
                    MessageBox.Show("!!!... Registro de Producto éxitoso ...!!!");
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

        public void buscar()
        {
            String sql = "select * from productos where id = " + comboBox2.SelectedValue;
            DataRow fila = conMysql.getRow(sql);
            if (fila != null)
            {
                textBox5.Text = fila["codigo"].ToString();
                textBox6.Text = fila["descripcion"].ToString();
                textBox7.Text = fila["valor"].ToString();
                textBox8.Text = fila["cantidad_inicial"].ToString();
                comboBox2.Text = "";
            }
            else
            {
                MessageBox.Show("El producto que buscas no existe");
            }
        }

        public void addListView()
        {

            lstProducto = conMysql.getRow("select * from productos where id='" + comboBox1.SelectedValue + "'");

            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("EL Producto que intentas mostrar, ya no se encuentra en nuestra base de datos");
            }

            ListViewItem lvItem = new ListViewItem();

            lvItem.SubItems[0].Text = lstProducto[0].ToString();
            lvItem.SubItems.Add(lstProducto[1].ToString());
            lvItem.SubItems.Add(lstProducto[2].ToString());
            lvItem.SubItems.Add(lstProducto[3].ToString());
            lvItem.SubItems.Add(lstProducto[4].ToString());

            listView1.Items.Add(lvItem);

        }

        public void editar()
        {
            String sql = String.Format("update productos set codigo='{0}', descripcion='{1}', valor='{2}', cantidad_inicial='{3}' where id='{4}'",
                          textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim(), comboBox2.SelectedValue);
            try
            {

                if (conMysql.Query(sql) == 1)
                {
                    MessageBox.Show("!!!... Edicion de Producto éxitosa ...!!!");
                }
                else
                {
                    MessageBox.Show("!!!... ERROR, NO se pudo editar ...!!!");
                }

                limpiar();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void eliminar()
        {
            String sql = String.Format("delete from productos where id= '{0}'", comboBox3.SelectedValue);

            if (MessageBox.Show("Seguro que deseas eliminar este Producto?", "Eliminar Producto",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                  == DialogResult.Yes)
            {
                try
                {
                    if (conMysql.Query(sql) == 1)
                    {
                        MessageBox.Show("!!!... Producto Eliminado con éxito ...!!!");
                    }
                    else
                    {
                        MessageBox.Show("!!!... ERROR, NO se pudo eliminar ...!!!");
                    }

                    limpiar();
                    conMysql.CargarCombo(comboBox3, sql, "descripcion", "id");
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Productos_Load(object sender, EventArgs e)
        {
            //Propiedades del ListView
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Columnas
            listView1.Columns.Add("Id", 30, HorizontalAlignment.Left);
            listView1.Columns.Add("Código", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Descripción", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Valor", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Cantidad", 120, HorizontalAlignment.Left);

            //cargar comboBox
            conMysql.Connect();
            String sql = "select id, descripcion from productos";
            conMysql.CargarCombo(comboBox1, sql, "descripcion", "id");
            conMysql.CargarCombo(comboBox2, sql, "descripcion", "id");
            conMysql.CargarCombo(comboBox3, sql, "descripcion", "id");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addListView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            editar();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
