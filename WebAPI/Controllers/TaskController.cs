using App.LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Model;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskLogic TaskLogic;

    public TaskController(ITaskLogic taskLogic)
    {
        this.TaskLogic = taskLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Tasks>> CreateAsync(CreateTaskDTO dto)
    {
        try
        {
            Tasks created = await TaskLogic.CreateTaskAsync(dto);
            return Created($"/todos/{created.id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tasks>>> GetAsync([FromQuery] string? userName, [FromQuery] int userId,[FromQuery] string? projectName,[FromQuery] int projectId,
        [FromQuery] bool completedStatus, [FromQuery] string? titleContains)
    {
        try
        {
            SearchTaskDTO parameters = new(userName,userId,projectName , projectId , completedStatus ,titleContains);
            var task = await TaskLogic.GetTasksAsync(parameters);
            return Ok(task);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateTaskDTO dto)
    {
        try
        {
            await TaskLogic.UpdateAsync(dto);
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
            await TaskLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}