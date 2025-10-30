namespace KanbanAPI.DTO
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<TarefaDto> Tarefas { get; set; } = new List<TarefaDto>();
    }
}
