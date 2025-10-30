using System.ComponentModel.DataAnnotations;

namespace KanbanAPI.Models
{
    public class Tarefa
    {

        [Key]
        [Required]
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Responsavel { get; set; } 
        public int Progresso { get; set; } = 0;
        public int ColunaId { get; set; }
        public Coluna? Coluna { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
