using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.DTOs
{
    public class PhoneResponseDTO
    {
        public int IDTelefono { get; set; }
        public int IDCliente { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public bool Movil { get; set; }
    }
}
