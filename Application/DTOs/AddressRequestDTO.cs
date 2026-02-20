using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.DTOs
{
    public class AddressRequestDTO
    {
        public int IDCliente { get; set; }
        public string Direccion { get; set; } = string.Empty;
    }
}
