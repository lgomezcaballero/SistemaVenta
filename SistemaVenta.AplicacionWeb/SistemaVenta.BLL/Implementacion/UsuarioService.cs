using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IGenericRepository<Usuario> _repositorio;
        private readonly IFireBaseService _fireBaseService;
        private readonly IUtilidadesService _utilidadesService;
        private readonly ICorreoService _correoService;

        public UsuarioService(
            IGenericRepository<Usuario> repositorio,
            IFireBaseService fireBaseService,
            IUtilidadesService utilidadesService,
            ICorreoService correoService
            )
        {
            _repositorio = repositorio;
            _fireBaseService = fireBaseService;
            _utilidadesService = utilidadesService;
            _correoService = correoService;
        }

        public async Task<List<Usuario>> Lista()
        {
            IQueryable<Usuario> query = await _repositorio.Consultar();
            return query.Include(r => r.IdRolNavigation).ToList();
        }

#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        public async Task<Usuario> Crear(Usuario entidad, Stream Foto = null, string NombreFoto = "", string UrlPlantillaCorreo = "")
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        {

            Usuario usuario_existe = await _repositorio.Obtener(u => u.Correo == entidad.Correo);

            if (usuario_existe != null)
                throw new TaskCanceledException("El correo ya existe");


#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
            try
            {

                string clave_generada = _utilidadesService.GenerarClave();
                entidad.Clave = _utilidadesService.ConvertirSha256(clave_generada);
                entidad.NombreFoto = NombreFoto;

                if (Foto != null)
                {
                    string urlFoto = await _fireBaseService.SubirStorage(Foto, "carpeta_usuario", NombreFoto);
                    entidad.UrlFoto = urlFoto;
                }

                Usuario usuario_creado = await _repositorio.Crear(entidad);

                if (usuario_creado.IdUsuario == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario");

                if (UrlPlantillaCorreo != "")
                {
                    UrlPlantillaCorreo = UrlPlantillaCorreo.Replace("[correo]", usuario_creado.Correo).Replace("[clave]", clave_generada);

                    string htmlCorreo = "";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlPlantillaCorreo);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {

                        using (Stream dataStream = response.GetResponseStream())
                        {

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                            StreamReader readerStream = null;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                            if (response.CharacterSet == null)
                                readerStream = new StreamReader(dataStream);
                            else
                                readerStream = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                            htmlCorreo = readerStream.ReadToEnd();
                            response.Close();
                            readerStream.Close();


                        }
                    }

                    if (htmlCorreo != "")
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                        await _correoService.EnviarCorreo(usuario_creado.Correo, "Cuenta Creada", htmlCorreo);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                }

                IQueryable<Usuario> query = await _repositorio.Consultar(u => u.IdUsuario == usuario_creado.IdUsuario);
                usuario_creado = query.Include(r => r.IdRolNavigation).First();

                return usuario_creado;
            }
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa


        }

#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        public async Task<Usuario> Editar(Usuario entidad, Stream Foto = null, string NombreFoto = "")
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        {
            Usuario usuario_existe = await _repositorio.Obtener(u => u.Correo == entidad.Correo && u.IdUsuario != entidad.IdUsuario);

            if (usuario_existe != null)
                throw new TaskCanceledException("El correo ya existe");


            try
            {

                IQueryable<Usuario> queryUsuario = await _repositorio.Consultar(u => u.IdUsuario == entidad.IdUsuario);

                Usuario usuario_editar = queryUsuario.First();

                usuario_editar.Nombre = entidad.Nombre;
                usuario_editar.Correo = entidad.Correo;
                usuario_editar.Telefono = entidad.Telefono;
                usuario_editar.IdRol = entidad.IdRol;
                usuario_editar.EsActivo = entidad.EsActivo;

                if (usuario_editar.NombreFoto == "")
                    usuario_editar.NombreFoto = NombreFoto;

                if (Foto != null)
                {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                    string urlFoto = await _fireBaseService.SubirStorage(Foto, "carpeta_usuario", usuario_editar.NombreFoto);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                    usuario_editar.UrlFoto = urlFoto;
                }

                bool respuesta = await _repositorio.Editar(usuario_editar);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el usuario");

                Usuario usuario_editado = queryUsuario.Include(r => r.IdRolNavigation).First();

                return usuario_editado;

            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> Eliminar(int IdUsuario)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == IdUsuario);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                string nombreFoto = usuario_encontrado.NombreFoto;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                bool respuesta = await _repositorio.Eliminar(usuario_encontrado);

                if (respuesta)
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                    await _fireBaseService.EliminarStorage("carpeta_usuario", nombreFoto);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                return true;

            }
            catch
            {
                throw;
            }
        }

        public async Task<Usuario> ObtenerPorCredenciales(string correo, string clave)
        {

            string clave_encriptada = _utilidadesService.ConvertirSha256(clave);

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            Usuario usuario_encontrado = await _repositorio.Obtener(u => u.Correo.Equals(correo)
            && u.Clave.Equals(clave_encriptada));
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return usuario_encontrado;

        }

        public async Task<Usuario> ObtenerPorId(int IdUsuario)
        {
            IQueryable<Usuario> query = await _repositorio.Consultar(u => u.IdUsuario == IdUsuario);

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            Usuario resultado = query.Include(r => r.IdRolNavigation).FirstOrDefault();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return resultado;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }


        public async Task<bool> GuardarPefil(Usuario entidad)
        {

            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == entidad.IdUsuario);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("El usuario no existe");


                usuario_encontrado.Correo = entidad.Correo;
                usuario_encontrado.Telefono = entidad.Telefono;

                bool respuesta = await _repositorio.Editar(usuario_encontrado);

                return respuesta;

            }
            catch
            {
                throw;
            }

        }


        public async Task<bool> CambiarClave(int IdUsuario, string ClaveActual, string ClaveNueva)
        {
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == IdUsuario);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                if (usuario_encontrado.Clave != _utilidadesService.ConvertirSha256(ClaveActual))
                    throw new TaskCanceledException("La contraseña ingresada como actual no es correcta");

                usuario_encontrado.Clave = _utilidadesService.ConvertirSha256(ClaveNueva);

                bool respuesta = await _repositorio.Editar(usuario_encontrado);

                return respuesta;


            }
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa

        }
        public async Task<bool> RestablecerClave(string Correo, string UrlPlantillaCorreo)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.Correo == Correo);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("No encontramos ningún usuario asociado al correo");


                string clave_generada = _utilidadesService.GenerarClave();
                usuario_encontrado.Clave = _utilidadesService.ConvertirSha256(clave_generada);


                UrlPlantillaCorreo = UrlPlantillaCorreo.Replace("[clave]", clave_generada);

                string htmlCorreo = "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlPlantillaCorreo);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {

                    using (Stream dataStream = response.GetResponseStream())
                    {

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                        StreamReader readerStream = null;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                        if (response.CharacterSet == null)
                            readerStream = new StreamReader(dataStream);
                        else
                            readerStream = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                        htmlCorreo = readerStream.ReadToEnd();
                        response.Close();
                        readerStream.Close();


                    }
                }

                bool correo_enviado = false;

                if (htmlCorreo != "")
                    correo_enviado = await _correoService.EnviarCorreo(Correo, "Contraseña Restablecida", htmlCorreo);


                if (!correo_enviado)
                    throw new TaskCanceledException("Tenemos problemas. Por favor inténtalo de nuevo más tarde");

                bool respuesta = await _repositorio.Editar(usuario_encontrado);

                return respuesta;

            }
            catch
            {
                throw;
            }

        }
    }
}
