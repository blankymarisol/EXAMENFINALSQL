using EXAMENFINALSQL.DATA.MODEL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EXAMENFINALSQL.DATA.DataAccess
{
    internal class Alejogame
    {
        //Conexion para la base de datos
        string connectionString = "Server=localhost; Database=bd_universidad; Uid=root; Pwd=inge26sistemas.";
        MySqlConnection connection;
        public bool ProbarConexion()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true; 
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        //Mostrar los datos 
        public DataTable VerJugadores()
        {
            DataTable juego = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM alejogame";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(juego);
                    }
                }
            }
            return juego;
        }
        //Actualizar datos 
        public void Actualizar(int no_Partida, string nombre, string alias, string estado_Partida, string duracion_Partida, string username, string email)
        {
            try
            {
                string query = "UPDATE alejogame SET Nombre = @Nombre, Alias = @Alias, Estado_Partida = @Estado_Partida, Duracion_Partida = @Duracion_Partida, Username = @Username, Email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("No_Partida", no_Partida);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Alias", alias);
                cmd.Parameters.AddWithValue("@Estado_Partida", estado_Partida);
                cmd.Parameters.AddWithValue("@Duracion_Partida", duracion_Partida);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        
        //Buscar por medio del numero de partida
        public DataRow BuscarPorNumeroPartida(int no_Partida)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT * FROM alejogame WHERE No_Partida = @No_Partida";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@No_Partida", no_Partida);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                connection.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("El numero de partida es erroneo");
            }
            finally
            {
                connection.Close();
            }
            return dt.Rows[0];
        }
        //Insertar la clase usuario
        public void Insertar(Jugador jdg)
        {
            try
            {
                string query = "INSERT INTO alejogame (Nombre, Alias, Estado_Partida, Duracion_Partida, Username, Email) VALUES (@Nombre, @Alias, @Estado_Partida, @Duracion_Partida, @Username, @Email)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nombre", jdg.Nombre);
                cmd.Parameters.AddWithValue("@Alias", jdg.Alias);
                cmd.Parameters.AddWithValue("@Estado_Partida", jdg.Estado_Partida);
                cmd.Parameters.AddWithValue("@Duracion_Partida", jdg.Duracion_Partida);
                cmd.Parameters.AddWithValue("@Username", jdg.Username);
                cmd.Parameters.AddWithValue("@Email", jdg.Email);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
        }
    }
}
