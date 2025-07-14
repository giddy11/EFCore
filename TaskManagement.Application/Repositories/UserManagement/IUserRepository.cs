using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Application.Repositories.UserManagement;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync(int skip, int take);
    Task<bool> EmailExistsAsync(string email);
    System.Threading.Tasks.Task AddAsync(User user);
    System.Threading.Tasks.Task UpdateAsync(User user);
    System.Threading.Tasks.Task DeleteAsync(User user);
}
