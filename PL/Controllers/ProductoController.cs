using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {

        private readonly BL.Producto _Producto;
        private readonly BL.Categoria _Categoria;
        private readonly BL.SubCategoria _SubCategoria;

        public ProductoController(BL.Producto Producto, BL.Categoria Categoria , BL.SubCategoria subCategoria)
        {
            _Producto = Producto;
            _Categoria = Categoria;
            _SubCategoria = subCategoria;
        }

        public IActionResult GetAll(int IdCategoria)
        {
            ML.Producto producto = new ML.Producto();

            ML.Result resultCategorias = _Categoria.GetAll();

            if (resultCategorias.Correct)
            {
                producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
            }

            ML.Result resultSubCategorias = _SubCategoria.GetByIdCategoria(IdCategoria);
            
            if (resultSubCategorias.Correct)
            {
                producto.SubCategoria.SubCategorias = resultSubCategorias.Objects;
            }


            
            return View();
        }
    }
}
