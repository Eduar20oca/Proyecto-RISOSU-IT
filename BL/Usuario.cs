using DL;
using Microsoft.EntityFrameworkCore;
using ML;

namespace BL
{
    public class Usuario
    {

        private readonly JocampoProgramacionNcapasContext _context;
        public Usuario(DL.JocampoProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetAllSPEF(ML.Usuario usuario)
        {
            usuario.Rol = new ML.Rol();
            ML.Result result = new ML.Result();

            try
            {

                var query = _context.UsuarioGetAllDTO.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}','{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Rol.IdRol}'").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var usuarioL in query)
                    {
                        ML.Usuario usuario1 = new ML.Usuario();
                        usuario1.Rol = new ML.Rol();
                        usuario1.Direccion = new ML.Direccion();
                        usuario1.Direccion.Colonia = new ML.Colonia();
                        usuario1.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario1.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuario1.IdUsuario = usuarioL.IdUsuario;
                        usuario1.Nombre = usuarioL.Nombre;
                        usuario1.ApellidoPaterno = usuarioL.ApellidoPaterno;
                        usuario1.ApellidoMaterno = usuarioL.ApellidoMaterno;
                        usuario1.UserName = usuarioL.UserName;
                        usuario1.Email = usuarioL.Email;
                        usuario1.Password = usuarioL.Password;
                        usuario1.FechaDeNacimiento = usuarioL.FechaNacimiento;
                        usuario1.Sexo = usuarioL.Sexo;
                        usuario1.CURP = usuarioL.CURP;
                        usuario1.Telefono = usuarioL.Telefono;
                        usuario1.Celular = usuarioL.Celular;
                        usuario1.Imagen = usuarioL.Imagen;
                        usuario1.Rol.Descripcion = usuarioL.Descripcion;
                        usuario1.Estatus = usuarioL.Estatus;
                        usuario1.Direccion.Calle = usuarioL.Calle;
                        usuario1.Direccion.NumeroInterior = usuarioL.NumeroInterior;
                        usuario1.Direccion.NumeroExterior = usuarioL.NumeroExterior;
                        usuario1.Direccion.Colonia.Nombre = usuarioL.NombreColonia;
                        usuario1.Direccion.Colonia.Municipio.Nombre = usuarioL.NombreMunicipio;
                        usuario1.Direccion.Colonia.Municipio.Estado.Nombre = usuarioL.NombreEstado;

                        result.Objects.Add(usuario1);

                    }
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay usuario registrados";
                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Error al obtener usuarios";
                result.Ex = ex;
            }
            return result;
        }
    }


}
