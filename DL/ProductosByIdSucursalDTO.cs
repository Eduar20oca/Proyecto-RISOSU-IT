using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class ProductosByIdSucursalDTO
    {
        public int IdProductoSucursal { get; set; }
        public int? Stock { get; set; } 
        public byte[]? Imagen { get; set; }
        public string? Nombre { get; set; }
        public string? NombreSucursal { get; set; }

    }
}
