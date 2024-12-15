namespace Domain.Interfaces
{
    public interface IReportRepository
    {
        int GetAverageCompletedTasksByUser(Guid userId, int days = 30);
    }
}
