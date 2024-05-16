using EasyRh.Domain.Entities;
using EasyRh.Domain.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace EasyRh.Infra.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly EasyRhDbContext _dbContext;

    public UserRepository(EasyRhDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Operações de leitura
    public async Task<bool> ExistsActiveUserByEmail(string email) => await _dbContext.users.AnyAsync(user => user.Email.Equals(email) && user.Active);

    public async Task<User> GetUserByEmail(string email) => await _dbContext.users.FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Active);

    //Operações de escrita
    public async Task SaveUser(User user) => await _dbContext.users.AddAsync(user);
}
