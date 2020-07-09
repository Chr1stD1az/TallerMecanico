using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Venta_Negocio
    {
        public string CrearVentaHDR(string fecha_vt, string IVA_vt, string total_vt, string id_cliente_vt, string id_docucmento_vt, string id_taller_vt)
        {
            OracleComand exec = new OracleComand();
            string id = "";
            DataTable respuesta = new DataTable();
            var Parameters = new Dictionary<string, string>();
            var Parameters2 = new Dictionary<string, string>();

            try
            {
                var totalFormatIva = Math.Round(decimal.Parse(IVA_vt), 0);
                var totalFormat = Math.Round(decimal.Parse(total_vt), 0);

                Parameters.Add("v_fecha_venta", fecha_vt.ToString());
                Parameters.Add("v_iva_venta", totalFormatIva.ToString());
                Parameters.Add("v_total_venta", totalFormat.ToString());
                Parameters.Add("v_id_cliente", id_cliente_vt.ToString());
                Parameters.Add("v_id_documento", id_docucmento_vt.ToString());
                Parameters.Add("v_id_taller", id_taller_vt.ToString());
                exec.ExecStoredProcedure("SP_CREAR_VENTA", Parameters);
                exec.ExecStoredProcedure("SP_TRAE_ID_VENTA", respuesta, Parameters2);
                foreach (DataRow item in respuesta.Rows)
                {
                    id = item["ID_VENTA"].ToString();
                }
            }
            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                id = "";
            }
            return id;
        }

        public bool CrearVentaDet(string cantidad_vt, string precio_prod_vt, string id_hdr_vt, string total_prod_vt, string tipo, string id_prod_serv)
        {

            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("V_CANTIDAD", cantidad_vt.ToString());
                Parameters.Add("v_PRECIO", precio_prod_vt.ToString());
                Parameters.Add("v_ID_VENTA", id_hdr_vt.ToString());
                Parameters.Add("v_TOTAL_DETALLE", total_prod_vt.ToString());
                Parameters.Add("v_TIPO", tipo.ToString());
                Parameters.Add("v_ID_PROD_SERV", id_prod_serv.ToString());

                exec.ExecStoredProcedure("SP_CREAR_VENTA_DETALLE", Parameters);
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
