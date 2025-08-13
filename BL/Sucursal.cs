using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {

        private readonly DL.JocampoProgramacionNcapasContext _context;

        public Sucursal(DL.JocampoProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {

            ML.Result result = new ML.Result();

            try
            {
                var query = _context.SucursalGetAllDTO.FromSqlRaw("SucursalGetAll").ToList();

                if(query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach(var item in query)
                    {
                        ML.Sucursal sucursal = new ML.Sucursal();

                        sucursal.IdSucursal = item.IdSucursal;
                        sucursal.Nombre = item.Nombre;
                        sucursal.Latitud = item.Latitud;
                        sucursal.Longitud = item.Longitud;

                        result.Objects.Add(sucursal);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay sucursales";
                }

            }catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.ex = ex;
            }

            return result;
        }

        public ML.Result ProductosByIdSucursal(int IdSucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.ProductosByIdSucursalDTO.FromSqlRaw($"ProductoByIdSucursal '{IdSucursal}'").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var item in query)
                    {
                        ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
                        productoSucursal.Producto = new ML.Producto();
                        productoSucursal.Sucursal = new ML.Sucursal();

                        productoSucursal.IdProductoSucursal = item.IdProductoSucursal;
                        productoSucursal.Stock = item.Stock ?? 0;
                        productoSucursal.Producto.Nombre = item.Nombre;
                        productoSucursal.Producto.Imagen = item.Imagen;
                        productoSucursal.Sucursal.Nombre = item.NombreSucursal;
                        if (productoSucursal.Producto.Imagen != null)
                        {
                            productoSucursal.Producto.Base64 = Convert.ToBase64String(productoSucursal.Producto.Imagen);
                        }
                        result.Objects.Add(productoSucursal);
                    }

                    result.Correct = true;
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.ex = ex;
            }
            return result;
        }

        public ML.Result UpdateStock(int IdProducto ,  int Stock)
        {
            ML.Result result = new ML.Result();

            try
            {
                var filasAfectadas = _context.Database.ExecuteSqlRaw($"ProductoSucursalUpdateStock '{IdProducto}','{Stock}'");

                if(filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se actualizo el Stock";
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.ex = ex;
            }

            return result;
        }

        public ML.Result ResetStock(int IdProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                var filasAfectadas = _context.Database.ExecuteSqlRaw($"ProductoSucursalResetStock '{IdProducto}'");

                if(filasAfectadas > 0)
                {
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
    }
}
