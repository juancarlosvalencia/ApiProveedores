using ApiProveedores.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiProveedores.Controllers
{
    /// <summary>
    /// Controlador de proveedores, encargado de gestionar la api del proveedor
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProveedoresController : ControllerBase
    {
        /// <summary>
        /// Objeto configurado mediante inyección de dependencias, establece los métodos crud para la gestion de los datos en MongoDb
        /// </summary>
        private readonly IMongoCollection<Proveedor> _proveedoresCollection;

        /// <summary>
        /// Clave secreta para el Jwt
        /// </summary>
        private readonly string secretKey;

        /// <summary>
        /// Constructor del controlador, aquí se establece la configuración de _proveedoresCollection usado en la gestion de los datos en MongoDb
        /// </summary>
        /// <param name="proveedoresStoreDatabaseSettings">Configuraciones de MongoDb obtenidas mediante inyección de dependencias</param>
        public ProveedoresController(
            IOptions<ApiProveedoresDatabaseSettings> proveedoresStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                proveedoresStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                proveedoresStoreDatabaseSettings.Value.DatabaseName);

            _proveedoresCollection = mongoDatabase.GetCollection<Proveedor>(
                proveedoresStoreDatabaseSettings.Value.ProveedoresCollectionName);
        }

        /// <summary>
        /// Método Get usado para obtener uno o varios proveedores en el store de MongoDb
        /// </summary>
        /// <returns>Lista de Proveedores</returns>
        [HttpGet]
        public async Task<List<Proveedor>> GetAsync(string? id = null)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return await _proveedoresCollection.Find(_ => true).ToListAsync();
                }
                else
                {
                    var resultado = await _proveedoresCollection.Find(x => x.Nit == id).FirstOrDefaultAsync();
                    var proveedores = new List<Proveedor>() { resultado };
                    return proveedores;
                }
            }
            catch (Exception)
            {
                return new List<Proveedor>();
            }
            
        }

        /// <summary>
        /// Método Post usado para guardar un proveedor nuevo en el store de MongoDb
        /// </summary>
        /// <param name="newProveedor">Objeto que contiene los datos del proveedor</param>
        /// <returns>El proveedor creado</returns>
        [HttpPost]
        public async Task CreateAsync(Proveedor newProveedor) {

            try
            {
                await _proveedoresCollection.InsertOneAsync(newProveedor);
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Método Put usado para actualizar un proveedor en el store de MongoDb
        /// </summary>
        /// <param name="id">Nit del proveedor</param>
        /// <param name="updatedBook">Objeto que contiene los datos del proveedor</param>
        /// <returns>El proveedor actualizado/returns>
        [HttpPut]
        public async Task UpdateAsync(string id, Proveedor updatedBook)
        {
            try
            {
                await _proveedoresCollection.ReplaceOneAsync(x => x.Nit == id, updatedBook);
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Método Delete usado para eliminar un proveedor en el store de MongoDb
        /// </summary>
        /// <param name="id">Nit del proveedor</param>
        /// <returns>El proveedor eliminado</returns>
        [HttpDelete]
        public async Task RemoveAsync(string id)
        {
            try
            {
                await _proveedoresCollection.DeleteOneAsync(x => x.Nit == id);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
