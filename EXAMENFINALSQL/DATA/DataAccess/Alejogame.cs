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
        public int Actualizar(Jugador jdg)
        {
            int rowsAffected = 0;
            try
            {
                DialogResult result = MessageBox.Show("Deseas actualizar los datos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        string query = "UPDATE alejogame SET Nombre = @Nombre, Alias = @Alias, Estado_Partida = @Estado_Partida, Duracion_Partida = @Duracion_Partida, Username = @Username, Email = @Email WHERE Numero_Partida = @Numero_Partida";
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@Numero_Partida", jdg.Numero_Partida);
                            cmd.Parameters.AddWithValue("@Nombre", jdg.Nombre);
                            cmd.Parameters.AddWithValue("@Alias", jdg.Alias);
                            cmd.Parameters.AddWithValue("@Estado_Partida", jdg.Estado_Partida);
                            cmd.Parameters.AddWithValue("@Duracion_Partida", jdg.Duracion_Partida);
                            cmd.Parameters.AddWithValue("@Username", jdg.Username);
                            cmd.Parameters.AddWithValue("@Email", jdg.Email);

                            connection.Open();
                            rowsAffected = cmd.ExecuteNonQuery();
                        }
                    }

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Datos actualizados correctamente");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el registro para actualizar");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message);
            }

            return rowsAffected;
        }


        //Buscar por medio del numero de partida
        public DataTable BuscarporNumeroPartida(int numero_Partida)
        {
            DataTable juego = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM alejogame WHERE Numero_Partida = @numero_Partida";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@numero_Partida", numero_Partida);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(juego);
                    }
                }
            }
            return juego;

        }
        //Insertar con la clase Jugador
        public void Insertar(Jugador jdg)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro");
            }
            finally
            {
                connection.Close();
            }
        }
        //Eliminar un registro
        public void Eliminar(int numero_Partida)
        {
            try
            {
                DialogResult resultE = MessageBox.Show("Esta seguro que desea eliminar este registro?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultE == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "DELETE FROM alejogame WHERE numero_Partida = @numero_Partida";
                        using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@numero_Partida", numero_Partida);
                            int mensaje = cmd.ExecuteNonQuery();
                            if (mensaje == 0)
                            {
                                MessageBox.Show("El registro solicitado no existe");
                            }
                            else
                            {
                                MessageBox.Show("Se ha eliminado el registro correctamente");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar el registro");
            }
        }
    }
}
