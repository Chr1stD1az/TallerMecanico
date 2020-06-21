using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Recepcionar_Pedido_Negocio
    {
        public string CrearRecepcionHDR(string fecha_recepcion, string estado_recep, string id_pedido, string id_empl, string proveedor, string total_recepcion)
        {

            OracleComand exec = new OracleComand();
            string id = "";
            DataTable respuesta = new DataTable();
            var Parameters = new Dictionary<string, string>();
            var Parameters2 = new Dictionary<string, string>();

            try
            {
                var totalFormat = Math.Round(decimal.Parse(total_recepcion), 0);

                Parameters.Add("v_FECHA_RECEP", fecha_recepcion.ToString());
                Parameters.Add("v_ID_ESTADO", estado_recep.ToString());
                Parameters.Add("v_ID_PEDIDO", id_pedido.ToString());
                Parameters.Add("v_ID_EMPLEADO", id_empl.ToString());
                Parameters.Add("v_ID_PROVEEDOR", proveedor.ToString());
                Parameters.Add("v_TOTAL_RECEP", totalFormat.ToString());
                exec.ExecStoredProcedure("SP_CREAR_RECEPCION_HDR", Parameters);
                exec.ExecStoredProcedure("SP_TRAE_ID_RECEPCION_HDR", respuesta, Parameters2);
                foreach (DataRow item in respuesta.Rows)
                {
                    id = item["ID_RECEP"].ToString();
                }
            }

            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                id = "";
            }
            return id;
        }


        public bool CrearRecepcionDet(string cantidad_recep_dt, string precio_recep_dt, string sku_recep_pedido_dt, string id_recep_hdr_dt, string id_prod_recep_dt, string total_recep_dt)
        {

            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {

                Parameters.Add("v_CANTIDAD_RECEP_DT", cantidad_recep_dt.ToString());
                Parameters.Add("v_PRECIO_COMPRA", precio_recep_dt.ToString());
                Parameters.Add("v_SKU_PROD_RECEP", sku_recep_pedido_dt.ToString());
                Parameters.Add("v_ID_RECEP", id_recep_hdr_dt.ToString());
                Parameters.Add("v_ID_PRODUCTO", id_prod_recep_dt.ToString());
                Parameters.Add("v_TOTAL_RECEP_DT", total_recep_dt.ToString());
                exec.ExecStoredProcedure("SP_CREAR_RECEPCION_DT", Parameters);
                respuesta = true;
            }

            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                respuesta = false;
            }
            return respuesta;
        }


        public DataTable ObternerPedidoHDR(string ID_PEDIDO)
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            try
            {

                Parameters.Add("v_IDPEDIDO;", ID_PEDIDO.ToString());
                exec.ExecStoredProcedure("SP_OBTENER_PEDIDO_DHR", dataTable, Parameters);

                return dataTable;
            }
            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
            }
            return dataTable;
        }

        
        public List<Recepcion_pedido_dto> ListarDetallePedido(string id_pedido)
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            List<Recepcion_pedido_dto> Listado = new List<Recepcion_pedido_dto>();

            try
            {
                Parameters.Add("v_IDPEDIDODET", id_pedido.ToString());
                exec.ExecStoredProcedure("SP_OBTENER_PEDIDO_DET", dataTable, Parameters);
                foreach (DataRow item in dataTable.Rows)
                {
                    
                    Recepcion_pedido_dto entidad = new Recepcion_pedido_dto();
                    //  entidad.id_pedido = item[""].ToString();
                    entidad.Familia = (item["DESCR_FAMILIA"].ToString());
                    entidad.Tipo = (item["DESCR_TIPO_PROD"].ToString());

                    entidad.Descripción = (item["DESCR_PRODUCTO"].ToString());
                    entidad.IDProducto = int.Parse(item["ID_PRODUCTO"].ToString());
                    entidad.Cantidad = int.Parse(item["CANTIDAD_PEDIDO_DT"].ToString());
                    entidad.Recibido = int.Parse(item["CANTIDAD_PEDIDO_DT"].ToString());
                    entidad.Precio = int.Parse(item["PRECIO_PEDIDO_DT"].ToString()).ToString("n2");

                    entidad.Total = int.Parse(item["TOTAL_PEDIDO"].ToString()).ToString("n2");

                    Listado.Add(entidad);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Listado;

        }

        public bool Cambiar_Estado_Pedido(string id_pedido, string estado)
        {
            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("V_IDPEDIDO", id_pedido.ToString());
                Parameters.Add("V_NEW_ESTADO", estado.ToString());
                exec.ExecStoredProcedure("SP_CAMBIAR_ESTADO_PED", Parameters);
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
