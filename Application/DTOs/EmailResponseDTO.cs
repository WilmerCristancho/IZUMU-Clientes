using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.DTOs
{
    public class EmailResponseDTO
    {
        public int IDEmail { get; set; }
        public int IDCliente { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
