namespace TaskManager.Entities
{
    class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int TeamId { get; set; }

        public Project(string name, int teamId)
        {
            Name = name;
            TeamId = teamId;
        }

        public Project(string name)
        {
            Name = name;
            TeamId = 0;
        }

        public Project()
        {
            TeamId = 0;
        }
    }
}
