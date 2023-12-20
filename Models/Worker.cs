namespace TaskManager.Entities
{
    public class Worker
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int TeamId { get; set; }

        public Worker() { TeamId = 0; }

        public Worker(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            TeamId = 0;
        }

        public Worker(string firstName, string lastName, int teamId)
        {
            FirstName = firstName;
            LastName = lastName;
            TeamId = teamId;
        }
    }
}
