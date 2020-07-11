using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
    public  class Deuda_Negocio
    {
        public List<EstadoDeuda_dto> ListarEstadoDeuda()
        {
            OracleComand exec = new OracleComand();

            List<EstadoDeuda_dto> retorno = new List<EstadoDeuda_dto>();

            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_Est_Deuda", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    EstadoDeuda_dto entidad = new EstadoDeuda_dto();
                    entidad.id_estado = int.Parse(rows["ID_ESTADO_DEUDA"].ToString());
                    entidad.desc_estado = rows["DESCR_ESTADO_DEUDA"].ToString();
                    retorno.Add(entidad);
                    entidad = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public string CrearDeuda(string fecha, string estado, string monto, string id_venta, string id_cliente)
        {
            OracleComand exec = new OracleComand();
            string id = "";
            DataTable respuesta = new DataTable();
            var Parameters = new Dictionary<string, string>();
            var Parameters2 = new Dictionary<string, string>();
            try
            {
                var totalFormat = Math.Round(decimal.Parse(monto), 0);
                Parameters.Add("v_FECHA", fecha.ToString());
                Parameters.Add("v_ID_ESTADO", estado.ToString());
                Parameters.Add("v_MONTO", totalFormat.ToString());
                Parameters.Add("v_ID_VENTA", id_venta.ToString());
                Parameters.Add("v_ID_CLIENTE", id_cliente.ToString());
                exec.ExecStoredProcedure("SP_CREAR_DEUDA", Parameters);
                exec.ExecStoredProcedure("SP_TRAE_ID_DEUDA", respuesta, Parameters2);
                foreach (DataRow item in respuesta.Rows)
                {
                    id = item["ID_DEUDA"].ToString();
                }
            }

            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                id = "";
            }
            return id;
        }

        public DataTable ObterneRDeuda(string ID_venta)
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            try
            {

                Parameters.Add("v_IDVENTA;", ID_venta.ToString());
                exec.ExecStoredProcedure("SP_OBTENER_DEUDA", dataTable, Parameters);

                return dataTable;
            }
            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
            }
            return dataTable;
        }

        public bool Cambiar_Estado_Deuda(string id_venta, string estado)
        {
            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("V_IDVENTA", id_venta.ToString());
                Parameters.Add("V_NEW_ESTADO", estado.ToString());
                exec.ExecStoredProcedure("SP_CAMBIAR_ESTADO_DEUDA", Parameters);
                respuesta = true;
            }

            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                respuesta = false;
            }
            return respuesta;
        }




    }
}
