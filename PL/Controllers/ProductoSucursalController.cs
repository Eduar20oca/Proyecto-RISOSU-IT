using DL;
using Microsoft.AspNetCore.Mvc;
using ML;
using System;
using System.Net;
using System.Net.Mail;
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
        public IActionResult UpdateStock(int IdProducto, int Stock)
        {

            ML.Result result = _Sucursal.UpdateStock(IdProducto, Stock);

            if (result.Correct)
            {

                RedirectToAction("GetAll");
            }


            return View("GetAll");
        }


        public IActionResult EnviarCorreo(ML.ProductoSucursal productoSucursal)
        {

            try
            {
                string correo = _Credenciales["Credenciales:Correo"];
                string contraseña = _Credenciales["Credenciales:Contraseña"];

                string body = "";
                string path = Path.Combine(_env.WebRootPath, "Content", "correo.html");

                StreamReader lector = new StreamReader(path);

                body = lector.ReadToEnd();
                body = body.Replace("{{Imagen}}", "https://media.istockphoto.com/id/1201223949/es/vector/flecha-al-icono-de-l%C3%ADnea-izquierda-y-derecha-aislado-sobre-fondo-blanco-ilustraci%C3%B3n-vectorial.jpg?s=612x612&w=0&k=20&c=V6RlhNeD5jrXK_lYNvUrfvUY1pGV_WU-RggoUdoalRo=");
                body = body.Replace("{{Producto}}", productoSucursal.Producto.Nombre);
                body = body.Replace("{{StockAntes}}", $"{productoSucursal.Stock}");
                body = body.Replace("{{StockActual}}", productoSucursal.Stock.ToString());
                body = body.Replace("{{Accion}}", Url.Action("GetAll", "ProductoSucursal"));

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
                    Body = body,
                    IsBodyHtml = true
                };

                mensaje.To.Add("jeocampo01@outlook.com");
                smtpClient.Send(mensaje);

            }
            catch (Exception ex)
            {

            }
            return View();
        }


    }
}
