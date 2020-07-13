using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Reserva_Negocio
    {

        public DataTable ListarReservaPorIDCliente(string ID_RESERVA)
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("V_ID_RESERVA", ID_RESERVA);
                exec.ExecStoredProcedure("SP_OBTENER_RESERVA_ID", dataTable, Parameters);

                return dataTable;
            }
            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
            }
            return dataTable;
        }

        public DataTable ListarReservaPorRutCliente(string RUT_RESERVA)
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("v_rut_cliente", RUT_RESERVA);
                exec.ExecStoredProcedure("SP_OBTENER_RESERVA_RUT", dataTable, Parameters);

                return dataTable;
            }
            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
            }
            return dataTable;
        }

       
    }
}
