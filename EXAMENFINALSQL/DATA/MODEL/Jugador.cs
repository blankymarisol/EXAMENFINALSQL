using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EXAMENFINALSQL.DATA.MODEL
{
    internal class Jugador
    {
        public int No_Partida { get; set; }
        public string Nombre { get; set; }
        public string Alias { get; set; }
        public string Estado_Partida { get; set; }
        public string Duracion_Partida { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public Jugador() { }
        public Jugador(int no_Partida, string nombre, string alias, string estado_Partida, string duracion_Partida, string username, string email) 
        {
            No_Partida = no_Partida;
            Nombre = nombre;
            Alias = alias;
            Estado_Partida = estado_Partida;
            Duracion_Partida = duracion_Partida;
            Username = username;
            Email = email;
        }

        public override string ToString()
        {
            return $"No_Partida: {No_Partida}, Nombre: {Nombre}, Alias: {Alias}, Estado_Partida: {Estado_Partida}, Username: {Username}, Email: {Email}";
        }
    }
}
