using Dapper;
using IzumuClientes.Domain.Common;
using IzumuClientes.Domain.Entities;
using IzumuClientes.Domain.Interfaces;
using IzumuClientes.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Infraestructure.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly DapperContext _context;

        public PlanRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<PlanEntity>>> SelectPlans()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var planes = await connection.QueryAsync<PlanEntity>(
                    "sp_ObtenerPlanes",
                    commandType: CommandType.StoredProcedure
                );
                return Result<IEnumerable<PlanEntity>>.Success(planes);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<PlanEntity>>.Failure(ex.Message);
            }
        }
    }
}
