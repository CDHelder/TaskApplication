using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApplication.Data.Entities;

namespace TaskApplication.Data
{
    public interface ITaskAppRepository
    {
        void Add(ToDoTask toDoTask);
        void Delete(ToDoTask toDoTask);

        Task<ToDoTask[]> GetAllTasksAsync();
        Task<ToDoTask> GetTaskAsync(int id);
        Task<ToDoTask[]> SearchTaskByName(string name);
        Task<ToDoTask[]> SearchTaskByDate(DateTime dateTime);

        Task<bool> SaveChanges();
    }
}
