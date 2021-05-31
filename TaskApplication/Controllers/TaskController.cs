using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskApplication.Data;
using TaskApplication.Data.Entities;

namespace TaskApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskAppRepository appRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public TaskController(ITaskAppRepository appRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.appRepository = appRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await appRepository.GetAllTasksAsync();
                return Ok(tasks);
                //return Ok(mapper.Map<ToDoTaskModel[]>(tasks));
            }
            //Maybe exception logica toevoegen aand return
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: \n{ex}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            try
            {
                var task = await appRepository.GetTaskAsync(id);

                if (task == null)
                    return NotFound();

                return Ok(task);
                //return Ok(mapper.Map<ToDoTaskModel>(task));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: \n{ex}");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTaskByName(string search)
        {
            try
            {
                var tasks = await appRepository.SearchTasksByName(search);
                if (!tasks.Any()) return NotFound();
                return Ok(tasks);
                //return Ok(mapper.Map<ToDoTaskModel[]>(tasks));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: \n{ex}");
            }
        }

        [HttpGet("datetime")]
        public async Task<IActionResult> SearchTaskByDate(DateTime datetime)
        {
            try
            {
                var tasks = await appRepository.SearchTaskByDate(datetime);
                if (!tasks.Any()) return NotFound();
                return Ok(tasks);
                //return Ok(mapper.Map<ToDoTaskModel[]>(tasks));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: \n{ex}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoTask task)
        {
            try
            {
                /*//~~~~~~ Hier staat de logica voor het gebruiken van de ToDoTaskModel ~~~~~~

                //  Alleen werkte deze logica niet dus ben ik direct de ToDoTask gaan gebruiken

                var location = linkGenerator.GetPathByAction("Get", "TaskController", new { id = model.Id });
                if (string.IsNullOrWhiteSpace(location)) return BadRequest($"Couldn't use id: {model.Id}");

                //Faka met die mapper
                var task = mapper.Map<ToDoTask>(model); */

                //Extra controle om te kijken of een id/naam al bestaat
                //var existing = await appRepository.GetTaskAsync(model.Id);
                //if (existing != null) return BadRequest($"Task with id: {model.Id} already exists");
                //model.Id = await appRepository.GetMaxId() + 1;


                //var existing = await appRepository.GetTaskAsync(model.Name);
                //if (existing != null) return BadRequest("That name is already in use");

                //var location = linkGenerator.GetPathByAction("GetTask", "Task", new { Name = model.Name });
                //if (string.IsNullOrWhiteSpace(location)) return BadRequest($"Couldn't use name: {model.Name}");

                //var task = mapper.Map<ToDoTask>(model);
                appRepository.Add(task);
                if (await appRepository.SaveChanges())
                {
                    return Created($"/api/Task/{task.Id}", task);
                    //return Created($"/api/Task/{model.Id}", mapper.Map<ToDoTaskModel>(model));
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: \n{ex}");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(ToDoTask task)
        {
            try
            {
                /*var oldTask = await appRepository.GetTaskAsync(id);
                if (oldTask == null) return NotFound($"Couldn't find task with ID: {id}");

                mapper.Map(model, oldTask);*/
                appRepository.UpdateTask(task);

                if (await appRepository.SaveChanges())
                {
                    return Ok(task);
                    //return Ok(mapper.Map<ToDoTaskModel>(oldTask));
                }

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: \n{ex}");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldTask = await appRepository.GetTaskAsync(id);
                if (oldTask == null) return NotFound($"Couldn't find task with ID: {id}");

                appRepository.Delete(oldTask);

                if (await appRepository.SaveChanges())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: \n{ex}");
            }

            return BadRequest();
        }
    }
}
