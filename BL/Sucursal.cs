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
    }
}
