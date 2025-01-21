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
    /// Controlador de usuarios, encargado de gestionar el token
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        /// <summary>
        /// Clave secreta para el Jwt
        /// </summary>
        private readonly string secretKey;

        /// <summary>
        /// Constructor del controlador, aquí se establece la configuración de config usado en la gestión del token
        /// </summary>
        /// <param name="config">Configuraciones de la aplicación obtenidas mediante inyección de dependencias</param>
        public UsuariosController(IConfiguration config)
        {
            secretKey = config.GetSection("JwtSettings").GetSection("SecretKey").ToString();
        }

        /// <summary>
        /// Método usado para obtener el token a través de un usuario y contraseña, en este caso el usuario y contraseña es código estático
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
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
    }
}
