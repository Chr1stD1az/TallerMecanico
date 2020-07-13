using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Datos
{
    public  class Info_Venta_dto
    {
        public string Documento { get; set; }
        public string Familia { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
    }
}
