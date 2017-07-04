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
    public partial class Clientes : Form
    {
        //instancia la clase conexion
        Conexion conMysql = new Conexion();
        DataRow lstNombre = null;

        public Clientes()
        {
            InitializeComponent();
        }

        public void guardar()
        {
            if (textBox1.Text.Trim() == String.Empty && textBox2.Text.Trim() == String.Empty && textBox3.Text.Trim() == String.Empty
                && textBox4.Text.Trim() == String.Empty && textBox5.Text.Trim() == String.Empty)
            {
                MessageBox.Show("!!!... Error, los campos no pueden estar vacios ...!!!");
                return;
            }
            
            if (textBox1.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debes ingresar una cédula");
                return;
            }

            if (textBox2.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debes ingresar un nombre");
                return;
            }

            if (textBox3.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debes ingresar una dirección");
                return;
            }

            if (textBox4.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debes ingresar un teléfono");
                return;
            }

            if (textBox5.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debes ingresar un correo");
                return;
            }
            
            String sql = String.Format("insert into clientes(cedula,nombre,direccion,telefono,correo)" +
                          " values('{0}','{1}','{2}','{3}','{4}')",
                          textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim());

            try
            {

                if (conMysql.Query(sql) == 1)
                {
                    MessageBox.Show("!!!... Registro de cliente éxitoso ...!!!");
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
            textBox9.Text = "";
            textBox10.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            listView1.Clear();
        }

        public void editar()
        {
            String sql = String.Format("update clientes set cedula='{0}', nombre='{1}',direccion='{2}', telefono='{3}', correo='{4}' where id='{5}'",
                          textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim(), textBox9.Text.Trim(), textBox10.Text.Trim(),comboBox2.SelectedValue);
            try
            {

                if (conMysql.Query(sql) == 1)
                {
                    MessageBox.Show("!!!... Edicion de cliente éxitosa ...!!!");
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
            String sql = String.Format("delete from clientes where id= '{0}'", comboBox3.SelectedValue);

            if (MessageBox.Show("Seguro que deseas eliminar este cliente?", "Eliminar Cliente",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                  == DialogResult.Yes)
            {
                try
                {

                    if (conMysql.Query(sql) == 1)
                    {
                        MessageBox.Show("!!!... Cliente Eliminado con éxito ...!!!");
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show("!!!... ERROR, NO se pudo eliminar ...!!!");
                    }

                    limpiar();

                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }

        }

        public void addListView()
        {
             
            lstNombre = conMysql.getRow("select * from clientes where id='" + comboBox1.SelectedValue + "'");

            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("EL Cliente que intentas mostrar, ya no se encuentra en nuestra base de datos");
            }

            ListViewItem lvItem = new ListViewItem();

            lvItem.SubItems[0].Text = lstNombre[0].ToString();
            lvItem.SubItems.Add(lstNombre[1].ToString());
            lvItem.SubItems.Add(lstNombre[2].ToString());
            lvItem.SubItems.Add(lstNombre[3].ToString());
            lvItem.SubItems.Add(lstNombre[4].ToString());
            lvItem.SubItems.Add(lstNombre[5].ToString());
           
            listView1.Items.Add(lvItem);

        }

        public void buscar()
        {
            String sql = "select * from clientes where id = " + comboBox2.SelectedValue;
            DataRow fila = conMysql.getRow(sql);
            if (fila != null)
            {
                textBox6.Text = fila["cedula"].ToString();
                textBox7.Text = fila["nombre"].ToString();
                textBox8.Text = fila["direccion"].ToString();
                textBox9.Text = fila["telefono"].ToString();
                textBox10.Text = fila["correo"].ToString();
            }
            else
            {
                MessageBox.Show("El cliente que buscas no existe");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            //Propiedades del ListView
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Columnas
            listView1.Columns.Add("Id", 30, HorizontalAlignment.Left);
            listView1.Columns.Add("Cédula", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Nombre", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Dirección", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Teléfono", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Email", 100, HorizontalAlignment.Left);
            
            conMysql.Connect();
            String sql = "select id, nombre from clientes";
            conMysql.CargarCombo(comboBox1, sql, "nombre", "id");
            conMysql.CargarCombo(comboBox2, sql, "nombre", "id");
            conMysql.CargarCombo(comboBox3, sql, "nombre", "id");
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addListView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            editar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
