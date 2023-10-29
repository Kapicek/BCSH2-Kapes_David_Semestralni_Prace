namespace TaskManager.Entities
{
    class TaskItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public int? ProjectId { get; set; }
        public int? WorkerId { get; set; }

        public TaskItem()
        {
            ProjectId = 0;
            WorkerId = 0;
        }

        public TaskItem(string name, string description, string priority, string status, int projectId)
        {
            Name = name;
            Description = description;
            Priority = priority;
            Status = status;
            ProjectId = projectId;
            WorkerId = 0;
        }

        public TaskItem(string name, string description, string priority, string status)
        {
            Name = name;
            Description = description;
            Priority = priority;
            Status = status;
            ProjectId = 0;
            WorkerId = 0;
        }
    }
}
