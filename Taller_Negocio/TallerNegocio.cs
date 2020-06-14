using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
    public class TallerNegocio
    {
        public List<Taller_dtocs> ListarTaller()
        {
            OracleComand exec = new OracleComand();

            List<Taller_dtocs> retorno = new List<Taller_dtocs>();

            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_TALLER", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Taller_dtocs entidad = new Taller_dtocs();
                    entidad.id_taller = int.Parse(rows["id_taller"].ToString());
                    entidad.nombre = rows["razo_social_taller"].ToString();
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
