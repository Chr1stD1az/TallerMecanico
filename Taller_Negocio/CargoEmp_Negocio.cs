using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;
namespace Taller_Negocio
{
    public class CargoEmp_Negocio
    {

        public List<Cargo_dto> ListarCargoEmp()
        {
            OracleComand exec = new OracleComand();

            List<Cargo_dto> retorno = new List<Cargo_dto>();

            try
            {
                var Parameters = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                exec.ExecStoredProcedure("SP_DDL_LISTAR_CARGO", dataTable, Parameters);
                foreach (DataRow rows in dataTable.Rows)
                {
                    Cargo_dto entidad = new Cargo_dto();
                    entidad.id_cargo = int.Parse(rows["id_cargo"].ToString());
                    entidad.desc_cargo = rows["desc_cargo"].ToString();
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
