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
                        usuario1.Direccion.NumeroInterior = Convert.ToInt32(usuarioL.NumeroInterior);
                        usuario1.Direccion.NumeroExterior = Convert.ToInt32(usuarioL.NumeroExterior);
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

        public ML.Result AddSPEF(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                int filasafectadas = _context.Database.ExecuteSqlRaw($"UsuarioAdd , '{Usuario.Nombre}','{Usuario.ApellidoPaterno}', " +
                    $"'{Usuario.ApellidoMaterno}', " +
                    $"'{Usuario.UserName}', " +
                    $"'{Usuario.Email}', " +
                    $"'{Usuario.Password}', " +
                    $"'{Usuario.Sexo}', " +
                    $"'{Usuario.Telefono}', " +
                    $"'{Usuario.Celular}', " +
                    $"'{Usuario.FechaDeNacimiento}', " +
                    $"'{Usuario.CURP}', " +
                    $"{Usuario.Rol.IdRol}, " +
                    $"'{Usuario.Imagen}', " +
                    $"'{Usuario.Direccion.Calle}', " +
                    $"'{Usuario.Direccion.NumeroInterior}', " +
                    $"'{Usuario.Direccion.NumeroExterior}', " +
                    $"{Usuario.Direccion.Colonia.IdColonia}");

                if (filasafectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    Console.WriteLine("Usuario no agregado");
                    result.Correct = false;
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Error al agregar al usuario";
                result.Ex = ex;
            }
            return result;
        }

        public ML.Result DeleteSPEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                int filasafectadas = _context.Database.ExecuteSqlRaw($"UsuarioDelete, {IdUsuario}");


                if (filasafectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    Console.WriteLine("Usuario no actualizado");
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Error al eliminar usuario" + ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public ML.Result UpdateSPEF(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();

            try
            {

                int filasafectadas = _context.Database.ExecuteSqlRaw($"UsuarioUpdate ,'{Usuario.IdUsuario}' ,'{Usuario.Nombre}','{Usuario.ApellidoPaterno}', " +
                    $"'{Usuario.ApellidoMaterno}', " +
                    $"'{Usuario.UserName}', " +
                    $"'{Usuario.Email}', " +
                    $"'{Usuario.Password}', " +
                    $"'{Usuario.Sexo}', " +
                    $"'{Usuario.Telefono}', " +
                    $"'{Usuario.Celular}', " +
                    $"'{Usuario.FechaDeNacimiento}', " +
                    $"'{Usuario.CURP}', " +
                    $"{Usuario.Rol.IdRol}, " +
                    $"'{Usuario.Imagen}', " +
                    $"'{Usuario.Direccion.Calle}', " +
                    $"'{Usuario.Direccion.NumeroInterior}', " +
                    $"'{Usuario.Direccion.NumeroExterior}', " +
                    $"{Usuario.Direccion.Colonia.IdColonia}");


                if (filasafectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    Console.WriteLine("Error al actaulizar al usuario");
                }



            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }

        public ML.Result GetByIdSPEF(int? IdUsuario)
        {

            ML.Result Result = new ML.Result();

            try
            {                
                    var usuario = _context.UsuarioGetByIDDTO.FromSqlRaw($"UsuarioGetById{IdUsuario}").AsEnumerable().FirstOrDefault();

                    if (usuario != null)
                    {

                        ML.Usuario usuario1 = new ML.Usuario();
                        usuario1.Rol = new ML.Rol();
                        usuario1.Direccion = new ML.Direccion();
                        usuario1.Direccion.Colonia = new ML.Colonia();
                        usuario1.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario1.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuario1.IdUsuario = usuario.IdUsuario;
                        usuario1.Nombre = usuario.Nombre;
                        usuario1.ApellidoPaterno = usuario.ApellidoPaterno;
                        usuario1.ApellidoMaterno = usuario.ApellidoMaterno;
                        usuario1.UserName = usuario.UserName;
                        usuario1.Email = usuario.Email;
                        usuario1.Password = usuario.Password;
                        usuario1.FechaDeNacimiento = usuario.FechaNacimiento;
                        usuario1.Sexo = usuario.Sexo;
                        usuario1.CURP = usuario.CURP;
                        usuario1.Telefono = usuario.Telefono;
                        usuario1.Celular = usuario.Celular;
                        usuario1.Imagen = usuario.Imagen;
                        usuario1.Rol.IdRol = usuario.IdRol ?? 0;
                        usuario1.Direccion.Calle = usuario.Calle;
                        usuario1.Direccion.NumeroInterior = Convert.ToInt32(usuario.NumeroInterior);
                        usuario1.Direccion.NumeroExterior = Convert.ToInt32(usuario.NumeroExterior);
                        usuario1.Direccion.Colonia.IdColonia = usuario.IdColonia ?? 0;
                        usuario1.Direccion.Colonia.Municipio.IdMunicipio = usuario.IdMunicipio ?? 0;
                        usuario1.Direccion.Colonia.Municipio.Estado.IdEstado = usuario.IdEstado ?? 0;

                        Result.Object = usuario1;
                        Result.Correct = true;
                    }
                    else
                    {
                        Result.Correct = false;
                        Console.WriteLine("No se ecnontro al usuario");
                    }
                

            }
            catch (Exception ex)
            {
                Result.Correct = false;
                Result.Ex = ex;
                Result.ErrorMessage = ex.Message;
            }
            return Result;
        }

        public ML.Result UpdateEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result result = new ML.Result();

            try
            {

                var filasAfectadas = _context.Database.ExecuteSqlRaw($"UsuarioUpdateEstatus {IdUsuario}, {Estatus}");

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    Console.WriteLine("No se actualizo el Estatus");
                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

    }


}
