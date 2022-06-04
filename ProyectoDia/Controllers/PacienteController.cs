using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoDia.DataAccess;
using ProyectoDia.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDia.Controllers
{
    public class PacienteController : Controller
    {
        //Use the context by injecting it into the constructor of the controller
        private readonly ApplicationDBContext _context;
        public PacienteController(ApplicationDBContext context)
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
            var listaMedicos = _context.Medico.ToList();


            List<SelectListItem> listaDropDown = listaMedicos.ConvertAll(d =>
            {
                var sli = new SelectListItem();
                if (d.Nombre != null)
                {
                    sli.Text = d.Nombre.ToString();

                }


                sli.Value = d.Id.ToString();

                sli.Selected = false;



                return sli;
            });

            ViewBag.listaDropDown = listaDropDown;

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

            if (paciente.MedicoCabecera == null)
            {

                int medicoid = paciente.MedicoCabeceraId;
                var medico = _context.Medico.Find(medicoid);
                paciente.MedicoCabecera = medico;
                paciente.MedicoCabeceraId = medico.Id;
            }


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
        //.............................................................
        //metodo de la tecla edit de index para actualizar bbdd, 
        //al pulsar esta tecla recibo el id del paciente desde index
        //aqui me muestra los datos existentes para que los cambie
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //busco en context el id y lo guardo en la variable
            var paciente = _context.Paciente.Find(id);

            if (paciente == null)
            {
                return NotFound();
            }

            var listaMedicos = _context.Medico.ToList();


            List<SelectListItem> listaDropDown = listaMedicos.ConvertAll(d =>
            {
                var sli = new SelectListItem();
                if (d.Nombre != null)
                {
                    sli.Text = d.Nombre.ToString();

                }


                sli.Value = d.Id.ToString();

                sli.Selected = false;



                return sli;
            });

            ViewBag.listaDropDown = listaDropDown;

            
            //si encontre el paciente, retorno la vista enviando el paciente
            return View(paciente);
        }

        //.......................................................
        //metodo que actualizar un usuario en la bbdd
        //aqui envia los datos modificados a la bbdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Paciente paciente)
        {
            if (paciente.MedicoCabecera == null)
            {

                int medicoid = paciente.MedicoCabeceraId;
                var medico = _context.Medico.Find(medicoid);
                paciente.MedicoCabecera = medico;
                paciente.MedicoCabeceraId = medico.Id;
            }

            if (ModelState.IsValid)
            {

                _context.Paciente.Update(paciente);
                //guardar cambios
                await _context.SaveChangesAsync();
                //retornar Index
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        //..........................................................
        //boton detalles
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //busco en context el id y lo guardo en la variable
            var paciente = _context.Paciente.Find(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        //............................................................
        //boton borrar
        //recibo el id de index
        //aqui obtiene los registros de la bbdd mediante el id que es un campo oculto
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //busco en context el id y lo guardo en la variable
            var paciente = _context.Paciente.Find(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        //..........................................................
        //metodo borrar
        //aqui envia los cambios a la bbdd
        //no puede ser delete porque ya esta creado con la misma cantidad de parametros
        //action name sera delete porque asi esta en el formulario de la vista
        //para que vea el nombre delete aunque se llame deleteregistro
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRegistro(int? id)
        {
            var paciente = await _context.Paciente.FindAsync(id);

            if (paciente == null)
            {
                return View();
            }


            _context.Paciente.Remove(paciente);
            //guardar cambios
            await _context.SaveChangesAsync();
            //retornar Index
            return RedirectToAction(nameof(Index));


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
