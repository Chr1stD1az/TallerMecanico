using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Datos
{
    public class Detalle_Pedido_dto
    {
    //    public int id_pedido_dt { get; set; }
        public int Producto { get; set; }
        public string Descripción { get; set; }
        public int Cantidad { get; set; }
        public string Precio { get; set; }
        public string Total { get; set; }
    }
}