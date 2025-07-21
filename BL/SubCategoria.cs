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

        public ML.Result GetByIdCategoria()
        {

            ML.Result result = new ML.Result();

            try
            {
                var query = _context.SubCategoria.FromSqlRaw($"")

            }
        }
    }
}
