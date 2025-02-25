using Api.Models;
using Api.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserInterface userInterface) : ControllerBase
    {
        // Create
        [HttpPost("add")]
        public async Task<IActionResult> Create(User user)
        {
            var result = await userInterface.CreateAsync(user);
            if (result)
                return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
            else 
                return BadRequest();
        }


        //Read all
        [HttpGet("get")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userInterface.GetAllAsync();
            if (!users.Any())
                return NotFound();
            else return Ok(users);
        }


        // Read single
        [HttpGet("get-single/{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await userInterface.GetByIdAsync(id);
            if (user is null)
                return NotFound();
            else
                return Ok(user);
        }


        // Update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var result = await userInterface.UpdateAsync(user);
            if (result)
                return Ok();
            else
                return NotFound();
        }


        // Delete
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await userInterface.DeleteAsync(id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }
    }
}
