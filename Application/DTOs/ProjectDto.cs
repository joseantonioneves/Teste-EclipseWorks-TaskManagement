namespace Application.DTOs
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<TaskDto> Tasks { get; set; }
        public Guid UserId { get; set; }  // Adicione a propriedade UserId
        public Guid AssignedUserId { get; set; }
    }
}
