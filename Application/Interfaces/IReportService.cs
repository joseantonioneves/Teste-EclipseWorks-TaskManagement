namespace Application.Interfaces
{
    public interface IReportService
    {
        int GetAverageCompletedTasksByUser(Guid userId);
        object GeneratePerformanceReport(Guid managerId);
    }
}
