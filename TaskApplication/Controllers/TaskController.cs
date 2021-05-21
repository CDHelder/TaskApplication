using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class TaskController : ControllerBase
    {
        private readonly ITaskAppRepository appRepository;
        private readonly IMapper mapper;

        public TaskController(ITaskAppRepository appRepository, IMapper mapper)
        {
            this.appRepository = appRepository;
            this.mapper = mapper;
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
    }
}
