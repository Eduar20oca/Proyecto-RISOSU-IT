using DL;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Producto
    {

        private readonly DL.JocampoProgramacionNcapasContext _context;
        public Producto(DL.JocampoProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetByIdSubCategoria(int IdSubCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {

                var query = _context.GetProductoByIdSubCategoriaDTO.FromSqlRaw($"ProductoGetByIdCategoria '{IdSubCategoria}'").ToList();

                if(query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach(var item in query)
                    {
                        ML.Producto Producto = new ML.Producto();
                        Producto.SubCategoria = new ML.SubCategoria();
                        Producto.SubCategoria.Categoria = new ML.Categoria();

                        Producto.IdProducto = item.IdProducto;
                        Producto.Nombre = item.Nombre;
                        Producto.Descripcion = item.Descripcion;
                        Producto.Precio = item.Precio;
                        Producto.Imagen = item.Imagen;
                        Producto.SubCategoria.Nombre = item.NombreSubCategoria;
                        Producto.SubCategoria.Categoria.Nombre = item.NombreCategoria;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ex = ex;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }


    }
}
