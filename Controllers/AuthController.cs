using API_Institucion.Datos.Dtos;
using API_Institucion.Entidades;
using API_Institucion.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Institucion.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly Conexion_Db _contexto_db;
        // Me permite acceder a appsettings.json
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config, Conexion_Db contexto_db)
        {
            _contexto_db = contexto_db;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] UsuarioDto usuario_dto)
        {
            var usuario = _contexto_db.usuarios.SingleOrDefault(user => user.dni == usuario_dto.Dni);

            if (usuario == null) 
                return NotFound(new { mensaje = "Usuario no encontrado." });

            if (BCrypt.Net.BCrypt.Verify(usuario_dto.Password, usuario.password))
            {
                string token = GenerarTokenAlumno(usuario_dto);
                return Ok(new { token = token });
            }
            else
                return Unauthorized(new { mensaje = "Creadenciales incorrectas." });
        }

        [HttpPost("registrar")]
        public IActionResult RegistrarAlumno([FromBody] UsuarioDto usuario_dto)
        {
            if (_contexto_db.usuarios.Any(usuario => usuario.dni == usuario_dto.Dni))
                return BadRequest(new { mensaje = "El Dni ya esta registrado." });

            string hash_password = BCrypt.Net.BCrypt.HashPassword(usuario_dto.Password);
            Usuario usuario = new Usuario(usuario_dto.Dni, usuario_dto.Email, usuario_dto.Carrera, hash_password, 3);

            _contexto_db.usuarios.Add(usuario);
            _contexto_db.SaveChanges();

            return Ok(new { mensaje = "Usuario creado correctamente." });
        }

        private string GenerarTokenAlumno(UsuarioDto usuario_dto)
        {
            var alumno = _contexto_db.usuarios.FirstOrDefault(u => u.dni == usuario_dto.Dni); 
            var claims = new[]
            {
                // Claim estandar, dentificador principal del usuario
                new Claim("UsuarioDni", usuario_dto.Dni),
                // Rol
                new Claim(ClaimTypes.Role, "Alumno"),
                // Claim personalizado
                new Claim("Carrera", alumno!.carrera),
                // Identificador del token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Convertir password a bytes para firmar y validar el token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            // Definir algoritmo de cifrado del token (HmacSha256)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddYears(100),
                signingCredentials: creds
            );

            // Serializar a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
