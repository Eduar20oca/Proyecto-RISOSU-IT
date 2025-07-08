using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BL.Usuario _usuario;

        public UsuarioController(BL.Usuario usuario)
        {
            _usuario = usuario;
        }
        
        public IActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result =_usuario.GetAllSPEF(usuario);

            
            
            return View(usuario);
        }
    }
}
