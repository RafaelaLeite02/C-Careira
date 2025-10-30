namespace KanbanAPI.DTO
{
    public class TarefaUpdateDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Responsavel { get; set; }
        public int Progresso { get; set; }
        public int ColunaId { get; set; }
        public int UsuarioId { get; set; }
    }
}
