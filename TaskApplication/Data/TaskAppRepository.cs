using Microsoft.EntityFrameworkCore;
using System;
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
            query = query.OrderByDescending(t => t.EndDate);
            return await query.ToArrayAsync();
        }

        public async Task<ToDoTask> GetTaskAsync(int id)
        {
            return await db.ToDoTasks.FirstOrDefaultAsync(r => r.Id == id);
            /*IQueryable<ToDoTask> query = db.ToDoTasks;
            query = query.Where(t => t.Name == name);
            return await query.FirstOrDefaultAsync();*/
        }

        public async Task<ToDoTask[]> SearchTasksByName(string name)
        {
            IQueryable<ToDoTask> query = db.ToDoTasks;
            query = query.OrderByDescending(t => t.Name).Where(d => d.Name.Contains(name));
            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChanges()
        {
            //Return true als er tenminste een rij is veranderd
            return (await db.SaveChangesAsync()) > 0;
        }

        public async Task<ToDoTask[]> SearchTaskByDate(DateTime dateTime)
        {
            IQueryable<ToDoTask> query = db.ToDoTasks;
            query = query.OrderByDescending(t => t.Name).Where(d => d.BeginDate == dateTime);
            return await query.ToArrayAsync();
        }

        public void UpdateTask(ToDoTask toDoTask)
        {
            db.Entry(toDoTask).State = EntityState.Modified;
        }
    }
}
