using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Comuna_Negocio
    {
        public List<Comuna_dto> ListarComuna()
        {
            OracleComand exec = new OracleComand();

            List<Comuna_dto> retorno = new List<Comuna_dto>();
            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_COMUNA", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Comuna_dto entidad = new Comuna_dto();
                    entidad.id_comuna = int.Parse(rows["id_comuna"].ToString());
                    entidad.desc_comuna = rows["desc_comuna"].ToString();
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
