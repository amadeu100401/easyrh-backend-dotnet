namespace EasyRh.Domain.Repositories;

public interface IUnityOfWork
{
    public Task commit();
}
