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
    public class VisitaMedicaController : Controller
    {
        private readonly ApplicationDBContext _context;
        public VisitaMedicaController(ApplicationDBContext context)
        {
            _context = context;
        }
        //recupera todas las visitas medicas de la base de datos
        //devuelve una vista con esa lista de visitas medicas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var visitasMedicas = _context.VisitaMedica.ToListAsync();
            var visita = visitasMedicas.Result.ToArray();
            int idMedico;
            int idPaciente;
            Medico medico;
            Paciente paciente;

            for (int i = 0; i < visita.Length; i++)
            {
                idMedico = visita[i].MedicoId;
                idPaciente = visita[i].PacienteId;
                medico = _context.Medico.Find(idMedico);
                paciente = _context.Paciente.Find(idPaciente);
                visita[i].Medico = medico;
                visita[i].Paciente = paciente;
            }

            return View(visita);
        }
        //devuelve la vista necesaria para crear una visita medica
        [HttpGet]
        public IActionResult Create()
        {
            var listaMedicos = _context.Medico.ToList();

            List<SelectListItem> listaDropDownMedicos = listaMedicos.ConvertAll(d =>
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

            ViewBag.listaDropDownMedicos = listaDropDownMedicos;
            //recupera y pinta combo de pacientes
            var listaPacientes = _context.Paciente.ToList();

            List<SelectListItem> listaDropDownPacientes = listaPacientes.ConvertAll(d =>
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
            ViewBag.listaDropDownPacientes = listaDropDownPacientes;

            return View();
        }
        //metodo que recibe la visita medica del formulario y la guarda en la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VisitaMedica visitaMedica)
        {
            if (visitaMedica.Medico == null && visitaMedica.Paciente == null)
            {
                int medicoid = visitaMedica.MedicoId;
                var medico = _context.Medico.Find(medicoid);
                visitaMedica.Medico = medico;
                visitaMedica.MedicoId = medico.Id;
                //Paciente
                int pacienteid = visitaMedica.PacienteId;
                var paciente = _context.Paciente.Find(pacienteid);
                visitaMedica.Paciente = paciente;
                visitaMedica.PacienteId = paciente.Id;
            }

            if (ModelState.IsValid)
            {
                _context.VisitaMedica.Add(visitaMedica);
                await _context.SaveChangesAsync();
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
            var vm = _context.VisitaMedica.Find(id);

            if (vm == null)
            {
                return NotFound();
            }
            //recupera y pinta combo de medicos
            var listaMedicos = _context.Medico.ToList();

            List<SelectListItem> listaDropDownMedicos = listaMedicos.ConvertAll(d =>
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

            ViewBag.listaDropDownMedicos = listaDropDownMedicos;
            //recupera y pinta combo de pacientes
            var listaPacientes = _context.Paciente.ToList();

            List<SelectListItem> listaDropDownPacientes = listaPacientes.ConvertAll(d =>
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
            ViewBag.listaDropDownPacientes = listaDropDownPacientes;
            //si encontre el paciente, retorno la vista enviando el paciente
            return View(vm);
        }

        //metodo que actualizar una visita medica en la bbdd
        //aqui envia los datos modificados a la bbdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VisitaMedica visitaMedica)
        {
            if (visitaMedica.Medico == null)
            {
                int medicoid = visitaMedica.MedicoId;
                var medico = _context.Medico.Find(medicoid);
                visitaMedica.Medico = medico;
                visitaMedica.MedicoId = medico.Id;
            }
            if (visitaMedica.Paciente == null)
            {
                int pacienteid = visitaMedica.PacienteId;
                var paciente = _context.Paciente.Find(pacienteid);
                visitaMedica.Paciente = paciente;
                visitaMedica.PacienteId = paciente.Id;
            }

            if (ModelState.IsValid)
            {
                _context.VisitaMedica.Update(visitaMedica);
                //guardar cambios
                await _context.SaveChangesAsync();
                //retornar Index
                return RedirectToAction(nameof(Index));
            }
            return View(visitaMedica);
        }

        //boton detalles
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //busco en context por id y lo guardo en la variable
            var visita = _context.VisitaMedica.Find(id);

            if (visita == null)
            {
                return NotFound();
            }

            int idMedico;
            int idPaciente;
            Medico medico;
            Paciente paciente;

            idMedico = visita.MedicoId;
            idPaciente = visita.PacienteId;
            medico = _context.Medico.Find(idMedico);
            paciente = _context.Paciente.Find(idPaciente);
            visita.Medico = medico;
            visita.Paciente = paciente;

            return View(visita);
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
