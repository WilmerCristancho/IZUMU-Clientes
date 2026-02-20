using System.ComponentModel.DataAnnotations;

namespace IzumuClientes.Web.Models
{
    public class ClienteFormViewModel
    {
        public int IDCliente { get; set; }

        [Required(ErrorMessage = "El tipo de identificación es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un tipo de identificación")]
        public int TipoIdentificacion { get; set; }

        [Required(ErrorMessage = "La identificación es obligatoria")]
        [MaxLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo números")]
        public string Identificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El primer nombre es obligatorio")]
        [MaxLength(50)]
        public string PrimerNombre { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? SegundoNombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        [MaxLength(50)]
        public string PrimerApellido { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? SegundoApellido { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [RegularExpression(@"^\d{2}-\d{2}-\d{4}$", ErrorMessage = "Formato dd-MM-yyyy")]
        public string FechaNacimiento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El plan es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un plan")]
        public int IDPlan { get; set; }
        public List<TelefonoViewModel> Telefonos { get; set; } = new();
        public List<EmailViewModel> Emails { get; set; } = new();
        public List<DireccionViewModel> Direcciones { get; set; } = new();
        public List<TipoDocumentoViewModel> TiposDocumento { get; set; } = new();
        public List<PlanViewModel> Planes { get; set; } = new();
    }

    public class TelefonoViewModel
    {
        public string Telefono { get; set; } = string.Empty;
        public bool Movil { get; set; }
    }

    public class EmailViewModel
    {
        public string Email { get; set; } = string.Empty;
    }

    public class DireccionViewModel
    {
        public string Direccion { get; set; } = string.Empty;
    }

    public class TipoDocumentoViewModel
    {
        public int IDTipoDocumento { get; set; }
        public string TipoDocumentoNombre { get; set; } = string.Empty;
    }

    public class PlanViewModel
    {
        public int IDPlan { get; set; }
        public string PlanNombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
    }
}
