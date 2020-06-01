using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;
using System.Data;


namespace Taller_Negocio
{
    public class Generar_Pedido_Negocio
    {
        public string CrearPedidoHDR(string fecha_pedido, string estado_pedido, string id_prov_pedido, string id_empl_pedido)
        {

            OracleComand exec = new OracleComand();
            string id = "";
            DataTable respuesta = new DataTable();
            var Parameters = new Dictionary<string, string>();
            var Parameters2 = new Dictionary<string, string>();

            try
            {

                Parameters.Add("v_FECHA_PEDIDO", fecha_pedido.ToString());
                Parameters.Add("v_ID_ESTADO", estado_pedido.ToString());
                Parameters.Add("v_ID_PROVEEDOR", id_prov_pedido.ToString());
                Parameters.Add("v_ID_EMPLEADO", id_empl_pedido.ToString());
                exec.ExecStoredProcedure("SP_CREAR_PEDIDO_HDR", Parameters);
                exec.ExecStoredProcedure("SP_TRAE_ID_PEDIDO_HDR", respuesta, Parameters2);
                foreach (DataRow item in respuesta.Rows)
                {
                    id = item["ID_PEDIDO"].ToString();
                }
            }

            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                id = "";
            }
            return id;
        }


        public bool CrearPedidoDet(string cantidad_pedido_dt, string precio_pedido_dt, string total_pedido, string id_pedido, string id_producto)
        {

            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("v_CANTIDAD_PEDIDO_DT", cantidad_pedido_dt.ToString());
                Parameters.Add("v_PRECIO_PEDIDO_DT", precio_pedido_dt.ToString());
                Parameters.Add("v_TOTAL_PEDIDO", total_pedido.ToString());
                Parameters.Add("v_ID_PEDIDO", id_pedido.ToString());
                Parameters.Add("v_ID_PRODUCTO", id_producto.ToString());
                exec.ExecStoredProcedure("SP_CREAR_PEDIDO_DT", Parameters);
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
