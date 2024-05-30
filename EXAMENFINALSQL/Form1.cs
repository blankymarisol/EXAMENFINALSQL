using EXAMENFINALSQL.DATA.DataAccess;
using EXAMENFINALSQL.DATA.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EXAMENFINALSQL
{
    public partial class Form1 : Form
    {
        Alejogame Clscone = new Alejogame();
        Jugador jdg = new Jugador();
        CLS cursor1 = new CLS();

        private Alejogame juego;
        public Form1()
        {
            InitializeComponent();
            juego = new Alejogame();
        }

        private void buttonprobar_Click(object sender, EventArgs e)
        {
            if (juego.ProbarConexion())
            {
                MessageBox.Show("Conexion Exitosa");
            }
            else
            {
                MessageBox.Show("Conexion Fallida");
            }
        }

        private void buttonver_Click(object sender, EventArgs e)
        {
            DataTable dt = juego.VerJugadores();
            dataGridView1.DataSource = dt;
        }

        private void buttonbuscar_Click(object sender, EventArgs e)
        {
            DataRow resp = Clscone.BuscarPorNumeroPartida(int.Parse(textBoxnumero.Text));
            if (resp != null)
            {
                textBoxnombre.Text = resp["Nombre"].ToString();
                textBoxalias.Text = resp["Alias"].ToString();
                textBoxestado.Text = resp["Estado_Partida"].ToString();
                textBoxduracion.Text = resp["Duracion_Partida"].ToString();
                textBoxusername.Text = resp["Username"].ToString();
                textBoxemail.Text = resp["Email"].ToString();
            }
            else
            {
                MessageBox.Show("No se encontro el registro");
            }
        }

        private void buttonactualizar_Click(object sender, EventArgs e)
        {
            Clscone.Actualizar(int.Parse(textBoxnumero.Text), textBoxnombre.Text, textBoxalias.Text, textBoxestado.Text, textBoxduracion.Text, textBoxusername.Text, textBoxemail.Text);
        }

        private void buttoncrear_Click(object sender, EventArgs e)
        {
            jdg.Nombre = textBoxnombre.Text;
            jdg.Alias = textBoxalias.Text;
            jdg.Estado_Partida = textBoxestado.Text;
            jdg.Duracion_Partida = textBoxduracion.Text;
            jdg.Username = textBoxusername.Text;
            jdg.Email = textBoxemail.Text;
            Clscone.Insertar(jdg);
        }
    }
}
