using EasyRh.Domain.Entities;

namespace EasyRh.Domain.Repositories.UserRepository;

public interface IUserWriteOnlyRepository
{
    public Task SaveUser(User user);
}
