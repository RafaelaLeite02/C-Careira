using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController] // Indica que é um controlador de API
    [Route("api/[controller]")] // Define a rota base para o controlador
    public class FilmeController : ControllerBase // Herdando de ControllerBase para funcionalidades de API
    {
        private readonly FilmeContext _context;
        private readonly IMapper _mapper; 

        public FilmeController(FilmeContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        
        [HttpPost("adicionar")]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDto) //[FromBody] indica que o filme virá do corpo da requisição
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme); // Adiciona o filme ao DbSet
            _context.SaveChanges(); // Salva as mudanças no banco de dados
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme); // Retorna 201 Created com a localização do novo recurso
            //CreatedAtAction cria uma resposta HTTP 201 com um cabeçalho Location apontando para a ação especificad
        }

        /// <summary>
        /// Visualizar od filmes que estão no banco de dados
        /// </summary>
        [HttpGet("visualizar")]
        public IEnumerable<ReadFilmeDTO> RecuperaFilmes()
        {
            return _mapper.Map<List<ReadFilmeDTO>>(_context.Filmes); // Retorna a lista de filmes 
        }
        // .Skip(1) pula o primeiro elemento
        // .Take(2) pega dois elementos a partir do ponto atual

        /// <summary>
        /// Visualizar um filme pelo id no banco de dados
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id) // IActionResult permite retornar diferentes tipos de respostas HTTP
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id); //FirstOrDefault retorna o primeiro elemento que satisfaz a condição ou null se nenhum for encontrado
            if (filme == null) return NotFound(); // Retorna 404 se o filme não for encontrado
            var filmeDto = _mapper.Map<ReadFilmeDTO>(filme);
            return Ok(filmeDto); // Retorna 200 OK com o filme encontrado
        }

        /// <summary>
        /// Atualizar um filme no banco de dados
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault( 
                filme => filme.Id == id); //Procurar o filme
            if(filme == null) return NotFound(); // Ver se não é nulo o filme
            _mapper.Map(filmeDto, filme); // Mapear para ser o DTO
            _context.SaveChanges();  // Salvar no banco a atualização
            return NoContent(); //Retorno mais certo para atualização (compativel com o codigo)
        }

        /// <summary>
        /// Atualizar Parcialmente um filme no banco de dados
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDTO> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(
                filme => filme.Id == id); 
            if (filme == null) return NotFound();

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDTO>(filme);
            patch.ApplyTo(filmeParaAtualizar, ModelState); // ApplyTo é para atribuir e o ModelState é para o estado
            if (!TryValidateModel(filmeParaAtualizar)) // Validação de estado
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmeParaAtualizar, filme); 
            _context.SaveChanges();  
            return NoContent(); 
        }

        /// <summary>
        /// Deletar um flme do banco de dados
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(
                filme => filme.Id == id);
            if (filme == null) return NotFound();
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }


    }
}
