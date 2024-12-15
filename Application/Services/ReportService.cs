using Application.Interfaces;
using Domain.Interfaces;  // Referência à interface de repositório no Domain

namespace Application.Services
{
    public class ReportService: IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public ReportService(IReportRepository reportRepository, ITaskRepository taskRepository, IUserRepository userRepository)
        {
            _reportRepository = reportRepository;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public int GetAverageCompletedTasksByUser(Guid userId)
        {
            return _reportRepository.GetAverageCompletedTasksByUser(userId);
        }

        public object GeneratePerformanceReport(Guid managerId)
        {
            // Lógica para gerar o relatório de desempenho
            var user = _userRepository.GetById(managerId);
            if (user == null)
                throw new Exception("Manager not found.");

            // Aqui você pode buscar tarefas, calcular métricas e formatar o relatório
            var tasks = _taskRepository.GetTasksByManagerId(managerId);
            var report = new
            {
                ManagerName = user.Name,
                TaskCount = tasks.Count(),
                CompletedTasks = tasks.Count(t => t.Status == Domain.ValueObjects.TaskStatus.Completada),
                PendingTasks = tasks.Count(t => t.Status == Domain.ValueObjects.TaskStatus.Pendente)
            };

            return report;
        }
    }
}
