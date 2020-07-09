using System;
using System.Collections.Generic;
using System.Data;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Cliente_Negocio
    {
        public bool CrearCliente(string rut_cliente, string dv_cliente, string p_nombre_cliente, string s_nombre_cliente,
            string p_apellido_cliente, string s_apellido_cliente, string direccion_cliente, string numeracion_cliente, string dept_cliente,
            string fono_cliente, string correo_cliente, string fiado, string nombre_usu_cliente, string contrasena_cliente, string id_tipo_cliente,
            string id_comuna, string id_taller)
        {
            OracleComand exec = new OracleComand();
            Compartido_Negocio compartido = new Compartido_Negocio();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("v_rut_cliente", rut_cliente.ToString());
                Parameters.Add("v_dv_cliente", dv_cliente.ToString());
                Parameters.Add("v_p_nombre_cliente", p_nombre_cliente);
                Parameters.Add("v_s_nombre_cliente", s_nombre_cliente);
                Parameters.Add("v_p_apellido_cliente", p_apellido_cliente);
                Parameters.Add("v_s_apellido_cliente", s_apellido_cliente);
                Parameters.Add("v_direccion_cliente", direccion_cliente);
                Parameters.Add("v_numeracion_cliente", numeracion_cliente);
                Parameters.Add("v_dept_cliente", dept_cliente);
                Parameters.Add("v_fono_cliente", fono_cliente.ToString());
                Parameters.Add("v_correo_cliente", correo_cliente);
                Parameters.Add("v_fiado", fiado);
                Parameters.Add("v_nombre_usu_cliente", nombre_usu_cliente);
                Parameters.Add("v_contrasena_cliente", compartido.Encriptar(contrasena_cliente));
                Parameters.Add("v_id_tipo_cliente", id_tipo_cliente.ToString());
                Parameters.Add("v_id_comuna", id_comuna.ToString());
                Parameters.Add("v_id_taller", id_taller.ToString());
                exec.ExecStoredProcedure("SP_CREAR_CLIENTE", Parameters);
                respuesta = true;
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
                respuesta = false;
            }
            return respuesta;
        }
        public bool EliminarCliente(int ID_CLIENTE)
        {
            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("v_ID_CLIENTE", ID_CLIENTE.ToString());

                exec.ExecStoredProcedure("SP_DELETECLIENTE ", Parameters);
                respuesta = true;
            }
            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                respuesta = false;
            }
            return respuesta;
        }

        public DataTable ListarCliente(string RUT_CLIENTE)
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            try
            {

                Parameters.Add("v_rut_cliente;", RUT_CLIENTE);
                exec.ExecStoredProcedure("SP_LISTAR_CLIENTE", dataTable, Parameters);

                return dataTable;
            }
            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
            }
            return dataTable;
        }

        public List<Cliente_dto> ListarClienteCMB()
        {
            OracleComand exec = new OracleComand();

            List<Cliente_dto> retorno = new List<Cliente_dto>();

            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_CLIENTE", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Cliente_dto entidad = new Cliente_dto();
                    entidad.id_cliente = int.Parse(rows["ID_CLIENTE"].ToString());
                    entidad.nombre_cliente = rows["P_NOMBRE_CLIENTE"].ToString() + " " + rows["P_APELLIDO_CLIENTE"].ToString();
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
