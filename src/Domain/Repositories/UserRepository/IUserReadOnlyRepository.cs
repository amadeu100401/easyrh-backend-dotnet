using EasyRh.Domain.Entities;

namespace EasyRh.Domain.Repositories.UserRepository;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsActiveUserByEmail(string email);

    public Task<User> GetUserByEmail(string email);
}
