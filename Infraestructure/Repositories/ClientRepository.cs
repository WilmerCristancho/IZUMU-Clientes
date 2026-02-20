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
    public class ClientRepository : IClientRepository
    {
        private readonly DapperContext _context;

        public ClientRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<ClientEntity>>> SelectClients()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var clientes = await connection.QueryAsync<ClientEntity>(
                    "sp_ObtenerClientes",
                    commandType: CommandType.StoredProcedure
                );
                return Result<IEnumerable<ClientEntity>>.Success(clientes);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ClientEntity>>.Failure(ex.Message);
            }
        }

        public async Task<Result<ClientEntity>> SelectbyIDClient(int idCliente)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var cliente = await connection.QueryFirstOrDefaultAsync<ClientEntity>(
                    "sp_ObtenerClientePorId",
                    new { IDCliente = idCliente },
                    commandType: CommandType.StoredProcedure
                );

                if (cliente is null)
                    return Result<ClientEntity>.Failure("Cliente no encontrado.");

                return Result<ClientEntity>.Success(cliente);
            }
            catch (Exception ex)
            {
                return Result<ClientEntity>.Failure(ex.Message);
            }
        }

        public async Task<Result<int>> InsertClient(ClientEntity cliente)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var idCliente = await connection.ExecuteScalarAsync<int>(
                    "sp_CrearCliente",
                    new
                    {
                        cliente.TipoIdentificacion,
                        cliente.Identificacion,
                        cliente.PrimerNombre,
                        cliente.SegundoNombre,
                        cliente.PrimerApellido,
                        cliente.SegundoApellido,
                        cliente.FechaNacimiento,
                        cliente.IDPlan,
                        cliente.Telefonos,
                        cliente.Emails,
                        cliente.Direcciones
                    },
                    commandType: CommandType.StoredProcedure
                );
                return Result<int>.Success(idCliente);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> UpdateClient(ClientEntity cliente)
        {
            try
            {
                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(
                    "sp_ActualizarCliente",
                    new
                    {
                        cliente.IDCliente,
                        cliente.TipoIdentificacion,
                        cliente.Identificacion,
                        cliente.PrimerNombre,
                        cliente.SegundoNombre,
                        cliente.PrimerApellido,
                        cliente.SegundoApellido,
                        cliente.FechaNacimiento,
                        cliente.IDPlan
                    },
                    commandType: CommandType.StoredProcedure
                );
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteClient(int idCliente)
        {
            try
            {
                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(
                    "sp_EliminarCliente",
                    new { IDCliente = idCliente },
                    commandType: CommandType.StoredProcedure
                );
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
