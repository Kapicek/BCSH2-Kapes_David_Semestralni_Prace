namespace TaskManager.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public Team()
        {
            Name = "";
        }

        public Team(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Team Name: {Name}";
        }
    }
}
