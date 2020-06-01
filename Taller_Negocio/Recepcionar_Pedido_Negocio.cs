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
                    entidad.Producto = int.Parse(item["ID_PRODUCTO"].ToString());
                    entidad.Cantidad = int.Parse(item["CANTIDAD_PEDIDO_DT"].ToString());
                    entidad.Cantidad_Recepcionada = 0;
                    entidad.Precio = int.Parse(item["PRECIO_PEDIDO_DT"].ToString());
                    entidad.Total = int.Parse(item["TOTAL_PEDIDO"].ToString());

                    Listado.Add(entidad);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Listado;

        }
    }
}
