using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;
using System.Data;


namespace Taller_Negocio
{
    public class Proveedor_Negocio
    {

        public bool CrearProveedor(string razon_social, string giro, string rut_prov, string dv_prov, string direccion_prov, string numeracion_prov,
                                   string fono_prov, string correo_prov, string nombre_usu_prov, string contrasena_prov, string comuna_prov)
        {

            OracleComand exec = new OracleComand();
            Compartido_Negocio compartido = new Compartido_Negocio();
            bool respuesta = false;
            var Parameters = new Dictionary<string, string>();
            try
            {
                
                Parameters.Add("v_razon_social_prov", razon_social);
                Parameters.Add("v_giro_prov", giro);
                Parameters.Add("v_rut_prov", rut_prov.ToString());
                Parameters.Add("v_dv_prov", dv_prov.ToString());
                Parameters.Add("v_direccion_prov", direccion_prov);
                Parameters.Add("v_numeracion_prov", numeracion_prov);
                Parameters.Add("v_fono_prov", fono_prov.ToString());
                Parameters.Add("v_correo_prov", correo_prov);
                Parameters.Add("v_nombre_usu_prov", nombre_usu_prov);
                Parameters.Add("v_contrasena_prov", compartido.Encriptar(contrasena_prov));
                Parameters.Add("v_id_comuna", comuna_prov.ToString());
                exec.ExecStoredProcedure("SP_CREAR_PROVEEDOR", Parameters);
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
