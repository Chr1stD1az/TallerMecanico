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
        public bool CrearVentaHDR(string fecha_vt, string IVA_vt, string total_vt, string id_cliente_vt, string id_docucmento_vt, string id_taller_vt)
        {

            OracleComand exec = new OracleComand();

            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();

            try
            {

                Parameters.Add("v_fecha_venta", fecha_vt.ToString());
                Parameters.Add("v_iva_venta", IVA_vt.ToString());
                Parameters.Add("v_total_venta", total_vt.ToString());
                Parameters.Add("v_id_cliente", id_cliente_vt.ToString());
                Parameters.Add("v_id_documento", id_docucmento_vt.ToString());
                Parameters.Add("v_id_taller", id_taller_vt.ToString());
                exec.ExecStoredProcedure("SP_CREAR_VENTA", Parameters);
                respuesta = true;
            }

            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                respuesta = false;
            }
            return respuesta;
        }

        public bool CrearVentaDet(string cantidad_vt, string precio_prod_vt, string id_hdr_vt, string id_servicio_vt, string id_producto_vt, string total_prod_vt)
        {

            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("V_CANTIDAD", cantidad_vt.ToString());
                Parameters.Add("v_PRECIO", precio_prod_vt.ToString());
                Parameters.Add("v_ID_VENTA", id_hdr_vt.ToString());
                Parameters.Add("v_ID_SERVICIO", id_servicio_vt.ToString());
                Parameters.Add("v_IDPRODUCTO", id_producto_vt.ToString());
                Parameters.Add("v_TOTAL_DETALLE", total_prod_vt.ToString());

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
