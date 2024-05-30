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
            int numeropartida = int.Parse(textBoxnumero.Text);
            DataTable partidaencontrada = juego.BuscarporNumeroPartida(numeropartida);
            if (partidaencontrada.Rows.Count > 0)
            {
                string nombre = partidaencontrada.Rows[0]["nombre"].ToString();
                string alias = partidaencontrada.Rows[0]["alias"].ToString();
                string estado_Partida = partidaencontrada.Rows[0]["estado_Partida"].ToString();
                string duracion_Partida = partidaencontrada.Rows[0]["duracion_Partida"].ToString();
                string username = partidaencontrada.Rows[0]["username"].ToString();
                string email = partidaencontrada.Rows[0]["email"].ToString();
                textBoxnombre.Text = nombre;
                textBoxalias.Text = alias;
                textBoxestado.Text = estado_Partida;
                textBoxduracion.Text = duracion_Partida;
                textBoxusername.Text = username;
                textBoxemail.Text = email;
            }
            else
            {
                MessageBox.Show("No se encontro el registro");
            }
        }
        private void buttonactualizar_Click(object sender, EventArgs e)
        {
            jdg.Numero_Partida = int.Parse(textBoxnumero.Text); // Asignar el valor de Numero_Partida
            jdg.Nombre = textBoxnombre.Text;
            jdg.Alias = textBoxalias.Text;
            jdg.Estado_Partida = textBoxestado.Text;
            jdg.Duracion_Partida = textBoxduracion.Text;
            jdg.Username = textBoxusername.Text;
            jdg.Email = textBoxemail.Text;
            int rowsAffected = Clscone.Actualizar(jdg);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Datos actualizados correctamente");
            }
            else
            {
                MessageBox.Show("No se encontró el registro para actualizar");
            }
        }

        

        private void buttoncrear_Click(object sender, EventArgs e)
        {
            try
            {
                jdg.Nombre = textBoxnombre.Text;
                jdg.Alias = textBoxalias.Text;
                jdg.Estado_Partida = textBoxestado.Text;
                jdg.Duracion_Partida = textBoxduracion.Text;
                jdg.Username = textBoxusername.Text;
                jdg.Email = textBoxemail.Text;
                Clscone.Insertar(jdg);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha ingresado correctamente");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxnumero.Text = " ";
            textBoxnombre.Text = "";
            textBoxalias.Text = "";
            textBoxestado.Text = "";
            textBoxduracion.Text = "";
            textBoxusername.Text = "";
            textBoxemail.Text = "";
        }

        private void buttoneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(textBoxnumero.Text, out int Numero_Partida))
                {
                    Clscone.Eliminar(Numero_Partida);
                    Jugador jdg = new Jugador();
                }
                else
                {
                    MessageBox.Show("El numero de partida no existe");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error");
            }
        }

        private void buttonsalir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas cerrar este formulario?", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Cierra el formulario actual
            }
        }
    }
}
