using Dapper;
using IzumuClientes.Domain.Common;
using IzumuClientes.Domain.Entities;
using IzumuClientes.Domain.Interfaces;
using IzumuClientes.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Infraestructure.Repositories
{
    public class TypeDocumentRepository : ITypeDocumentRepository
    {
        private readonly DapperContext _context;

        public TypeDocumentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<TypeDocumentEntity>>> SelectTypeDocument()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var tipos = await connection.QueryAsync<TypeDocumentEntity>(
                    "sp_ObtenerTiposDocumento",
                    commandType: CommandType.StoredProcedure
                );
                return Result<IEnumerable<TypeDocumentEntity>>.Success(tipos);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<TypeDocumentEntity>>.Failure(ex.Message);
            }
        }
    }
}
