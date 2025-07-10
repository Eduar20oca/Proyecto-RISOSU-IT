using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;

        public UsuarioController(BL.Usuario usuario, BL.Rol rol)
        {
            _usuario = usuario;
            _rol = rol;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = _usuario.GetAllSPEF(usuario);

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

        [HttpPost]
        public IActionResult GetAll(ML.Usuario Usuario)
        {
            Usuario.Rol = new ML.Rol();

            Usuario.Nombre = Usuario.Nombre ?? "";
            Usuario.ApellidoPaterno = Usuario.ApellidoPaterno ?? "";
            Usuario.ApellidoMaterno = Usuario.ApellidoMaterno ?? "";

            ML.Result result = _usuario.GetAllSPEF(Usuario);

            if (result.Correct)
            {
                Usuario.Usuarios = result.Objects;
            }

            ML.Result resultRol = _rol.GetAll();
            if (resultRol.Correct)
            {
                Usuario.Rol.Roles = resultRol.Objects;
            }

            return View(Usuario);
        }



    }
}
