using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Runtime.Remoting;
using ML;
using BL;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace SL_REST.Controllers
{

    [ApiController]
    [Route("api")]
    public class UsuarioController : ControllerBase
    {

        private readonly BL.Usuario _usuario;

        public UsuarioController (BL.Usuario usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Lector")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            usuario.Nombre = "";
            usuario.ApellidoMaterno = "";
            usuario.ApellidoPaterno = "";

            

            ML.Result result = _usuario.GetAllSPEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }           
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody]ML.Usuario usuario)
        {
            
            ML.Result result = _usuario.AddSPEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("Update/{IdUsuario}")]
        public IActionResult Update(int IdUsuario ,[FromBody] ML.Usuario usuario)
        {
            usuario.IdUsuario = IdUsuario;

            ML.Result result = _usuario.AddSPEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("Delete/{IdUsuario}")]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = _usuario.DeleteSPEF(IdUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route("GetById/{IdUsuario}")]
        public IActionResult GetById(int? IdUsuario)
        {
            ML.Result result = _usuario.GetByIdSPEF(IdUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public IActionResult Login([FromBody]ML.Login Login)
        {
            ML.Result result = _usuario.Login(Login);

            if (result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario = (ML.Usuario)result.Object;

                string token = GenerateJwtToken(usuario);

                return Ok(token);
            }
            else
            {
                return BadRequest(result);
            }

        }

        private string GenerateJwtToken(ML.Usuario usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Role, usuario.Rol.Descripcion),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fdio15asas4rwey7856dfgsdfwe757sd5das5asd"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }



}

