using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubCategoria
    {
        private readonly DL.JocampoProgramacionNcapasContext _context;
        public SubCategoria(DL.JocampoProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetByIdCategoria(int IdCategoria)
        {

            ML.Result result = new ML.Result();

            try
            {
                var query = _context.SubCategoria.FromSqlRaw($"SubCategoriaByIdCategoria '{IdCategoria}'").ToList();

                if(query.Count > 0)
                {

                    result.Object = new List<object>();

                    foreach(var item in query)
                    {
                        ML.SubCategoria subCategoria = new ML.SubCategoria();
                        subCategoria.IdSubCategoria = item.IdSubCategoria;
                        subCategoria.Nombre = item.Nombre;

                        result.Objects.Add(subCategoria);
                    }
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
