using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;
using System.Data;
namespace Taller_Negocio
{
    public class Empleado_Negocio
    {
        /// <summary>
        /// Método que Guarda / Edita empledo
        /// </summary>
        /// <param name="rut_empleado"></param>
        /// <param name="dv_empleado"></param>
        /// <param name="p_nombre_empleado"></param>
        /// <param name="s_nombre_empleado"></param>
        /// <param name="p_apellido_empleado"></param>
        /// <param name="s_apellido_empleado"></param>
        /// <param name="direccion_empleado"></param>
        /// <param name="numeracion_empleado"></param>
        /// <param name="depto_empleado"></param>
        /// <param name="fono_empleado"></param>
        /// <param name="correo_empleado"></param>
        /// <param name="nombre_usu_empleado"></param>
        /// <param name="contrasena_empleado"></param>
        /// <param name="id_comuna"></param>
        /// <param name="id_taller"></param>
        /// <param name="id_cargo"></param>
        /// <returns></returns>
        public bool crearEmpleado(string rut_empleado, string dv_empleado, string p_nombre_empleado, string s_nombre_empleado, string p_apellido_empleado, 
            string s_apellido_empleado, string direccion_empleado, string numeracion_empleado, string depto_empleado, string fono_empleado, string correo_empleado, 
            string nombre_usu_empleado, string contrasena_empleado, string id_comuna, string id_taller, string id_cargo)
        {
            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters =new Dictionary<string, string>();
            try
            {
                Parameters.Add("v_rut_empleado", rut_empleado.ToString());
                Parameters.Add("v_dv_empleado", dv_empleado);
                Parameters.Add("v_p_nombre_empleado", p_nombre_empleado);
                Parameters.Add("v_s_nombre_empleado", s_nombre_empleado);
                Parameters.Add("v_p_apellido_empleado", p_apellido_empleado);
                Parameters.Add("v_s_apellido_empleado", s_apellido_empleado);
                Parameters.Add("v_direccion_empleado", direccion_empleado);
                Parameters.Add("v_numeracion_empleado", numeracion_empleado);
                Parameters.Add("v_dept_empleado", depto_empleado);
                Parameters.Add("v_fono_empleado", fono_empleado.ToString());
                Parameters.Add("v_correo_empleado", correo_empleado);
                Parameters.Add("v_nombre_usu_empleado", nombre_usu_empleado);
                Parameters.Add("v_contrasena_empleado", contrasena_empleado);
                Parameters.Add("v_id_comuna", id_comuna.ToString());
                Parameters.Add("v_id_taller", id_taller.ToString());
                Parameters.Add("v_id_cargo", id_cargo.ToString());
                exec.ExecStoredProcedure("SP_CREAR_EMPLEADO", Parameters);
                respuesta = true;
            }

            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                respuesta = false;
            }
            return respuesta;
        }
        /// <summary>
        /// Método que elimina Empleado
        /// </summary>
        /// <param name="ID_EMPLEADO"></param>
        /// <returns></returns>
        public bool EliminarEmp( int ID_EMPLEADO)
        {
            OracleComand exec = new OracleComand();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("v_ID_EMP", ID_EMPLEADO.ToString());

                exec.ExecStoredProcedure("SP_DELETEEMPLEADO ", Parameters);
                respuesta = true;
            }
            catch (Exception e)
            {
                string mensaje = e.Message.ToString();
                respuesta = false;
            }
            return respuesta;
        }
        /// <summary>
        /// Lista 1 o todos los empleados según el parametro que se pase (0 Todos // Rut = empleado)
        /// </summary>
        /// <param name="RUT_EMPLEADO"></param>
        /// <returns></returns>
        public DataTable ListarEmpleado(string RUT_EMPLEADO)
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            try
            {
               
                Parameters.Add("v_rut_empleado;", RUT_EMPLEADO);
                exec.ExecStoredProcedure("SP_LISTAR_EMPLEADO", dataTable, Parameters);

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
