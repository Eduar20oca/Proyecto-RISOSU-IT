using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {

        private readonly DL.JocampoProgramacionNcapasContext _context;

        public Estado(DL.JocampoProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {

                var query = _context.Estados.FromSqlRaw("EstadoGetAll").ToList();

                if(query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach(var item in query)
                    {
                        ML.Estado estado = new ML.Estado();

                        estado.IdEstado = item.IdEstado;
                        estado.Nombre = item.Nombre;

                        result.Objects.Add(estado);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron Estados";
                }


            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
