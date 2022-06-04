using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDia.DataAccess;
using ProyectoDia.Models;
using ProyectoDia.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProyectoDia.Controllers
{
    public class MedicoController : Controller
    {
          //Use the context by injecting it into the constructor of the controller
        private readonly ApplicationDBContext _context;

        public MedicoController(ApplicationDBContext context)
        {
            _context = context;
        }

        //async method because for bringing data from the db is better to be async ???
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medico.ToListAsync());
        }
        //.............................................................
        //not need to be async ???
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
            /* MedicoViewModel mvm = new MedicoViewModel();
             mvm.ListaPacientes = (System.Collections.Generic.IEnumerable<Business.Models.Paciente>)await _context.Paciente.ToListAsync();
             return View(mvm);*/
        }
        //.............................................................
        //I call this method when I create a Patient
        //POST because it enter data to the DB
        [HttpPost]
        //protect the form 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medico medico)
        {
           
            //validate the model
            //all the fields must be validated
            if (ModelState.IsValid)
            {
                //send data to the DB
                //save patient
                _context.Medico.Add(medico);
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
            var medico = _context.Medico.Find(id);

            if (medico == null)
            {
                return NotFound();
            }
            //si encontre el paciente, retorno la vista enviando el paciente
            return View(medico);
        }
        //.......................................................
        //metodo que actualizar un usuario en la bbdd
        //aqui envia los datos modificados a la bbdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Medico medico)
        {
          
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
            var medico = _context.Medico.Find(id);

            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
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
            var medico = _context.Medico.Find(id);

            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
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
            var medico = await _context.Medico.FindAsync(id);

            if (medico == null)
            {
                return View();
            }


            _context.Medico.Remove(medico);
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
