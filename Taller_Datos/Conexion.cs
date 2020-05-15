using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Datos
{
    public class Conexion
    {
        private string _servidor;
        private string _usuario;
        private string _password;
        public string Servidor
        {
            get
            {
                return _servidor;
            }

            set
            {
                _servidor = "Data Source = " + value + ";"; ;
            }
        }
        public string Usuario
        {
            get
            {
                return _usuario;
            }

            set
            {
                _usuario = "user id=" + value + ";";
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = "password=" + value + ";";
            }
        }

        public OracleConnection coneccion;
        private static OracleConnection con = new OracleConnection();

        public bool AbrirConexion()
        {
            try
            {
                // string cadenaconexion = ConfigurationManager.ConnectionStrings["cadenaconexion"].ConnectionString;

                string stringconexion = Servidor + Usuario + Password;

                coneccion = new OracleConnection(stringconexion);

                return true;
            }
            catch
            {

                return false;
            }

        }
        public OracleConnection ObtenerConneccion()
        {
            try
            {
                string stringconexion = Servidor + Usuario + Password;
                return new OracleConnection(stringconexion);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al leer la configuración de la aplicación \n" + ex.Message);
            }
        }
        public bool cerrarConexion()
        {
            try
            {
                coneccion.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
