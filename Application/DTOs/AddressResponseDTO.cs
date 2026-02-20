using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.DTOs
{
    public class AddressResponseDTO
    {
        public int IDDireccion { get; set; }
        public int IDCliente { get; set; }
        public string Direccion { get; set; } = string.Empty;
    }
}
