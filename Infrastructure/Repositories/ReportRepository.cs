using Domain.Interfaces; // Alterado para referenciar a interface no Domain

namespace Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly TaskRepository _taskRepository;
        private readonly UserRepository _userRepository;

        public ReportRepository(TaskRepository taskRepository, UserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        // Método que gera o relatório de desempenho, calculando a média de tarefas concluídas por usuário
        public int GetAverageCompletedTasksByUser(Guid userId, int days = 30)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new Exception("User not found.");

            // Filtra as tarefas que foram concluídas nos últimos 'days' dias
            var completedTasks = _taskRepository.GetCompletedTasksByUser(userId, DateTime.Now.AddDays(-days), DateTime.Now);

            if (completedTasks.Count() == 0)
                return 0; // Retorna 0 se não houver tarefas concluídas no período

            // Calcula a média de tarefas concluídas
            return completedTasks.Count() / days;
        }
    }
}
