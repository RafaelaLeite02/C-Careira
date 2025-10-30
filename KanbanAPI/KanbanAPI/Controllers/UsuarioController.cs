using KanbanAPI.Data;
using KanbanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanbanAPI.DTO;

namespace KanbanAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {


        private readonly AppDbContext _context;

            public UsuarioController(AppDbContext context){ 
                _context = context;
            }

            [HttpGet] //Buscar tudo 
            public async Task<IActionResult> GetAll() 
            {
                var usuario = await _context.Usuarios //await diz ao seu programa para "pausar" a execução deste método e esperar o resultado da operação do banco de dados, sem travar a aplicação inteira
                    .Include(u => u.Tarefas)
                    .Select(u => new UsuarioDto 
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        Email = u.Email,
                        Tarefas = u.Tarefas.Select(T => new TarefaDto{
                            Id = T.Id,
                            Titulo = T.Titulo,
                            Progresso = T.Progresso
                        }).ToList()
                    })
                    .ToListAsync();
                return Ok(usuario);
            }

            [HttpGet("{id}")] //Buscar por Id
            public async Task<IActionResult> GetById(int id) {
                var usuario = await _context.Usuarios
                    .Include(u => u.Tarefas)
                    .FirstOrDefaultAsync(u => u.Id == id);

                    if (usuario == null) return NotFound();
                return Ok(usuario);
            }

            [HttpPost] //Criar
            public async Task<IActionResult> Create([FromBody]UsuarioCreateDto dto)
            {

                if(!ModelState.IsValid)
                return BadRequest(ModelState);

                var usuario = new Usuario()
                {
                    Nome = dto.Nome,
                    Email = dto.Email
                };
           
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAll), new { id = usuario.Id }, usuario); //nameof(GetById) pega o nome do seu método GetById (que provavelmente existe no seu controller) e o transforma na string "GetById".
            }

            [HttpPut("{id}")] //Atualizar
            public async Task<IActionResult> Update(int id, [FromBody] UsuarioUpdateDto usuarioDto)
            {
                var existente = await _context.Usuarios.FindAsync(id); //FindAsync primeiro verifica se o objeto já está sendo rastreado pelo DbContext (na memória/cache). Se estiver, ele o retorna imediatamente, sem nem mesmo fazer uma consulta ao banco.
                if (existente == null) return NotFound();

                existente.Nome = usuarioDto.Nome;
                existente.Email = usuarioDto.Email;

                await _context.SaveChangesAsync();
            return Ok(existente);
            }

            [HttpDelete("{id}")] //Deletar
            public async Task<IActionResult> Delete(int id) 
            {
                var usuario = await _context.Usuarios.FindAsync(id); 
                if (usuario == null) return NotFound();

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return NoContent();

            }

        
    }
}
