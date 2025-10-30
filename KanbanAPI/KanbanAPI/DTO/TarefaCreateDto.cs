namespace KanbanAPI.DTO
{
    public class TarefaCreateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Responsavel { get; set; } = string.Empty;
        public int Progresso { get; set; }
        public int ColunaId { get; set; }
        public int UsuarioId { get; set; }
    }
}
