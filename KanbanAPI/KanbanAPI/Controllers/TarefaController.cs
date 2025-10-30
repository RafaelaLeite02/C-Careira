using KanbanAPI.Data;
using KanbanAPI.DTO;
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
                    .Select(t => new TarefaDto
                    {
                        Id = t.Id,
                        Titulo = t.Titulo,
                        Descricao = t.Descricao,
                        Responsavel = t.Responsavel,
                        Progresso = t.Progresso,
                        ColunaId = t.ColunaId,
                        ColunaNome = t.Coluna.Nome,
                        UsuarioId = t.UsuarioId,
                        UsuarioNome = t.Usuario.Nome
                    })
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
        public async Task<IActionResult> Create([FromBody] TarefaCreateDto dto) 
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null)
                return BadRequest(new { mensagem = "Usuario invalido" });

            var coluna = await _context.Colunas.FindAsync(dto.ColunaId);
            if (coluna == null)
                return BadRequest(new { mensagem = "Coluna invalida " });

            var tarefa = new Tarefa
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Responsavel = dto.Responsavel,
                Progresso = dto.Progresso,
                ColunaId = dto.ColunaId,
                UsuarioId = dto.UsuarioId
            };
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = tarefa.Id }, tarefa);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TarefaUpdateDto dto) 
        {
            var tarefaExistente = await _context.Tarefas.FindAsync(id);
            if (tarefaExistente == null)
                return NotFound(new { mensagem = "Tarefa não encontrada" });

            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null)
                return BadRequest(new { mensagem = "Usuário inválido" });

            var coluna = await _context.Colunas.FindAsync(dto.ColunaId);
            if (coluna == null)
                return BadRequest(new { mensagem = "Coluna inválida" });

            tarefaExistente.Titulo = dto.Titulo;
            tarefaExistente.Descricao = dto.Descricao;
            tarefaExistente.Progresso = dto.Progresso;
            tarefaExistente.ColunaId = dto.ColunaId;
            tarefaExistente.UsuarioId = dto.UsuarioId;
            tarefaExistente.Responsavel = dto.Responsavel;

            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound(new { mensagem = "Tarefa não enontrada" });

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpPut("{id}/move/{colunaId}")]
        public async Task<IActionResult> MoverTarefa(int id, int colunaId) 
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound(new { mensagem = "Tarefa não encontrada" });
            
            var colunaDestino = await _context.Colunas.FindAsync(colunaId);
            if (colunaDestino == null)
                return BadRequest(new { mensagem = "Coluna invalida" });

            tarefa.ColunaId = colunaId;

            switch (colunaDestino.Nome.ToLower())
            {
                case "a fazer":
                    tarefa.Progresso = 0;
                    break;
                case "fazendo":
                    tarefa.Progresso = 50;
                    break;
                case "feito":
                    tarefa.Progresso = 100;
                    break;
            }

            await _context.SaveChangesAsync();
            return Ok(tarefa);

        }


    }

}
