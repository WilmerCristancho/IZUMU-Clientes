using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Domain.Entities
{
    public class PhoneEntity
    {
        public int IDTelefono { get; set; }
        public int IDCliente { get; set; }
        public string NumeroTelefono { get; set; } = string.Empty;
        public bool Movil { get; set; }
        public bool Eliminado { get; set; }
    }
}
