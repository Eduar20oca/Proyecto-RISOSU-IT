using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {

        private readonly BL.Producto _Producto;
        private readonly BL.Categoria _Categoria;
        private readonly BL.SubCategoria _SubCategoria;

        public ProductoController(BL.Producto Producto, BL.Categoria Categoria, BL.SubCategoria subCategoria)
        {
            _Producto = Producto;
            _Categoria = Categoria;
            _SubCategoria = subCategoria;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.SubCategoria = new ML.SubCategoria();
            producto.SubCategoria.Categoria = new ML.Categoria();

            ML.Result resultCategorias = _Categoria.GetAll();

            if (resultCategorias.Correct)
            {
                producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
            }

            return View(producto);
        }

        [HttpGet]
        public IActionResult Form(int IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            if (IdProducto > 0)
            {                
                ML.Result result = _Producto.GetById(IdProducto);

                producto = (ML.Producto)result.Object;
            }

            return View(producto);
        }

        [HttpPost]
        public IActionResult Form(ML.Producto producto)
        {

            if(producto.IdProducto > 0)
            {
                _Producto.Update(producto);
            }
            else
            {
                _Producto.Add(producto);
            }

            return View("GetAll");

        }

        [HttpGet]
        public IActionResult GetByIdSubcategoria(int IdSubCategoria)
        {
            ML.Result resultProductos = _Producto.GetByIdSubCategoria(IdSubCategoria);

            if (resultProductos.Correct)
            {
                return Json(resultProductos);
            }
            else
            {
                return Json(null);
            }            
        }

        [HttpGet]
        public JsonResult GetByIdCategoria(int IdCategoria)
        {
            ML.Result resultSubCategoria = _SubCategoria.GetByIdCategoria(IdCategoria);

            if (resultSubCategoria.Correct)
            {
                return Json(resultSubCategoria);
            }
            else
            {
                return Json(null);
            }
        }
        
    }
}
