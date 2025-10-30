using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using KanbanAPI.Models;

namespace KanbanAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Coluna> Colunas { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


    }
}
