using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Tipo_Documento_Negocio
    {
        public List<Tipo_Documento_dto> ListarDocumento()
        {
            OracleComand exec = new OracleComand();

            List<Tipo_Documento_dto> retorno = new List<Tipo_Documento_dto>();
            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_T_DOC", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Tipo_Documento_dto entidad = new Tipo_Documento_dto();
                    entidad.id_documento = int.Parse(rows["ID_DOCUMENTO"].ToString());
                    entidad.descr_documento = rows["DESCR_DOCUMENTO"].ToString();
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
