using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {

        private readonly DL.JocampoProgramacionNcapasContext _context;

        public Municipio(DL.JocampoProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetMunicipioByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.GetByIdEstadoDTO.FromSqlRaw($"MunicipioByIdEstado '{IdEstado}'").ToList();

                if(query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach(var item in query)
                    {
                        ML.Municipio municipio = new ML.Municipio();

                        municipio.IdMunicipio = item.IdMunicipio;
                        municipio.Nombre = item.Nombre;

                        result.Objects.Add(municipio);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron municipios";
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
