namespace KanbanAPI.DTO
{
    public class TarefaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Responsavel { get; set; }
        public int Progresso { get; set; }
        public int ColunaId { get; set; }
        public string? ColunaNome { get; set; }
        public int UsuarioId { get; set; }
        public string? UsuarioNome { get; set; }
    }
}
