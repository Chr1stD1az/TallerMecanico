using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
   public  class Servicio_Negocio
    {
        public List<Servicios_dto> ListaServicio()
        {
            OracleComand exec = new OracleComand();

            List<Servicios_dto> retorno = new List<Servicios_dto>();
            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_SERVICIO", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Servicios_dto entidad = new Servicios_dto();
                    entidad.ID = int.Parse(rows["ID_SERVICIO"].ToString());
                    entidad.Descripción = rows["DESC_SERVICIO"].ToString();
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

        public DataTable Buscar_SERV_id(string id_SER)
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            try
            {
                Parameters.Add("V_ID_SER;", id_SER);
                exec.ExecStoredProcedure("SP_LISTAR_SERV_ID", dataTable, Parameters);

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
