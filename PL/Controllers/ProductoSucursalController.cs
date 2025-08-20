using DL;
using Microsoft.AspNetCore.Mvc;
using ML;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Xml.Schema;

namespace PL.Controllers
{
    public class ProductoSucursalController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _Credenciales;
        private readonly BL.Sucursal _Sucursal;

        public ProductoSucursalController(BL.Sucursal sucursal, IConfiguration Credenciales, IWebHostEnvironment env)
        {
            _Sucursal = sucursal;
            _Credenciales = Credenciales;
            _env = env;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
            productoSucursal.Sucursal = new ML.Sucursal();
            ML.Result result = _Sucursal.GetAll();

            if (result.Correct)
            {
                productoSucursal.Sucursal.Sucursales = result.Objects;
            }

            return View(productoSucursal);
        }

        [HttpPost]
        public IActionResult GetAll(ML.ProductoSucursal productoSucursal)
        {

            ML.Result result = _Sucursal.ProductosByIdSucursal(productoSucursal.Sucursal.IdSucursal);

            if (result.Correct)
            {
                productoSucursal.ProductosSucursales = result.Objects;
                productoSucursal.Sucursal = new ML.Sucursal();

                ML.Result resultSucursal = _Sucursal.GetAll();

                if (resultSucursal.Correct)
                {
                    productoSucursal.Sucursal.Sucursales = resultSucursal.Objects;
                }

                return View(productoSucursal);
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateStock(int IdProductoSucursal, int Stock, string Nombre)
        {

            ML.Result result = _Sucursal.UpdateStock(IdProductoSucursal, Stock);

            if (result.Correct)
            {
                EnviarCorreo(Nombre);
                return RedirectToAction("GetAll");
            }


            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public IActionResult EnviarCorreo(string ProductoNombre)
        {

            try
            {
                string correo = _Credenciales["Credenciales:Correo"];
                string contraseña = _Credenciales["Credenciales:Contraseña"];

                string body = "";
                string path = Path.Combine(_env.WebRootPath, "Content", "correo.html");
                string imgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Content", "imagen.png");

                StreamReader lector = new StreamReader(path);

                body = lector.ReadToEnd();                
                body = body.Replace("{{Producto}}", ProductoNombre);
                body = body.Replace("{{Accion}}", Url.Action("GetAll", "ProductoSucursal", null, Request.Scheme));

                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                LinkedResource imagen = new LinkedResource(imgpath, MediaTypeNames.Image.Png)
                {
                    ContentId = "Imagen",
                    TransferEncoding = TransferEncoding.Base64
                };

                vistaHtml.LinkedResources.Add(imagen);

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(correo, contraseña),
                    EnableSsl = true
                };

                var mensaje = new MailMessage
                {
                    From = new MailAddress(correo, "Jesus"),
                    Subject = "Asunto",                    
                    IsBodyHtml = true
                };

                mensaje.To.Add("jeocampo01@outlook.com");
                mensaje.AlternateViews.Add(vistaHtml);
                smtpClient.Send(mensaje);

            }
            catch (Exception ex)
            {

            }
            return View();
        }


    }
}
