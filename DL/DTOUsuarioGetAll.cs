using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class DTOUsuarioGetAll
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; } = null!;

        public string? UserName { get; set; } = null!;

        public string? ApellidoMaterno { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Password { get; set; } = null!;

        public string? Sexo { get; set; } = null!;

        public string? Telefono { get; set; } = null!;

        public string? Celular { get; set; }

        public string? FechaNacimiento { get; set; }

        public string? CURP { get; set; }

        public int? IdRol { get; set; } 
        public string? Descripcion { get; set; }

        public byte[]? Imagen { get; set; }

        public bool Estatus { get; set; }

        public string? Calle { get; set; }

        public int? NumeroInterior { get; set; }
        public int? NumeroExterior { get; set; }
        public string? NombreColonia { get; set; }
        public string? NombreMunicipio { get; set; }
        public string? NombreEstado { get; set; }
    }
}
