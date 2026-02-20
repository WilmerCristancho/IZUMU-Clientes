using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Domain.Entities
{
    public class TypeDocumentEntity
    {
        public int IDTipoDocumento { get; set; }
        public string TipoDocumentoNombre { get; set; } = string.Empty;
    }
}
