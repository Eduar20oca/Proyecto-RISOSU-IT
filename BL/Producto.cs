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

                        result.Objects.Add(Producto);
                    }
                    result.Correct = true;
                }
                else{
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron productos";
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

        public ML.Result Add(ML.Producto producto)
        {            
            ML.Result result = new ML.Result();

            try
            {

                var filasAfectadas = _context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}' , '{producto.Descripcion}', '{producto.Precio}' , {producto.Imagen}, '{producto.SubCategoria.IdSubCategoria}'");

                if(filasAfectadas > 0)
                {
                    result.Correct = true;                    
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo añadir el producto";
                }


            }catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.ex = ex;
            }

            return result;

        }

        public ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();

            try {

                var filasAfectadas = _context.Database.ExecuteSqlRaw($"ProductoDelete '{IdProducto}'");

            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.ex = ex;
            }

            return result;
        }

        public ML.Result GetById(int IdProducto)
        {

            ML.Result result = new ML.Result();

            try
            {

                var query = _context.Productos.FromSqlRaw($"ProductoGetById '{IdProducto}'").FirstOrDefault();

                if(query != null)
                {
                    ML.Producto producto = new ML.Producto();
                    producto.SubCategoria = new ML.SubCategoria();

                    producto.Nombre = query.Nombre;
                    producto.Descripcion = query.Descripcion;
                    producto.Precio = query.Precio;
                    producto.Imagen = query.Imagen;
                    producto.SubCategoria.IdSubCategoria = query.IdSubCategoria ?? 0;

                    result.Object = producto;
                    
                    result.Correct = true;
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

        public ML.Result Update(ML.Producto producto)
        {

            ML.Result result = new ML.Result();

            try
            {

                var filasAfectadas = _context.Database.ExecuteSqlRaw($"ProductoUpdate '{producto.IdProducto}' , '{producto.Nombre}', '{producto.Descripcion}', '{producto.Precio}', '{producto.Imagen}', '{producto.SubCategoria.IdSubCategoria}' ");


                if(filasAfectadas > 0)
                {
                    result.Correct = true;

                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo actualizar el producto";
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
