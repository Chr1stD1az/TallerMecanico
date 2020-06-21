using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Datos
{
    public class Recepcion_pedido_dto
    {

        //    public int id_pedido { get; set; }
        public int IDProducto { get; set; }
        public string SKU { get; set; }
        public string Familia { get; set; }
        public string Tipo { get; set; }
        public string Descripción { get; set; }
        public int Cantidad { get; set; }
        public int Recibido { get; set; }
        public string Precio { get; set; }
        public string Total { get; set; }


    }
}
