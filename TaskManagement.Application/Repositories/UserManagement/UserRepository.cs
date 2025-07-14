using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.UserManagement;
using TaskManagement.Persistence;

namespace TaskManagement.Application.Repositories.UserManagement;

public class UserRepository : IUserRepository
{
    private readonly TaskManagementDbContext _context;

    public UserRepository(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id) => await _context.Users.FindAsync(id);

    public async Task<List<User>> GetAllAsync(int skip, int take) =>
        await _context.Users.Skip(skip).Take(take).ToListAsync();

    public async Task<bool> EmailExistsAsync(string email) =>
        await _context.Users.AnyAsync(u => u.Email == email);

    public async System.Threading.Tasks.Task AddAsync(User user) => await _context.Users.AddAsync(user);

    public async System.Threading.Tasks.Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
