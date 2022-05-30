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
        private readonly ApplicationDBContext _context;
        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        //async method because for bringing data from the db is better to be async ???
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            return View(await _context.Paciente.ToListAsync());
        }

        //.............................................................
        //not need to be async ???
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //.............................................................
        //I call this method when I create a Patient
        //POST because it enter data to the DB
        [HttpPost]
        //protect the form 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            //validate the model
            //all the fields must be validated
            if (ModelState.IsValid)
            {
                //send data to the DB
                //save patient
                _context.Paciente.Add(paciente);
                //save changes
                await _context.SaveChangesAsync();
                //once the register is saved, return to Index page
                return RedirectToAction(nameof(Index));
            }
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
