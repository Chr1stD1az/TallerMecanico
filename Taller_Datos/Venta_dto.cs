using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Datos
{
    public class Venta_dto
    {
        public string T { get; set; }
        public int ID { get; set; }
        public string SKU { get; set; }
        public string Descripción { get; set; }
        public int Cantidad { get; set; }
        public string Precio { get; set; }
        public string Total { get; set; }   
    }
}
