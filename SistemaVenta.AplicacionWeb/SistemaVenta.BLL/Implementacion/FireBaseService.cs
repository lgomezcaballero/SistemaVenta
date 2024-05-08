using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.BLL.Interfaces;
using Firebase.Auth;
using Firebase.Storage;
using SistemaVenta.Entity;
using SistemaVenta.DAL.Interfaces;

namespace SistemaVenta.BLL.Implementacion
{
    public class FireBaseService : IFireBaseService
    {
        private readonly IGenericRepository<Configuracion> _repositorio;

        public FireBaseService(IGenericRepository<Configuracion> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<string> SubirStorage(Stream StreamArchivo, string CarpetaDestino, string NombreArchivo)
        {
            string UrlImagen = "";
            

            try
            {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.Recurso.Equals("FireBase_Storage"));
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

#pragma warning disable CS8619 // La nulabilidad de los tipos de referencia del valor no coincide con el tipo de destino
#pragma warning disable CS8621 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).
#pragma warning disable CS8714 // El tipo no se puede usar como parámetro de tipo en el método o tipo genérico. La nulabilidad del argumento de tipo no coincide con la restricción "notnull"
                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);
#pragma warning restore CS8714 // El tipo no se puede usar como parámetro de tipo en el método o tipo genérico. La nulabilidad del argumento de tipo no coincide con la restricción "notnull"
#pragma warning restore CS8621 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).
#pragma warning restore CS8619 // La nulabilidad de los tipos de referencia del valor no coincide con el tipo de destino


                var auth = new FirebaseAuthProvider(new FirebaseConfig(Config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(Config["email"], Config["clave"]);

                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Config["ruta"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(Config[CarpetaDestino])
                    .Child(NombreArchivo)
                    .PutAsync(StreamArchivo, cancellation.Token);

                UrlImagen = await task;
                
            }
            catch {
                UrlImagen = "";
                
            }

            return UrlImagen;

        }

        



        public async Task<bool> EliminarStorage(string CarpetaDestino, string NombreArchivo)
        {
            try
            {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.Recurso.Equals("FireBase_Storage"));
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

#pragma warning disable CS8619 // La nulabilidad de los tipos de referencia del valor no coincide con el tipo de destino
#pragma warning disable CS8621 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).
#pragma warning disable CS8714 // El tipo no se puede usar como parámetro de tipo en el método o tipo genérico. La nulabilidad del argumento de tipo no coincide con la restricción "notnull"
                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);
#pragma warning restore CS8714 // El tipo no se puede usar como parámetro de tipo en el método o tipo genérico. La nulabilidad del argumento de tipo no coincide con la restricción "notnull"
#pragma warning restore CS8621 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).
#pragma warning restore CS8619 // La nulabilidad de los tipos de referencia del valor no coincide con el tipo de destino


                var auth = new FirebaseAuthProvider(new FirebaseConfig(Config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(Config["email"], Config["clave"]);

                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Config["ruta"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(Config[CarpetaDestino])
                    .Child(NombreArchivo)
                    .DeleteAsync();

                 await task;

                return true;
            }
            catch
            {
                return false;
            }
        }


        //public async Task<Stream> DescargarStorage(string carpetaOrigen, string nombreArchivo)
        //{
        //    try
        //    {
        //        // Obtener la configuración de Firebase Storage
        //        IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.Recurso.Equals("FireBase_Storage"));
        //        Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

        //        // Autenticación con Firebase
        //        var auth = new FirebaseAuthProvider(new FirebaseConfig(Config["api_key"]));
        //        var a = await auth.SignInWithEmailAndPasswordAsync(Config["email"], Config["clave"]);

        //        // Configuración de Firebase Storage
        //        var storage = new FirebaseStorage(
        //            Config["ruta"],
        //            new FirebaseStorageOptions
        //            {
        //                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
        //                ThrowOnCancel = true
        //            });

        //        // Descarga el archivo desde Firebase Storage y devuelve el Stream
        //        return await storage.Child(carpetaOrigen).Child(nombreArchivo).GetStreamAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Maneja cualquier error que pueda ocurrir durante la descarga del archivo
        //        throw new Exception("Error al descargar el archivo desde Firebase Storage", ex);
        //    }
        //}




    }
}
