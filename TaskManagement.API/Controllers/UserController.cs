using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Extensions;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Services.UserManagement;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _userService.GetByIdAsync(id);
            return response.ResponseResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var response = await _userService.CreateAsync(request);
            return response.ResponseResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var response = await _userService.GetAllAsync(page, pageSize);
            return response.ResponseResult();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateUserRequest request)
        {
            var response = await _userService.UpdateAsync(id, request);
            return response.ResponseResult();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _userService.DeleteAsync(id);
            return response.ResponseResult();
        }
    }
}
