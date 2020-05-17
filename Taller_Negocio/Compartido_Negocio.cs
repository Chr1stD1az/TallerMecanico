using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Negocio
{
    public class Compartido_Negocio
    {
        public  string Encriptar(string _cadena)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadena);
            result = Convert.ToBase64String(encryted);
            return result;
        }





    }
}
