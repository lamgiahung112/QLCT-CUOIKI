using CuoiKi.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuoiKi.Models
{
    public class Task : ModelBase
    {
        public string Assignee { get; set; }
        public string Assigner { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Constructor method used to map data from SQL to Work
        /// </summary>
        public Task(string ID, string Assignee, string Assigner, 
            string Description, string Title, DateTime StartingTime, 
            DateTime EndingTime, string Status, DateTime CreatedAt, DateTime UpdatedAt) : base(ID) { 
            this.Assignee = Assignee;
            this.Assigner = Assigner;
            this.Description = Description;
            this.Title = Title;
            this.StartingTime = StartingTime;
            this.EndingTime = EndingTime;
            this.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), Status);
            this.CreatedAt = CreatedAt;
            this.UpdatedAt = UpdatedAt;
        }
        /// <summary>
        /// Factory method to easily create new instance of work
        /// </summary>
        /// <param name="Assignee">Id of the staff</param>
        /// <param name="Assigner">Id of the manager</param>
        /// <param name="Description">Description of work</param>
        /// <param name="Title">Title of work</param>
        /// <param name="StartingTime">Starting time of work</param>
        /// <param name="EndingTime">Ending time of Work</param>
        /// <returns>A new instance of Work</returns>
        public static Task CreateNewTask(string Assignee, string Assigner, string Description, string Title, DateTime StartingTime, DateTime EndingTime)
        {
            string workId = Assignee + new Random().NextInt64().ToString();
            Task work = new(workId, Assignee, Assigner, Description, Title, StartingTime, EndingTime, nameof(TaskStatus.WIP), DateTime.Now, DateTime.Now);
            return work;
        }
    }
}
