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

        [Route("error-page")]
        public IActionResult Failure(string err)
        {
            return View("Failure", err);
        }

        public IActionResult Index()
        {
            List<LegionTask> tasks = db.LegionTask.ToList();
            return View(tasks);
        }

        [Route("edit-task-page/{legionTaskId}")]
        public IActionResult EditTask(Guid legionTaskId)
        {
            var legionTask = db.LegionTask.SingleOrDefault(t => t.Id == legionTaskId);

            if (legionTask == null)
            {
                return Failure($"Could not find task with Id {legionTaskId}!");
            }

            return View(legionTask);
        }

        [Route("create-task-page")]
        public IActionResult CreateTask()
        {
            return View();
        }

        [Route("create-task")]
        [HttpPost]
        public void TaskCreated(string text)
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
            LegionTask original = db.LegionTask.SingleOrDefault(t => t.Id == editedLegionTask.Id);
            if (original == null)
            {
                return;
            }

            original.Text = editedLegionTask.Text;
            original.Complete = editedLegionTask.Complete;

            db.LegionTask.Update(original);
            db.SaveChanges();
        }

        [Route("delete-task/{legionTaskId}")]
        [HttpPost]
        public void TaskEdited(Guid legionTaskId)
        {
            LegionTask legionTask = db.LegionTask.SingleOrDefault(t => t.Id == legionTaskId);
            if (legionTask != null)
            {
                db.LegionTask.Remove(legionTask);
                db.SaveChanges();
            }
        }
    }
}