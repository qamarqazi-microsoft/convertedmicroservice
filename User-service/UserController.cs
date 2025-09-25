using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserMicroservice.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _