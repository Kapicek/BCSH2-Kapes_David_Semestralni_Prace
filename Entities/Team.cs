namespace TaskManager.Entities
{
    class Team
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public Team()
        {
        }

        public Team(string name)
        {
            Name = name;
        }
    }
}
