using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController] // Indica que é um controlador de API
    [Route("api/[controller]")] // Define a rota base para o controlador
    public class FilmeController : ControllerBase // Herdando de ControllerBase para funcionalidades de API
    {

        private static List<Filme> filmes = new List<Filme>(); // Lista estática para armazenar filmes
        private static int id = 0; // Contador estático para IDs

        [HttpPost("adicionar")]
        public IActionResult AdicionaFilme([FromBody] Filme filme) //[FromBody] indica que o filme virá do corpo da requisição
        {
            filme.Id = id++; // Atribui um ID único ao filme
            filmes.Add(filme); // Adiciona o filme à lista
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme); // Retorna 201 Created com a localização do novo recurso
            //CreatedAtAction cria uma resposta HTTP 201 com um cabeçalho Location apontando para a ação especificad
        }


        [HttpGet("visualizar")]
        public IEnumerable<Filme> RecuperaFilmes() => filmes; // Retorna a lista de filmes 
        // .Skip(1) pula o primeiro elemento
        // .Take(2) pega dois elementos a partir do ponto atual


        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id) // IActionResult permite retornar diferentes tipos de respostas HTTP
        {
            var filme = filmes.FirstOrDefault(filme => filme.Id == id); //FirstOrDefault retorna o primeiro elemento que satisfaz a condição ou null se nenhum for encontrado
            if (filme == null) return NotFound(); // Retorna 404 se o filme não for encontrado
            return Ok(filme); // Retorna 200 OK com o filme encontrado
        }
    }
}
