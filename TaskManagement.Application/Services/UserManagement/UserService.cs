using AutoMapper;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Repositories.UserManagement;
using TaskManagement.Application.Utils;
using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Application.Services.UserManagement;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    async Task<OperationResponse<CreateUserResponse>> IUserService.CreateAsync(CreateUserRequest request)
    {
        if (await _repository.EmailExistsAsync(request.Email))
        {
            return OperationResponse<CreateUserResponse>.FailedResponse()
                .AddError("User with this email already exists");
        }

        var user = User.New(request.Email, request.FirstName, request.LastName);
        await _repository.AddAsync(user);

        var response = _mapper.Map<CreateUserResponse>(user);
        return OperationResponse<CreateUserResponse>.SuccessfulResponse(response);
    }

    public async Task<OperationResponse<string>> DeleteAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
        {
            return OperationResponse<string>
                .FailedResponse(StatusCode.NotFound)
                .AddError("User not found");
        }

        await _repository.DeleteAsync(user);
        return OperationResponse<string>.SuccessfulResponse("User deleted successfully");
    }

    async Task<OperationResponse<List<GetUserResponse>>> IUserService.GetAllAsync(int page, int pageSize)
    {
        var users = await _repository.GetAllAsync((page - 1) * pageSize, pageSize);
        var mapped = _mapper.Map<List<GetUserResponse>>(users);
        return OperationResponse<List<GetUserResponse>>.SuccessfulResponse(mapped);
    }

    public async Task<OperationResponse<GetUserResponse>> GetByIdAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
        {
            return OperationResponse<GetUserResponse>
                .FailedResponse(StatusCode.NotFound)
                .AddError("User not found");
        }

        var mapped = _mapper.Map<GetUserResponse>(user);
        return OperationResponse<GetUserResponse>.SuccessfulResponse(mapped);
    }

    public async Task<OperationResponse<GetUserResponse>> UpdateAsync(Guid id, UpdateUserRequest request)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
        {
            return OperationResponse<GetUserResponse>
                .FailedResponse(StatusCode.NotFound)
                .AddError("User not found");
        }

        user.Update(request.FirstName, request.LastName, request.Email);
        await _repository.UpdateAsync(user);

        var mapped = _mapper.Map<GetUserResponse>(user);
        return OperationResponse<GetUserResponse>.SuccessfulResponse(mapped);
    }

}
