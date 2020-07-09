using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Producto_Negocio
    {

        public List<Producto_dto> ListarProducto(int IdTipoProd, string busca)
        {
            OracleComand exec = new OracleComand();
            List<Producto_dto> listado_Pro = new List<Producto_dto>();

            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("V_ID_TIPO_PRO", IdTipoProd.ToString());
                Parameters.Add("BUSCADOR_PROD", busca);
                exec.ExecStoredProcedure("SP_DDL_LISTAR_PRODUCTO", dataTable, Parameters);

                foreach (DataRow item in dataTable.Rows)
                {
                    Producto_dto entidad = new Producto_dto();
                    entidad.id_producto = int.Parse(item["id_producto"].ToString());
                    entidad.descr_producto = item["descr_producto"].ToString();
                    entidad.stock = int.Parse(item["stock"].ToString());
                    listado_Pro.Add(entidad);
                    entidad = null;
                }
            }
            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
            }
            return listado_Pro;
        }

        public DataTable Buscar_Prod_id(string id_prod)
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("V_ID_PROD;", id_prod);
                exec.ExecStoredProcedure("SP_LISTAR_PROD_ID", dataTable, Parameters);

                return dataTable;
            }
            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
            }
            return dataTable;
        }

        public bool Aumentar_cant_prod(string id_producto, string cantidad)
        {
            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("V_IDPRODUCTO", id_producto.ToString());
                Parameters.Add("V_NEW_CANTIDAD", cantidad.ToString());
                exec.ExecStoredProcedure("SP_AUMENTAR_STOCK", Parameters);
                respuesta = true;
            }

            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                respuesta = false;
            }
            return respuesta;
        }

        public bool Restar_cant_prod(string id_producto, string cantidad)
        {
            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("V_IDPRODUCTO", id_producto.ToString());
                Parameters.Add("V_CANTIDAD_MENOS", cantidad.ToString());
                exec.ExecStoredProcedure("SP_QUITAR_STOCK", Parameters);
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
