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
    public class PhoneRepository : IPhoneRepository
    {
        private readonly DapperContext _context;
        public PhoneRepository(DapperContext context) => _context = context;
        public async Task<Result<int>> InsertPhone(PhoneEntity phone)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var id = await connection.ExecuteScalarAsync<int>(
                    "sp_CrearTelefono",
                    new { phone.IDCliente, Telefono = phone.NumeroTelefono, phone.Movil },
                    commandType: CommandType.StoredProcedure);
                return Result<int>.Success(id);
            }
            catch (Exception ex) { return Result<int>.Failure(ex.Message); }
        }       
    }
}
