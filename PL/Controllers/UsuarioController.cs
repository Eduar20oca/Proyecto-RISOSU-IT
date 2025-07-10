using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;
        private readonly BL.Estado _estado;
        private readonly BL.Municipio _municipio;
        private readonly BL.Colonia _colonia;

        public UsuarioController(BL.Usuario usuario, BL.Rol rol, BL.Estado estado, BL.Municipio municipio , BL.Colonia colonia)
        {
            _usuario = usuario;
            _rol = rol;
            _estado = estado;
            _municipio = municipio;
            _colonia = colonia;
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
                
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();


            ML.Result resultRol = _rol.GetAll();

            if (resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }

            ML.Result ResultEstado = _estado.GetAll();
            if (ResultEstado.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = ResultEstado.Objects;
            }

            if (IdUsuario > 0)
            {

                ML.Result resultUsuario = _usuario.GetByIdSPEF(IdUsuario);
                usuario = (ML.Usuario)resultUsuario.Object;

                if (usuario != null)
                {
                    ML.Result resultRol2 = _rol.GetAll();
                    usuario.Rol.Roles = resultRol2.Objects;

                    ML.Result resultEstado2 = _estado.GetAll();
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = ResultEstado.Objects;

                    ML.Result resultMunicipio = _municipio.GetMunicipioByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado.Value);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;

                    ML.Result resultColonia = _colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                }


            }
            return View(usuario);
        }

    }
}
