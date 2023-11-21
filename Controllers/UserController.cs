using ConversorDeMonedasBack.Data.Interfaces;
using ConversorDeMonedasBack.Entities;
using ConversorDeMonedasBack.Models.Dtos;
using ConversorDeMonedasBack.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConversorDeMonedasBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == UserRoleEnum.Admin.ToString())
            {
                return Ok(_userService.GetAllUsers());
            }
             return Forbid();
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            if (userId == 0)
            {
                return BadRequest("El ID ingresado debe ser distinto de 0");
            }

            User? user = _userService.GetUserById(userId);
            if (user is null)
            {
                return NotFound();
            }

            var dto = new GetUserByIdResponse()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                SuscriptionId = user.SubscriptionId,
                Conversions = user.Conversions,
                Role = user.Role
            };

            return Ok(dto);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateUser(CreateUserDto dto)
        {
            try
            {
                _userService.CreateUser(dto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el usuario: {ex.Message}");
            }
            return Created("Created", dto);
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(UpdateUserDto dto, int userId)
        {
            if (!_userService.CheckIfUserExists(userId))
            {
                return NotFound();
            }
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == "Admin")
            {
                try
                {
                    _userService.UpdateUser(dto, userId);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                return NoContent();
            }
            return Forbid();
        }

        [HttpDelete]
        public IActionResult DeleteUser(int userId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            
            if (role == "Admin")
            {
                User? user = _userService.GetUserById(userId);

                if (user is null)
                {
                    return BadRequest("El cliente que intenta eliminar no existe");
                }
                if (user.FirstName != "Admin")
                {
                    _userService.DeleteUser(userId);
                }
                return NoContent();
            }
            return Forbid();

        }
    }
}

