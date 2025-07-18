using Microsoft.AspNetCore.Mvc;

namespace SL_REST.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly BL.Usuario _usuario;

        public UsuarioController (BL.Usuario usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        [Route("GetAll")]
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


    }
}
