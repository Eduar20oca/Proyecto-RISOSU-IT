using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;
        private readonly BL.Estado _estado;
        private readonly BL.Municipio _municipio;
        private readonly BL.Colonia _colonia;

        public UsuarioController(BL.Usuario usuario, BL.Rol rol, BL.Estado estado, BL.Municipio municipio, BL.Colonia colonia)
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
            //ML.Result result = _usuario.GetAllSPEF(usuario);
            ML.Result result = GetAllREST();

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

            Usuario.Nombre = Usuario.Nombre ?? "";
            Usuario.ApellidoPaterno = Usuario.ApellidoPaterno ?? "";
            Usuario.ApellidoMaterno = Usuario.ApellidoMaterno ?? "";

            //ML.Result result = _usuario.GetAllSPEF(Usuario);

            ML.Result result = GetAllREST();
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

                ML.Result resultUsuario = GetByIdREST(IdUsuario);
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

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario, IFormFile ArchivoImagen)
        {


            if (ModelState.IsValid)
            {
                //HttpPostedFileBase imagenValida = Request.Files["ArchivoImagen"];
                //if (ArchivoImagen != null)
                //{
                //    using (var ms = new MemoryStream())
                //    {
                //        //copia el archivo a un memory stream (ms)
                //        ArchivoImagen.InputStream.CopyTo(ms);
                //        //convierte el memorystream un arreglo de bytes y asignamos el arreglo a la propiedad imagen
                //        usuario.Imagen = ms.ToArray();
                //    }
                //}

                if (usuario.IdUsuario > 0)
                {
                    UpdateREST(usuario);
                }
                else
                {
                    AddREST(usuario);
                }
            }

            return RedirectToAction("GetAll");

        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {

            DeleteREST(IdUsuario);

            return RedirectToAction("GetALL");
        }

        [HttpGet]
        public JsonResult GetByIdEstado(int IdEstado)
        {
            ML.Result ResultMunicipios = _municipio.GetMunicipioByIdEstado(IdEstado);

            return Json(ResultMunicipios);
        }

        [HttpGet]
        public JsonResult GetByIdMunicipio(int? IdMunicipio)
        {
            if (IdMunicipio == null)
            {
                int IdMunicipioNull = 0;
                ML.Result ResultColonias = _colonia.GetByIdMunicipio(IdMunicipioNull);
                return Json(ResultColonias);
            }
            else
            {
                ML.Result ResultColonias = _colonia.GetByIdMunicipio(IdMunicipio ?? 0);
                return Json(ResultColonias);
            }
        }

        [HttpPost]
        public JsonResult UpdateEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result resultUpdateEstatus = _usuario.UpdateEstatus(IdUsuario, Estatus);

            return Json(resultUpdateEstatus);
        }

        [NonAction]
        public ML.Result AddREST(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {
                string endPoint = "http://localhost:5080/Add";
                client.BaseAddress = new Uri(endPoint);

                var postTask = client.PostAsJsonAsync<ML.Usuario>("Add", usuario);
                postTask.Wait();

                var respuesta = postTask.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    result = readTask.Result;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Error al agregar usuario: ";
                }
            }

            return result;

        }

        [NonAction]
        public ML.Result GetAllREST()
        {
            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {
                string uri = "http://localhost:5080/GetAll";

                client.BaseAddress = new Uri(uri);

                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();

                var resultado = responseTask.Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var readTask = resultado.Content.ReadAsAsync<ML.Result>();
                    result.Objects = new List<object>();
                    
                    foreach (var item in readTask.Result.Objects)
                    {
                        ML.Usuario usuarioitem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());

                        result.Objects.Add(usuarioitem);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }

            }
            return result;
        }

        [NonAction]
        public ML.Result DeleteREST(int IdUsuario)
        {

            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {
                string uri = "http://localhost:5080/Delete/{IdUsuario}";

                client.BaseAddress = new Uri(uri);

                var postTask = client.DeleteAsync($"Delete/{IdUsuario}");
                postTask.Wait();

                var resultado = postTask.Result;

                if (resultado.IsSuccessStatusCode)
                {

                    var readTask = resultado.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    result = readTask.Result;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Error al Eliminar Usuario";
                }

                return result;
            }
        }

        [NonAction]
        public ML.Result UpdateREST(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {

                string uri = "http://localhost:5080/Update/{IdUsuario}";
                client.BaseAddress = new Uri(uri);

                var putTask = client.PutAsJsonAsync($"Update/{usuario.IdUsuario}", usuario);

                var respuesta = putTask.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    result = readTask.Result;

                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Error al Actualizar al usuario";
                }

            }

            return result;
        }

        [NonAction]
        private ML.Result GetByIdREST(int? IdUsuario) 
        {
            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {
                string uri = "http://localhost:5080/GetById/{IdUsuario}";

                client.BaseAddress = new Uri(uri);

                var responseTask = client.GetAsync($"GetById/{IdUsuario}");
                responseTask.Wait();

                var respuesta = responseTask.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    result = readTask.Result;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Error al consultar usuario";
                }
            }

            return result;
        }





    }
}
