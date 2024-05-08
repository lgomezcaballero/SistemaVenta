using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface INegocioService
    {
        Task<Negocio> Obtener();
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        Task<Negocio> GuardarCambios(Negocio entidad, Stream Logo = null, string NombreLogo = "");
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.

    }
}
