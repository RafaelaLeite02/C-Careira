using FilmesAPI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace FilmesAPI.Controllers
{
    [ApiController] // Indica que é um controlador de API
    [Route("[controller]")] // Define a rota base para o controlador
    public class FilmeController : ControllerBase // Herdando de ControllerBase para funcionalidades de API
    {

        private static List<Filme> filmes = new List<Filme>(); // Lista estática para armazenar filmes

        [HttpPost("adicionar")]
        public void AdicionaFilme([FromBody] Filme filme) //[FromBody] indica que o filme virá do corpo da requisição
        {
            filmes.Add(filme); // Adiciona o filme à lista
            Console.WriteLine(filme.Titulo);
            Console.WriteLine(filme.Duracao);
        }

    }
}
