namespace IzumuClientes.Application.DTOs
{
    public class ClientRequestDTO
    {
        public int TipoIdentificacion { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string PrimerNombre { get; set; } = string.Empty;
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; } = string.Empty;
        public string? SegundoApellido { get; set; }
        public string FechaNacimiento { get; set; } = string.Empty; 
        public int IDPlan { get; set; }
        public List<TelefonoDTO>? Telefonos { get; set; }
        public List<EmailDTO>? Emails { get; set; }
        public List<DireccionDTO>? Direcciones { get; set; }
    }
    public class TelefonoDTO
    {
        public string Telefono { get; set; } = string.Empty;
        public bool Movil { get; set; }
    }
    public class EmailDTO
    {
        public string Email { get; set; } = string.Empty;
    }
    public class DireccionDTO
    {
        public string Direccion { get; set; } = string.Empty;
    }
}
