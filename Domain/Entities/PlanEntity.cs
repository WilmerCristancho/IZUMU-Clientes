using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Domain.Entities
{
    public class PlanEntity
    {
        public int IDPlan { get; set; }
        public string PlanNombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
    }
}
