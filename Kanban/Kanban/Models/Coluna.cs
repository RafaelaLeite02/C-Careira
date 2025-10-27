using System.ComponentModel.DataAnnotations;

namespace Kanban.Models
{
    public class Coluna
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}
