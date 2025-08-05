using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class UsuarioLoginDTO
    {
        public required string UsuarioNombre{ get; set; }
        public required string UserName { get; set; }
        public required string RolNombre { get; set; }
    }
}
