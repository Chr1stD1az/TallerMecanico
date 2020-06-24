using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Datos
{
    public class Inf_pedido_dto
    {
        public int IDPedido { get; set; }
        public string Empleado { get; set; }
        public string Fecha_Recepcion { get; set; }
        public int IDRecepcion { get; set; }
        public string Empresa { get; set; }
        public int IDProducto { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
    }

}
