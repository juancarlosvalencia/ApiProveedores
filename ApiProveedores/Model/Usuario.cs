namespace ApiProveedores.Model
{
    /// <summary>
    /// Modelo usado para ingresar las credenciales del usuario
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Correo del usuario
        /// </summary>
        public string correo { get; set; }

        /// <summary>
        /// Clave del usuario
        /// </summary>
        public string clave { get; set; }
    }
}
