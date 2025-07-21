using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Categoria
    {
        private readonly DL.JocampoProgramacionNcapasContext _context;
        public Categoria(DL.JocampoProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                var query = _context.Categoria.FromSqlRaw("CategoriaGetAll").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Categoria categoria = new ML.Categoria();

                        categoria.IdCategoria = item.IdCategoria;
                        categoria.Nombre = item.Nombre;

                        result.Objects.Add(categoria);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay Categorias";
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;                
            }
            return result;
        }
    }
}
