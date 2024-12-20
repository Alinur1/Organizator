using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _iUsers;

        public UsersController(IUsers iUsers)
        {
            _iUsers = iUsers;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var obj = await _iUsers.GetAllUsersAsync();
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsersById(int id)
        {
            var obj = await _iUsers.GetUsersByIdAsync(id);
            if(obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsers([FromBody] Users users)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = await _iUsers.AddUsersAsync(users);
            return CreatedAtAction(nameof(AddUsers), new {id = users.UserID}, obj);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsers(int id, [FromBody] Users users)
        {
            if(id != users.UserID)
            {
                return BadRequest("User ID mismatch/User not found.");
            }
            try
            {
                var obj = await _iUsers.UpdateUsersAsync(users);
                return Ok(obj);
            }
            catch(KeyNotFoundException)
            {
                return NotFound("Something went wrong. Please try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var obj = await _iUsers.DeleteUsersAsync(id);
            if(!obj)
            {
                return NotFound();
            }
            return Ok("User deleted successfully.");
        }
    }
}
