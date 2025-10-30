namespace KanbanAPI.Controllers;

using KanbanAPI.Data;
using KanbanAPI.DTO;
using KanbanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]

public class ColunaController : ControllerBase
{
    private readonly AppDbContext _context;

    public ColunaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Coluna>>> GetAll() 
    { 
        return await _context.Colunas.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Coluna>> Create([FromBody] ColunaCreateDto dto) 
    {
        if (string.IsNullOrWhiteSpace(dto.Nome)) 
            return BadRequest(new { mensagem = "Nome da coluna é obrigatorio" });

        var coluna = new Coluna()
        {
            Nome = dto.Nome,
        };

        _context.Colunas.Add(coluna);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = coluna.Id }, coluna );
    }
}
