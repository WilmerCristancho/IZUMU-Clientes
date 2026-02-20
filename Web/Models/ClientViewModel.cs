namespace IzumuClientes.Web.Models
{
    public class ClientViewModel
    {
        public int IDCliente { get; set; }
        public int TipoIdentificacion { get; set; }
        public string TipoIdentificacionNombre { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; } = string.Empty;
        public int IDPlan { get; set; }
        public string PlanNombre { get; set; } = string.Empty;
        public string? Telefonos { get; set; }
        public string? Emails { get; set; }
        public string? Direcciones { get; set; }
    }
}
