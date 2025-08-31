namespace WorkflowTracking.Modules.WFManagment.Application.Abstractions.Data;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
