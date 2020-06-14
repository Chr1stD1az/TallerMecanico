using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Compartido_Negocio
    {
        public string Encriptar(string _cadena)
        {


            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadena);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public  string DecrytedString(string _stringToDecrypt)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_stringToDecrypt);
            result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            return result;
        }
        public List<Familia_Prod_dto> ListarFamProd()
        {
            OracleComand exec = new OracleComand();

            List<Familia_Prod_dto> retorno = new List<Familia_Prod_dto>();

            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_FAM_PROD", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Familia_Prod_dto entidad = new Familia_Prod_dto();
                    entidad.id_familia_prod = int.Parse(rows["id_familia_prod"].ToString());
                    entidad.descr_familia = rows["descr_familia"].ToString();
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

        public List<Tipo_Producto_dto> ListarTipoProd(int IdTipoFamilia)
        {
            OracleComand exec = new OracleComand();

            List<Tipo_Producto_dto> retorno = new List<Tipo_Producto_dto>();

            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                Parameters.Add("V_ID_FAM_PRO", IdTipoFamilia.ToString());
                exec.ExecStoredProcedure("SP_DDL_LISTAR_TIPO_PROD", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Tipo_Producto_dto entidad = new Tipo_Producto_dto();
                    entidad.id_tipo_prod = int.Parse(rows["id_tipo_prod"].ToString());
                    entidad.descr_tipo_prod = rows["descr_tipo_prod"].ToString();
                    entidad.id_familia_prod = int.Parse(rows["id_familia_prod"].ToString());
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

        public List<Estado_Pedido_dto> ListarEstadoPedido()
        {
            OracleComand exec = new OracleComand();
            List<Estado_Pedido_dto> retorno = new List<Estado_Pedido_dto>();
            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_ESTADO", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Estado_Pedido_dto entidad = new Estado_Pedido_dto();
                    entidad.id_estado = int.Parse(rows["id_estado"].ToString());
                    entidad.desc_estado = rows["desc_estado"].ToString();
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

        public List<empleado_dto> ListarEmpleado()
        {
            OracleComand exec = new OracleComand();
            List<empleado_dto> retorno = new List<empleado_dto>();
            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_EMPLEADO", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    empleado_dto entidad = new empleado_dto();
                    entidad.id_empleado = int.Parse(rows["id_empleado"].ToString());
                    entidad.p_nombre_empleado = rows["p_nombre_empleado"].ToString() +" "+ rows["p_apellido_empleado"].ToString();
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
        public List<Proveedor_dto> ListarProveedor()
        {
            OracleComand exec = new OracleComand();
            List<Proveedor_dto> retorno = new List<Proveedor_dto>();
            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_PROVEE", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Proveedor_dto entidad = new Proveedor_dto();
                    entidad.id_proveedor = int.Parse(rows["id_proveedor"].ToString());
                    entidad.razon_social_prov = rows["razon_social_prov"].ToString();
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


    }
}
