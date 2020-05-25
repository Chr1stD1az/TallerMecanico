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
        public int id_producto { get; set; }
        public string descr_producto { get; set; }
        public int cantidad { get; set; }
        public decimal total_prod { get; set; }
        public decimal precio_prod { get; set; }
    }
}