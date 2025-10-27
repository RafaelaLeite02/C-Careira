using KanbanAPI.Data;
using KanbanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : Controller
    {
        private readonly AppDbContext _context;

        public TarefaController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        { 
            var tarefas = await _context.Tarefas
                .Include(t => t.Coluna)
                .Include(t => t.Usuario)
                .ToListAsync();

            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var tarefa = await _context.Tarefas
                .Include(t => t.Coluna)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa == null)
                return NotFound(new { mesagem = "Tarefa não encontrada." });
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tarefa tarefa) 
        {
            var usuario = await _context.Usuarios.FindAsync(tarefa.UsuarioId);
            if (usuario == null)
                return BadRequest(new { mensagem = "Usuario invalido" });

            var coluna = await _context.Colunas.FindAsync(tarefa.ColunaId);
            if (coluna == null)
                return BadRequest(new { mensagem = "Coluna invalida " });

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = tarefa.Id }, tarefa);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tarefa tarefa) 
        {
            var tarefaExistente = await _context.Tarefas.FindAsync(id);
            if (tarefaExistente == null)
                return NotFound(new { mensagem = "Tarefa não encontrada" });

            tarefaExistente.Titulo = tarefa.Titulo;
            tarefaExistente.Descricao = tarefa.Descricao;
            tarefaExistente.Progresso = tarefa.Progresso;
            tarefaExistente.ColunaId = tarefa.ColunaId;
            tarefaExistente.UsuarioId = tarefa.UsuarioId;

            await _context.SaveChangesAsync();
            return NoContent();

        }


    }

}
