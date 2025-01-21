using ApiProveedores.Model;
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
            IOptions<ApiProveedoresDatabaseSettings> proveedoresStoreDatabaseSettings, IConfiguration config)
        {
            secretKey = config.GetSection("JwtSettings").GetSection("SecretKey").ToString();

            var mongoClient = new MongoClient(
                proveedoresStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                proveedoresStoreDatabaseSettings.Value.DatabaseName);

            _proveedoresCollection = mongoDatabase.GetCollection<Proveedor>(
                proveedoresStoreDatabaseSettings.Value.ProveedoresCollectionName);
        }

        [HttpPost]
        public IActionResult ObtenerToken([FromBody] Usuario usuario)
        {
            if (usuario.correo == "juan@hotmail.es" && usuario.clave == "123456")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.correo));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject=claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado });
            }else
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
        }


        /// <summary>
        /// Método Get usado para obtener todos los proveedores en el store de MongoDb
        /// </summary>
        /// <returns>Lista de Proveedores</returns>
        [HttpGet]
        public async Task<List<Proveedor>> GetAsync() =>
            await _proveedoresCollection.Find(_ => true).ToListAsync();

        /// <summary>
        /// Método Get usado para obtener un proveedor del store de MongoDb
        /// </summary>
        /// <param name="id">Nit del proveedor</param>
        /// <returns>El proveedor buscado</returns>
        public async Task<Proveedor?> GetAsync(string id) =>
            await _proveedoresCollection.Find(x => x.Nit == id).FirstOrDefaultAsync();

        /// <summary>
        /// Método Post usado para guardar un proveedor nuevo en el store de MongoDb
        /// </summary>
        /// <param name="newProveedor">Objeto que contiene los datos del proveedor</param>
        /// <returns>El proveedor creado</returns>
        [HttpPost]
        public async Task CreateAsync(Proveedor newProveedor) =>
            await _proveedoresCollection.InsertOneAsync(newProveedor);

        /// <summary>
        /// Método Put usado para actualizar un proveedor en el store de MongoDb
        /// </summary>
        /// <param name="id">Nit del proveedor</param>
        /// <param name="updatedBook">Objeto que contiene los datos del proveedor</param>
        /// <returns>El proveedor actualizado/returns>
        [HttpPut]
        public async Task UpdateAsync(string id, Proveedor updatedBook) =>
            await _proveedoresCollection.ReplaceOneAsync(x => x.Nit == id, updatedBook);

        /// <summary>
        /// Método Delete usado para eliminar un proveedor en el store de MongoDb
        /// </summary>
        /// <param name="id">Nit del proveedor</param>
        /// <returns>El proveedor eliminado</returns>
        [HttpDelete]
        public async Task RemoveAsync(string id) =>
            await _proveedoresCollection.DeleteOneAsync(x => x.Nit == id);
    }
}
