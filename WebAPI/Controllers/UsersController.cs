using Domain;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts.Interfaces;
using WebAPI.DTOs;

namespace WebAPI.Controllers;

[Route("users")]
public class UsersController(IUserRepository userRepository) : ControllerBase

{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = userRepository.GetAll();

        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await userRepository.GetByIdAsync(id);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var user = new User
        {
            UserName = dto.Username,
            Password = dto.Password
        };

        await userRepository.AddAsync(user);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        var userToUpdate = await userRepository.GetByIdAsync(id);
        userToUpdate.UserName = dto.Username ?? userToUpdate.UserName;
        userToUpdate.Password = dto.Password ?? userToUpdate.Password;
        await userRepository.UpdateAsync(userToUpdate);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        await userRepository.DeleteAsync(user);
        return Ok();
    }
}