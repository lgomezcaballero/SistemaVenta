
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class ArchivoController : Controller
    {
        public ActionResult Subir()
        {
            return View();
        }
    }
}
