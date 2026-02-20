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
    public class AddressRepository : IAddressRepository
    {
        private readonly DapperContext _context;
        public AddressRepository(DapperContext context) => _context = context;

        public async Task<Result<int>> InsertAddress(AddressEntity idAddress)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var id = await connection.ExecuteScalarAsync<int>(
                    "sp_CrearDireccion",
                    new { idAddress.IDCliente, Direccion = idAddress.DireccionResidencia },
                    commandType: CommandType.StoredProcedure);
                return Result<int>.Success(id);
            }
            catch (Exception ex) { return Result<int>.Failure(ex.Message); }
        }       
    }
}
