using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Kanban.Models
{
    public class Tarefa
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Responsavel { get; set; }
        public int Progresso { get; set; } = 0; //0 a 100
        public int ColunaId { get; set; }
        public Coluna? Coluna { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        //? Você diz ao compilador: Esta propriedade pode ser null
        // string.Empty Você diz ao compilador: "Esta propriedade NÃO PODE ser null e, para garantir isso, seu valor inicial será sempre uma string vazia.
    }
}
