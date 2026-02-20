namespace IzumuClientes.Application.DTOs
{
    public class ClientResponseDTO
    {
        public int IDCliente { get; set; }
        public int TipoIdentificacion { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string PrimerNombre { get; set; } = string.Empty;
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; } = string.Empty;
        public string? SegundoApellido { get; set; }
        public string? NombreCliente { get; set; }
        public string FechaNacimiento { get; set; } = string.Empty; 
        public int IDPlan { get; set; }
        public string? Telefonos { get; set; }   
        public string? Emails { get; set; }   
        public string? Direcciones { get; set; }  
    }
}
