using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProyectoDia.DataAccess;
using ProyectoDia.Models;
using ProyectoDia.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDia.Controllers
{
    public class HomeController : Controller
    {
        //Use the context by injecting it into the constructor of the controller
        private readonly ApplicationDBContext _contexto;
        public HomeController(ApplicationDBContext contexto)
        {
            _contexto = contexto;
        }

        //async method because for bringing data from the db is better to be async ???
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            return View(await _contexto.Paciente.ToListAsync());
        }

        //.............................................................
        //not need to be async ???
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
