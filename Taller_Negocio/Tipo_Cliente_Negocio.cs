using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Tipo_Cliente_Negocio
    {
        public List<Tipo_Cliente_dto> ListarTipoCliente()
        {
            OracleComand exec = new OracleComand();

            List<Tipo_Cliente_dto> retorno = new List<Tipo_Cliente_dto>();
            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_TIPO_CLI", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Tipo_Cliente_dto entidad = new Tipo_Cliente_dto();
                    entidad.id_tipo_cliente = int.Parse(rows["id_tipo_cliente"].ToString());
                    entidad.descr_tipo_cl = rows["descr_tipo_cl"].ToString();
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
