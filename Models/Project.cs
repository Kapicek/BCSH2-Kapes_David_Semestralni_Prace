namespace TaskManager.Entities
{
    public class Project
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

        public override string ToString()
        {
            return $"Project Name: {Name}";
        }
    }
}
