using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApplication.Data.Entities;

namespace TaskApplication.Data
{
    public class TaskAppRepository : ITaskAppRepository
    {
        //Maybe ILogger toevoegen?

        private readonly ApplicationContext db;

        public TaskAppRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public void Add(ToDoTask toDoTask)
        {
            db.Add(toDoTask);
        }

        public void Delete(ToDoTask toDoTask)
        {
            db.Remove(toDoTask);
        }

        public async Task<ToDoTask[]> GetAllTasksAsync()
        {
            IQueryable<ToDoTask> query = db.ToDoTasks;
            query = query.OrderByDescending(t => t.Name);
            return await query.ToArrayAsync();
        }

        public async Task<ToDoTask> GetTaskAsync(int id)
        {
            IQueryable<ToDoTask> query = db.ToDoTasks;
            query = query.Where(t => t.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<ToDoTask[]> SearchTaskByName(string name)
        {
            IQueryable<ToDoTask> query = db.ToDoTasks;
            query = query.OrderByDescending(t => t.Name).Where(d => d.Name.Contains(name));
            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChanges()
        {
            //Return alleen als er tenminste een rij is veranderd
            return (await db.SaveChangesAsync()) > 0;
        }

        public async Task<ToDoTask[]> SearchTaskByDate(DateTime dateTime)
        {
            IQueryable<ToDoTask> query = db.ToDoTasks;
            query = query.OrderByDescending(t => t.Name).Where(d => d.BeginDate == dateTime);
            return await query.ToArrayAsync();
        }
    }
}
