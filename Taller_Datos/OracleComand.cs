using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Taller_Datos
{
  
  public  class OracleComand
    {

        Conexion con = new Conexion();
        public OracleComand()
        {
            con.Servidor = "localhost / orcl";
            con.Usuario = "taller";
            con.Password = "taller";
        }

        /// <summary>
        /// Ejecuta procedimiento almacenado y devuelve DataTable
        /// </summary>
        /// <param name="StoredProcedureName"></param>
        /// <param name="Data"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public DataTable ExecStoredProcedure(string StoredProcedureName, DataTable Data, Dictionary<string, string> Params = null)
        {
            try
            {
                bool conexion = con.AbrirConexion();
                if (conexion)
                {
                    var _cmd = new OracleCommand();
                    var _da = new OracleDataAdapter(_cmd);
                    _cmd.Connection = con.ObtenerConneccion();
                    _cmd.CommandText = StoredProcedureName;
                    _cmd.CommandType = CommandType.StoredProcedure;
                    if (Params != null)
                    {
                        foreach (var item in Params)
                        {
                            _cmd.Parameters.Add(item.Key, item.Value);
                        }
                    }

                    var _salida = new OracleParameter();
                    _salida.ParameterName = "SALIDA";
                    _salida.OracleDbType = OracleDbType.RefCursor;
                    _salida.Direction = ParameterDirection.Output;
                    _cmd.Parameters.Add(_salida);


                    _cmd.Connection.Open();
                    _cmd.ExecuteNonQuery();

                    _da.Fill(Data);

                    con.cerrarConexion();

                }

                return Data;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ha sucedido algo inesperado al interactuar con la base de datos \n" + ex.Message);
            }
        }

        /// <summary>
        /// Ejecuta procedimiento almacenado sin retorno
        /// </summary>
        /// <param name="StoredProcedure"></param>
        /// <param name="Params"></param>
        public void ExecStoredProcedure(string StoredProcedure, Dictionary<string, string> Params = null)
        {

            try
            {
                bool conexion = con.AbrirConexion();
                if (conexion)
                {
                    var _cmd = new OracleCommand();
                    _cmd.Connection = con.ObtenerConneccion();
                    _cmd.CommandText = StoredProcedure;
                    _cmd.CommandType = CommandType.StoredProcedure;
                    if (Params != null)
                    {
                        foreach (var item in Params)
                        {
                            _cmd.Parameters.Add(item.Key, item.Value);
                        }
                    }
                    _cmd.Connection.Open();
                    _cmd.ExecuteNonQuery();
                    con.cerrarConexion();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ha sucedido algo inesperado al interactuar con la base de datos \n" + ex.Message);
            }
        }

        /// <summary>
        /// Ejecuta procedimiento almacenado y devuelve un string
        /// </summary>
        /// <param name="StoredProcedure"></param>
        /// <param name="salida"></param>
        /// <param name="Params"></param>
        public void ExecStoredProcedure(string StoredProcedure, out string salida, Dictionary<string, string> Params = null)
        {
            try
            {
                var _cmd = new OracleCommand();
                _cmd.Connection = con.ObtenerConneccion();
                _cmd.CommandText = StoredProcedure;
                _cmd.CommandType = CommandType.StoredProcedure;
                if (Params != null)
                {
                    foreach (var item in Params)
                    {
                        _cmd.Parameters.Add(item.Key, item.Value);
                    }
                }

                _cmd.Parameters.Add(new OracleParameter("SALIDA", OracleDbType.Varchar2, 8, DBNull.Value, ParameterDirection.Output));
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                con.cerrarConexion();

                if (_cmd.Parameters.Contains("SALIDA"))
                {
                    salida = _cmd.Parameters["SALIDA"].Value.ToString();
                }
                else
                {
                    salida = null;
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ha sucedido algo inesperado al interactuar con la base de datos \n" + ex.Message);
            }
        }


    }
}
