using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> options) // DbContextOptions é uma classe que contém as opções de configuração para o contexto do banco de dados.
            : base(options)
        {
        }

        public DbSet<Filme> Filmes { get; set; }// DbSet representa uma coleção de todas as entidades no contexto, ou que podem ser consultadas do banco de dados, do tipo especificado.
    }
}
