using ApiProveedores.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApiProveedores.Model
{
    /// <summary>
    /// Modelo del proveedor
    /// </summary>
    public class Proveedor
    {
        /// <summary>
        /// Nit del proveedor
        /// </summary>
        [BsonId]
        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        [SoloNumeros(ErrorMessage = "{0} debe contener solo números")]
        [StringLength(10, ErrorMessage = "{0} debe estar entre {1} y {2}", MinimumLength = 9)]
        public string Nit { get; set; } = "";

        /// <summary>
        /// Razón social del proveedor
        /// </summary>
        [Display(Name = "Razón social")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} debe estar entre {1} y {2}")]
        public string RazonSocial { get; set; }

        /// <summary>
        /// Dirección del proveedor
        /// </summary>
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "{0} debe estar entre {1} y {2}")]
        public string Direccion { get; set; }

        /// <summary>
        /// Ciudad del proveedor
        /// </summary>
        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} debe estar entre {1} y {2}")]
        public string Ciudad { get; set; }

        /// <summary>
        /// Departamento del proveedor
        /// </summary>
        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} debe estar entre {1} y {2}")]
        public string Departamento { get; set; }

        /// <summary>
        /// Correo electrónico del proveedor
        /// </summary>
        [Required(ErrorMessage = "{0} es un campo requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} debe estar entre {1} y {2}")]
        [EmailAddress(ErrorMessage = "{0} debe ser un correo electrónico valido")]
        [Display(Name = "Correo")]
        public string Correo { get; set; } = "";

        /// <summary>
        /// Determina si el proveedor está activo
        /// </summary>
        [Required(ErrorMessage = "{0} es un campo requerido")]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        /// <summary>
        /// Fecha de creación del proveedor
        /// </summary>
        [Required(ErrorMessage = "{0} es un campo requerido")]
        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Nombre del contacto del proveedor
        /// </summary>
        [Display(Name = "Nombre del contacto")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} debe estar entre {1} y {2}")]
        public string NombreContacto { get; set; }

        /// <summary>
        /// Correo electrónico del contacto del proveedor
        /// </summary>
        [Required(ErrorMessage = "{0} es un campo requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} debe estar entre {1} y {2}")]
        [EmailAddress(ErrorMessage = "{0} debe ser un correo electrónico valido")]
        [Display(Name = "Correo del contacto")]
        public string CorreoContacto { get; set; } = "";

    }
}
