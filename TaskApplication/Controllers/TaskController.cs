using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApplication.Data;
using TaskApplication.Data.Entities;
using TaskApplication.Models;

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
                return Ok(mapper.Map<ToDoTaskModel[]>(tasks));
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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

                return Ok(mapper.Map<ToDoTaskModel>(task));
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTaskByName(string search)
        {
            try
            {
                var tasks = await appRepository.SearchTaskByName(search);
                if (!tasks.Any()) return NotFound();
                return Ok(mapper.Map<ToDoTaskModel[]>(tasks));
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("datetime")]
        public async Task<IActionResult> SearchTaskByDate(DateTime datetime)
        {
            try
            {
                var tasks = await appRepository.SearchTaskByDate(datetime);
                if (!tasks.Any()) return NotFound();
                return Ok(mapper.Map<ToDoTaskModel[]>(tasks));
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoTask model)
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
                model.Id = await appRepository.GetMaxId() + 1;

                appRepository.Add(model);
                if (await appRepository.SaveChanges())
                {
                    return Created($"/api/Task/{model.Id}", mapper.Map<ToDoTaskModel>(model));
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ToDoTaskModel model)
        {
            try
            {
                var oldTask = await appRepository.GetTaskAsync(id);
                if (oldTask == null)
                {
                    return NotFound($"Couldn't find task with id of {id}");
                }

                //Update functie werkt nog niet helemaal, rest wel
                mapper.Map(model, oldTask);
                //appRepository.UpdateTask(oldTask);
                if (await appRepository.SaveChanges())
                {
                    return Ok(oldTask);
                }

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

            return BadRequest();
        }
    }
}
