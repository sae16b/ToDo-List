namespace ToDoList.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LegionTask
    {
        public LegionTask()
        {
            Text = "banana";
            Complete = false;
            CreationDate = DateTime.UtcNow;
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Text { get; set; }

        public bool Complete { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
