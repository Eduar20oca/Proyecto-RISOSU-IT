using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoSucursalController : Controller
    {

        private readonly BL.Sucursal _Sucursal;

        public ProductoSucursalController(BL.Sucursal sucursal)
        {
            _Sucursal = sucursal;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Sucursal sucursal = new ML.Sucursal();
            ML.Result result = _Sucursal.GetAll();

            if (result.Correct)
            {              
                sucursal.Sucursales = result.Objects;                
            }

            return View(sucursal);
        }



    }
}
