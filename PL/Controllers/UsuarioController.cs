using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;

        public UsuarioController(BL.Usuario usuario , BL.Rol rol)
        {
            _usuario = usuario;
            _rol = rol;
        }


        public IActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Rol = new ML.Rol();
            ML.Result result =_usuario.GetAllSPEF(usuario);          

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }

            ML.Result resultRol = _rol.GetAll();

            if (resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }


            
            
            return View(usuario);
        }
    }
}
