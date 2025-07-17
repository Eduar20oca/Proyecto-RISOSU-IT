using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        private readonly DL.JocampoProgramacionNcapasContext _context;

        public Colonia(DL.JocampoProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.GetByIdMunicipioDTO.FromSqlRaw($"ColoniaGetByIdMunicipio '{IdMunicipio}'").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach(var item in query)
                    {
                        ML.Colonia colonia = new ML.Colonia();

                        colonia.IdColonia = item.IdColonia;
                        colonia.Nombre = item.Nombre;

                        result.Objects.Add(colonia);
                    }
                    result.Correct = true;
                }

            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

    }
}
