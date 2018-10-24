using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private GestorBD.GestorBD GestorBDAcc;
        string cadSql;
        DataSet dsDist = new DataSet();
        DataSet dsPedido = new DataSet();
        DataSet dsPedidoLibro = new DataSet();
        Varios.Comunes comunes = new Varios.Comunes();
        const int OK = 1;


        private void Form1_Load(object sender, EventArgs e)
        {
            //1- Crea el objeto para acceder a la base de datos.
            GestorBDAcc = new GestorBD.GestorBD("Microsoft.ACE.OLEDB.12.0", "Admin", "",
                          "C:/Users/BALTAMIRG/ExamenViejo/BD2oPedLibros.accdb");

            //2- Obtiene datos de las materias.
            cadSql = "select distinct idPed from Pedido";                         //Consulta.
            GestorBDAcc.consBD(cadSql, dsPedido, "Pedido");           //Se ejecuta.
            comunes.cargaCombo(comboBox1, dsPedido, "Pedido", "idPed");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //2- Obtiene datos de las materias.
            cadSql = "select * from PedidoLibro where idPed="+comboBox1.SelectedItem.ToString();                         //Consulta.
            GestorBDAcc.consBD(cadSql, dsPedidoLibro, "PedidoLibro");           //Se ejecuta.
            dataGridView1.DataSource = dsPedidoLibro.Tables["PedidoLibro"];        //Muestra resultados.

            //2- Obtiene datos de las materias.
            cadSql = "select * from Distribuidor d, Pedido p where idPed=" + comboBox1.SelectedItem.ToString()+" and p.rfcDis = d.rfcDis";                         //Consulta.
            GestorBDAcc.consBD(cadSql, dsDist, "Distribuidor");           //Se ejecuta.
            dataGridView2.DataSource = dsDist.Tables["Distribuidor"];        //Muestra resultados.



        }

        private void button1_Click(object sender, EventArgs e)
        {
            int folio, idLib;
            DialogResult botón;
            String fecha, valorF;

            //Toma el folio de la calificación seleccionada en el data grid.
            folio = Convert.ToInt16(dataGridView1["idPed", dataGridView1.CurrentRow.Index].Value);
            idLib = Convert.ToInt16(dataGridView1["idLib", dataGridView1.CurrentRow.Index].Value);
            valorF = dataGridView1["fechaEnt", dataGridView1.CurrentRow.Index].Value.ToString();
            botón = MessageBox.Show("¿Modificaa el registro con idPed = " + folio.ToString(),
              "Eliminación", MessageBoxButtons.YesNo);
            //Construye la fecha para el formato de Oracle.
            fecha = "date'" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" +
              dateTimePicker1.Value.Day + "'";

            //Si se selecciona el botón Yes, del MessageBox, modifica el registro asociado.
            if (botón == DialogResult.Yes)
            {
                if (valorF.ToString().Equals(""))
                {
                    //Construye la cadena de mod y la envía para su ejecución.
                    cadSql = "update PedidoLibro set fechaEnt = " + fecha + " where idPed = " + folio + " and idLib =" + idLib;
                    if (GestorBDAcc.cambiaBD(cadSql) == OK)
                        MessageBox.Show("Se modifico el registro del folio " + folio + " exitosamente");
                    else
                        MessageBox.Show("No se pudo modificar- " + cadSql);
                }
                else
                {
                    MessageBox.Show("No se pudo modificar- ");

                }

            }
        }
    }
}
