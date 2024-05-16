using EasyRh.Domain.Repositories;

namespace EasyRh.Infra.DataAccess;

public class UnityOfWork : IUnityOfWork
{
    private readonly EasyRhDbContext _dbContext;

    public UnityOfWork(EasyRhDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task commit() => await _dbContext.SaveChangesAsync();
}
