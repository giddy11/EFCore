using TaskManagement.Application.Dtos;
using TaskManagement.Application.Utils;

namespace TaskManagement.Application.Services.UserManagement;

public interface IUserRepository
{
    Task<OperationResponse<GetUserResponse>> GetByIdAsync(Guid id);
    Task<OperationResponse<CreateUserResponse>> CreateAsync(CreateUserRequest request);
    Task<OperationResponse<List<GetUserResponse>>> GetAllAsync(int page, int pageSize);
    Task<OperationResponse<GetUserResponse>> UpdateAsync(Guid id, UpdateUserRequest request);
    Task<OperationResponse<string>> DeleteAsync(Guid id);
}
