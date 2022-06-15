using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoDia.DataAccess;
using ProyectoDia.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDia.Controllers
{
    public class PacienteController : Controller
    {
        //accedo al contexto inyectandolo a traves de constructor
        private readonly ApplicationDBContext _context;
        public PacienteController(ApplicationDBContext context)
        {
            _context = context;
        }
        //metodo que recupera todos los pacientes de la base de datos
        //devuelve una vista y le pasa la lista de pacientes.
        [HttpGet]
        public async Task <IActionResult> listarPacientes()
        {
            //recupera los pacientes de la base de datos
            var pacientes = _context.Paciente.ToListAsync();
            //pasa el resultado a array
            var pacients = pacientes.Result.ToArray();
            int idMedico;
            Medico medico;
            //recupera el id del medico de cabecera de cada paciente
            //busca ese medico por id en la base de datos
            //establece ese medico como propiedad de ese paciente
            for (int i = 0; i < pacients.Length; i++)
            {
                idMedico = pacients[i].MedicoCabeceraId;
                medico = _context.Medico.Find(idMedico);
                pacients[i].MedicoCabecera = medico;
            }
            return View(pacients);
        }

        //metodo que recupera los pacientes activos y devuelve una vista con ellos
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var listaPacientesTodos = await _context.Paciente.ToListAsync();

            List<Paciente> listaPacientesActivos = new List<Paciente>();
            int idMedico;
            Medico medico;
            //recorre la lista total de  pacientes y chequea si la propiedad activo es true
            //si es asi, establece la propiedad medico y agrega el paciente a la lista
            foreach (var paciente in listaPacientesTodos)
            {
                if (paciente.Activo == true)
                {
                    idMedico = paciente.MedicoCabeceraId;
                    medico = _context.Medico.Find(idMedico);
                    paciente.MedicoCabecera = medico;
                    listaPacientesActivos.Add(paciente);
                }
            }
            return View(listaPacientesActivos);
        }

       //metodo que metodo que devuelve la vista necesaria para crear un paciente
        [HttpGet]
        public IActionResult Create()
        {
            //poblamos el desplegable con todos los medicos de la base de datos
            var listaMedicos = _context.Medico.ToList();
            List<SelectListItem> listaDropDown = listaMedicos.ConvertAll(d =>
            {
                var sli = new SelectListItem();
                if (d.Nombre != null)
                {
                    sli.Text = d.Nombre.ToString();

                    sli.Value = d.Id.ToString();

                    sli.Selected = false;
                }

                return sli;
            });
            //le pasamos a la vista la lisata de SelectListItem
            ViewBag.listaDropDown = listaDropDown;

            return View();
        }
        
        //metodo que recibe la informacion del usuario al da
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            //da valor al campo medicoCabecera buscandolo a traves de su id
            if (paciente.MedicoCabecera == null)
            {

                if (paciente.MedicoCabeceraId == 0 )
                {

                    return RedirectToAction(nameof(Index));
                }

                int medicoid = paciente.MedicoCabeceraId;
                var medico = _context.Medico.Find(medicoid);
                paciente.MedicoCabecera = medico;
                paciente.MedicoCabeceraId = medico.Id;
            }
            //pone el campo activo en true
            paciente.Activo = true;
            //valido los campos
                if (ModelState.IsValid)
            {
                //agrego el paciente a la tabla paciente
                _context.Paciente.Add(paciente);
                //guardo
                await _context.SaveChangesAsync();
                //ocon el registro guardado, retornamos a Index
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
      
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
            //pueblo el dropdown con todos los medicos
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
            paciente.Activo = true;

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

        
        //boton detalles recibe id de la vista.
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
        //metodo que cambia el estado del paciente de activo a inactivo
        public async Task<IActionResult> CambiarEstado(int? id)
        {
            var paciente = await _context.Paciente.FindAsync(id);

            if (paciente == null)
            {
                return View();
            }
            if (paciente.Activo)
            {
            paciente.Activo = false;

            }
            else
            {
                paciente.Activo = true;
            }

            _context.Paciente.Update(paciente);
            //guardar cambios
            await _context.SaveChangesAsync();
            //retornar Index
            return RedirectToAction(nameof(listarPacientes));
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
