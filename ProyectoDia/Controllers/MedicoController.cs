using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDia.DataAccess;
using ProyectoDia.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProyectoDia.Controllers
{
    public class MedicoController : Controller
    {
        private readonly ApplicationDBContext _context;
        public MedicoController(ApplicationDBContext context)
        {
            _context = context;
        }
        //devuelve una vista con la lista de los medicos activos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listarMedicos = await _context.Medico.ToListAsync();
            List<Medico> listarMedicosActivos = new List<Medico>();
            foreach (var medico in listarMedicos) 
            {
                if (medico.Activo == true)
                {

                    listarMedicosActivos.Add(medico);
                }
            }
            return View(listarMedicosActivos);
        }
        //devuelve la vista necesaria para crear un medico
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();

        }
       //guarda el registro qur recibe como parametro en la base de datos 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medico medico)
        {
            medico.Activo = true;
            if (ModelState.IsValid)
            {
                _context.Medico.Add(medico);
                await _context.SaveChangesAsync();
                //una vez guardado, vuelve a index
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //devuelve la vista con la lista de todos los medicos
        public async Task<IActionResult> listarMedicos()
        {
        
            return View(await _context.Medico.ToListAsync());
        }
        //cambia el estado de activo a inactivo y biceversa
        public async Task<IActionResult> CambiarEstado(int? id)
        {
            var medico = await _context.Medico.FindAsync(id);

            if (medico == null)
            {
                return View();
            }
            if (medico.Activo)
            {
                medico.Activo = false;

            }
            else
            {
                medico.Activo = true;
            }

            _context.Medico.Update(medico);
            //guardar cambios
            await _context.SaveChangesAsync();
            //retornar Index
            return RedirectToAction(nameof(listarMedicos));
        }
    
        //metodo de la tecla edit de index para actualizar bbdd, 
        //al pulsar esta tecla recibo el id del medico desde index
        //aqui me muestra los datos existentes para que los cambie
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //busco en context el id y lo guardo en la variable
            var medico = _context.Medico.Find(id);

            if (medico == null)
            {
                return NotFound();
            }
            //si encontre el medico, retorno la vista enviando el medico
            return View(medico);
        }
       
        //metodo que actualizar un medico en la bbdd
        //aqui envia los datos modificados a la bbdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Medico medico)
        {
            medico.Activo = true;
          
            if (ModelState.IsValid)
            {

                _context.Medico.Update(medico);
                //guardar cambios
                await _context.SaveChangesAsync();
                //retornar Index
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }
       
        //boton detalles
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //busco en context el id y lo guardo en la variable
            var medico = _context.Medico.Find(id);

            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
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
