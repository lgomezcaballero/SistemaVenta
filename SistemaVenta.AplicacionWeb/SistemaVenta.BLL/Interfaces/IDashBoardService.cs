using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IDashBoardService
    {
        Task<int> TotalRequestUltimaSemana();   
        Task<int> TotalAssets();
        Task<int> TotalLocations();
        Task<int> TotalManagers();
        Task<int> TotalUserAssets();
        Task<int> TotalBusiness();
        Task<int> TotalRequest();
        Task<Dictionary<string, int>> RequestUltimaSemana();
        Task<Dictionary<string, int>> AssetTopUltimaSemana();
        Task<Dictionary<string, int>> AssetVsStatus();




    }
}
