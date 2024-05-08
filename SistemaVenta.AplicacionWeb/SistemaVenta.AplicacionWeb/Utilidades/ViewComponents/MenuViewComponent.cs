
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces;

namespace SistemaVenta.AplicacionWeb.Utilidades.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IMenuService _menuServicio;
        private readonly IMapper _mapper;
        public MenuViewComponent(IMenuService menuServicio,
            IMapper mapper)
        {
            _menuServicio = menuServicio;
            _mapper = mapper;
        }


        public async Task<IViewComponentResult> InvokeAsync() {

            ClaimsPrincipal claimUser = HttpContext.User;
            List<VMMenu> listaMenus;


#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (claimUser.Identity.IsAuthenticated)
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                listaMenus = _mapper.Map<List<VMMenu>>(await _menuServicio.ObtenerMenus(int.Parse(idUsuario)));
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            }
            else { 
                listaMenus = new List<VMMenu> { };
            }
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return View(listaMenus);

        }

    }
}
