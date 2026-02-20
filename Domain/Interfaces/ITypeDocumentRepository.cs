using IzumuClientes.Domain.Common;
using IzumuClientes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Domain.Interfaces
{
    public interface ITypeDocumentRepository
    {
        Task<Result<IEnumerable<TypeDocumentEntity>>> SelectTypeDocument();
    }
}
