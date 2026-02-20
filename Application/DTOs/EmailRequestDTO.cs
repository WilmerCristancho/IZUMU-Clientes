using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.DTOs
{
    public class EmailRequestDTO
    {
        public int IDCliente { get; set; }
        public string Email { get; set; } = string.Empty;
        public int IDEmail { get; internal set; }
    }
}
