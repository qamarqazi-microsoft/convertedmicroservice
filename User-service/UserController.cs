using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<User>> GetById(Guid id)
