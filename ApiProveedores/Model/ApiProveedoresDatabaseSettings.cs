namespace ApiProveedores.Model
{
    /// <summary>
    /// Clase en la cual se almacenan los datos de configuración de MongoDb
    /// </summary>
    public class ApiProveedoresDatabaseSettings
    {
        /// <summary>
        /// Cadena de conexión
        /// </summary>
        public string ConnectionString { get; set; } = null!;

        /// <summary>
        /// Nombre de la base datos
        /// </summary>
        public string DatabaseName { get; set; } = null!;

        /// <summary>
        /// Nombre de la colección
        /// </summary>
        public string ProveedoresCollectionName { get; set; } = null!;
    }
}
