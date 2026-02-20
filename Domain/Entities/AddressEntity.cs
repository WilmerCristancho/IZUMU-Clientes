using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Domain.Entities
{
    public class AddressEntity
    {
        public int IDDireccion { get; set; }
        public int IDCliente { get; set; }
        public string DireccionResidencia { get; set; } = string.Empty;
        public bool Eliminado { get; set; }
    }
}
