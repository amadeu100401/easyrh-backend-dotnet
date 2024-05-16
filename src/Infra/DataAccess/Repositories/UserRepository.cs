using EasyRh.Domain.Repositories.User;
using EasyRh.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EasyRh.Infra.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository
{
    private readonly EasyRhDbContext _dbContext;

    public UserRepository(EasyRhDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsActiveUserByEmail(string email) => await _dbContext.users.AnyAsync(user => user.Email.Equals(email) && user.Active);
}
