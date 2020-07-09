using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Datos;

namespace Taller_Negocio
{
    public class Info_Venta_Negocio
    {
        public List<Info_Venta_dto> ListarVenta(string id_doc, string id_famiprod, string fechaI, string fechaF)
        {
            OracleComand exec = new OracleComand();
            DataTable dataTable = new DataTable();
            var Parameters = new Dictionary<string, string>();
            List<Info_Venta_dto> Listado = new List<Info_Venta_dto>();

            fechaI = DateTime.Parse(fechaI).ToString("dd/MM/yyyy");
            fechaF = DateTime.Parse(fechaF).ToString("dd/MM/yyyy");

            try
            {
                Parameters.Add("v_idDoc", id_doc.ToString());
                Parameters.Add("v_idFam", id_famiprod.ToString());
                Parameters.Add("v_fecha_Ini", fechaI.ToString());
                Parameters.Add("v_fecha_Fin", fechaF.ToString());

                exec.ExecStoredProcedure("SP_INFORME_VENTA", dataTable, Parameters);
                foreach (DataRow item in dataTable.Rows)
                {
                    Info_Venta_dto entidad = new Info_Venta_dto();

                    entidad.Documento = (item["DESCR_DOCUMENTO"].ToString());
                    entidad.Familia = item["DESCR_FAMILIA"].ToString();
                    entidad.Tipo = item["DESCR_TIPO_PROD"].ToString();
                    entidad.Nombre = item["DESCR_PRODUCTO"].ToString();
                    entidad.Cantidad = int.Parse(item["SUM(VT.CANTIDAD_VENTA_DT)"].ToString());
                    entidad.Total = int.Parse(item["SUM(VT.TOTAL_VENTA_DT)"].ToString());


                    Listado.Add(entidad);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Listado;

        }

    }
}
