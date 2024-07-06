using System;

namespace ChoucairApp.Core.Domain.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Task_Title { get; set; }
        public string Task_Description { get; set; }
        public DateTime Task_EndDate { get; set; }
        public DateTime Task_StartDate { get; set; }
        public int StatusID { get; set; }
        public string UserID { get; set; }  
    }
}
