using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Inf_Pedido_Negocio
    {
        public List<Inf_pedido_dto> ListarPedido(string id_prove, string id_emp, string fechaI, string fechaF )
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            List<Inf_pedido_dto> Listado = new List<Inf_pedido_dto>();
            
            fechaI = DateTime.Parse(fechaI).ToString("dd/MM/yyyy");
            fechaF = DateTime.Parse(fechaF).ToString("dd/MM/yyyy");

            try
            {
                Parameters.Add("v_idProve", id_prove.ToString());
                Parameters.Add("v_idEmpl", id_emp.ToString());
                Parameters.Add("v_fecha_Ini", fechaI.ToString());
                Parameters.Add("v_fecha_Fin", fechaF.ToString());
                 
                exec.ExecStoredProcedure("SP_INFORME_PEDIDO", dataTable, Parameters);
                foreach (DataRow item in dataTable.Rows)
                {
                    Inf_pedido_dto entidad = new Inf_pedido_dto();
                    

                   
                    //  entidad.id_pedido = item[""].ToString();
                    entidad.Empleado = (item["P_NOMBRE_EMPLEADO"].ToString());
                    entidad.IDPedido = int.Parse(item["ID_PEDIDO"].ToString());
                    entidad.Fecha_Recepcion = DateTime.Parse(item["FECHA_RECEP"].ToString()).ToString("dd/MM/yyyy");
                    entidad.IDRecepcion = int.Parse(item["ID_RECEP"].ToString());
                    entidad.Empresa = item["RAZON_SOCIAL_PROV"].ToString();
                    entidad.IDProducto = int.Parse(item["ID_PRODUCTO"].ToString());
                    entidad.Producto =  item["DESCR_PRODUCTO"].ToString();
                    entidad.Cantidad = int.Parse(item["CANTIDAD_RECEP_DT"].ToString());

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
