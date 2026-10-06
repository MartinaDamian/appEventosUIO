using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuitoVibesMvc.Data;
using QuitoVibesMvc.Models;

namespace QuitoVibesMvc.Controllers
{
    [Authorize] // 🔒 PROTECCIÓN STRICTA: Solo accesible para usuarios autenticados
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eventos (Read - Listar)
        public async Task<IActionResult> Index(string? search, string? categoria, string? lugar)
        {
            var query = _context.Eventos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.Titulo.ToLower().Contains(search.ToLower()) ||
                                         e.Descripcion.ToLower().Contains(search.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(categoria))
            {
                query = query.Where(e => e.Categoria == categoria);
            }

            if (!string.IsNullOrWhiteSpace(lugar))
            {
                query = query.Where(e => e.Lugar == lugar);
            }

            ViewData["CurrentSearch"] = search;
            ViewData["CurrentCategoria"] = categoria;
            ViewData["CurrentLugar"] = lugar;

            var eventos = await query.OrderByDescending(e => e.Fecha).ToListAsync();
            return View(eventos);
        }

        // GET: Eventos/Details/5 (Read - Detalles)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create (Create - Formulario)
        public IActionResult Create()
        {
            return View(new EventoCultural());
        }

        // POST: Eventos/Create (Create - Guardar)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,Lugar,Direccion,Fecha,Hora,Precio,Categoria,Vibra,ImagenUrl,Organizador,Estado")] EventoCultural evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "¡Evento cultural creado exitosamente en la agenda!";
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Eventos/Edit/5 (Update - Formulario)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5 (Update - Guardar)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,Lugar,Direccion,Fecha,Hora,Precio,Categoria,Vibra,ImagenUrl,Organizador,Estado")] EventoCultural evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "¡Evento actualizado correctamente!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Eventos/Delete/5 (Delete - Confirmación)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5 (Delete - Eliminar)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "El evento ha sido eliminado de la agenda de Quito.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }
    }
}
