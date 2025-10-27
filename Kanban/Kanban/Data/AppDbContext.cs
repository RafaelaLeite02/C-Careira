using Microsoft.EntityFrameworkCore;
using Kanban.Models;

namespace Kanban.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Coluna> Colunas { get; set;  }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
