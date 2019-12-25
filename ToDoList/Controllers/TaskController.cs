using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("")]
    public class TaskController : Controller
    {
        private readonly ToDoListContext db;

        public TaskController(ToDoListContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            List<LegionTask> tasks = db.LegionTask.ToList();
            return View(tasks);
        }

        [Route("add-task")]
        [HttpPost]
        public void AddTask(string text)
        {
            LegionTask legionTask = new LegionTask()
            {
                Id = Guid.NewGuid(),
                Text = text,
            };
            db.LegionTask.Add(legionTask);
            db.SaveChanges();
        }

        [Route("edit-task")]
        [HttpPost]
        public void TaskEdited(LegionTask editedLegionTask)
        {
            db.LegionTask.Update(editedLegionTask);
            db.SaveChanges();
        }
    }
}