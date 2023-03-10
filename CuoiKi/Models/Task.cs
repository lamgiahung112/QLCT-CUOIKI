using CuoiKi.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuoiKi.Models
{
    public class Task
    {
        public String Id { get; set; }
        public String Assignee { get; set; }
        public String Assigner { get; set; }
        public String Description { get; set; }
        public String Title { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Constructor method used to map data from SQL to Work
        /// </summary>
        public Task(String ID, String Assignee, String Assigner, 
            String Description, String Title, DateTime StartingTime, 
            DateTime EndingTime, TaskStatus Status, DateTime CreatedAt, DateTime UpdatedAt) { 
            this.Id = ID;
            this.Assignee = Assignee;
            this.Assigner = Assigner;
            this.Description = Description;
            this.Title = Title;
            this.StartingTime = StartingTime;
            this.EndingTime = EndingTime;
            this.Status = Status;
            this.CreatedAt = CreatedAt;
            this.UpdatedAt = UpdatedAt;
        }
        /// <summary>
        /// Factory method to easily create new instance of work
        /// </summary>
        /// <param name="Assignee">Id of the manager</param>
        /// <param name="Assigner">Id of the staff</param>
        /// <param name="Description">Description of work</param>
        /// <param name="Title">Title of work</param>
        /// <param name="StartingTime">Starting time of work</param>
        /// <param name="EndingTime">Ending time of Work</param>
        /// <returns>A new instance of Work</returns>
        public static Task CreateNewTask(String Assignee, String Assigner, String Description, String Title, DateTime StartingTime, DateTime EndingTime)
        {
            String workId = Assignee + new Random().NextInt64().ToString();
            Task work = new(workId, Assignee, Assigner, Description, Title, StartingTime, EndingTime, TaskStatus.WIP, DateTime.Now, DateTime.Now);
            return work;
        }
    }
}
