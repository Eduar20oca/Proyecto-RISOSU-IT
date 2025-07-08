using System;
using System.Collections.Generic;

namespace DL;

public partial class UsuarioGetAllView
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? FechaNacimiento { get; set; }

    public string? Curp { get; set; }

    public byte[]? Imagen { get; set; }

    public bool Estatus { get; set; }

    public int? IdRol { get; set; }

    public string? Descripcion { get; set; }

    public string? Calle { get; set; }

    public string? NumeroInterior { get; set; }

    public string? NumeroExterior { get; set; }

    public string? CodigoPostal { get; set; }

    public string? NombreColonia { get; set; }

    public string? NombreMunicipio { get; set; }

    public string? NombreEstado { get; set; }
}
