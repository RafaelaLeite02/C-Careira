using System.ComponentModel.DataAnnotations;

namespace KanbanAPI.DTO
{
    public class UsuarioCreateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; } = string.Empty;
    }
}
