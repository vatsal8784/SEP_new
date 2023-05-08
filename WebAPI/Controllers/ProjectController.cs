using App.Logic;
using App.LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectLogic _projectLogic;

    public ProjectController(IProjectLogic projectLogic)
    {
        this._projectLogic = projectLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Projects>> CreateAsync(CreateProjectDTO dto)
    {
        try
        {
            Projects created = await _projectLogic.CreateProjectAsync(dto);
            return Created($"/todos/{created.id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Projects>>> GetAsync([FromQuery] string? userName,
        [FromQuery] int? userId,
        [FromQuery] bool? completedStatus, [FromQuery] string? titleContains)
    {
        try
        {
            SearchProjectDTO parameters = new(userName, userId, completedStatus, titleContains);
            var todos = await _projectLogic.GetProjectAsync(parameters);
            return Ok(todos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateProjectDTO dto)
    {
        try
        {
            await _projectLogic.UpdateProjectAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }

    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await _projectLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}