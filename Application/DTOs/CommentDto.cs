namespace Application.DTOs
{
    public class CommentDto
    {
        public string? Text { get; set; } // Conteúdo do comentário
        public DateTime CreatedAt { get; set; } // Data de criação do comentário
        public Guid CreatedBy { get; set; } // Identificação do usuário que criou o comentário
    }
}
