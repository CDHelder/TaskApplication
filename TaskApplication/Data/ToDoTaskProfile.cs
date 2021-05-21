using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApplication.Data.Entities;
using TaskApplication.Models;

namespace TaskApplication.Data
{
    public class ToDoTaskProfile : Profile
    {
        public ToDoTaskProfile()
        {
            this.CreateMap<ToDoTask, ToDoTaskModel>();
        }
    }
}
