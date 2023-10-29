using TaskManager;

class Program
{
    static void Main()
    {
        DbConnection dbConnection = new DbConnection();
        dbConnection.CreateTables();

        //Team team1 = new Team("Nový tým 1");
        //Team team2 = new Team("Nový tým 2");
        //dbConnection.InsertTeam(team1);
        //dbConnection.InsertTeam(team2);

        //Worker worker1 = new Worker("Dan", "Malý");
        //Worker worker2 = new Worker("Jakub", "Černoušek");

        //dbConnection.InsertWorker(worker1);
        //dbConnection.InsertWorker(worker2);

        //dbConnection.AddWorkerToTeam(1, 1);
        //dbConnection.AddWorkerToTeam(2, 1);

        //Project project = new Project("Nový projekt");
        //dbConnection.InsertProject(project);

        //dbConnection.AddProjectToTeam(1, 1);

        //TaskItem task1 = new TaskItem("Úkol 1", "Popis úkolu 1", "Vysoká", "Aktivní");
        //TaskItem task2 = new TaskItem("Úkol 2", "Popis úkolu 2", "Střední", "Nedokončený");
        //TaskItem task3 = new TaskItem("Úkol 3", "Popis úkolu 3", "Nízká", "Dokončený");
        //TaskItem task4 = new TaskItem("Úkol 4", "Popis úkolu 4", "Vysoká", "Aktivní");
        //TaskItem task5 = new TaskItem("Úkol 5", "Popis úkolu 5", "Střední", "Nedokončený");

        //dbConnection.InsertTaskItem(task1);
        //dbConnection.InsertTaskItem(task2);
        //dbConnection.InsertTaskItem(task3);
        //dbConnection.InsertTaskItem(task4);
        //dbConnection.InsertTaskItem(task5);

        //dbConnection.AddTaskToProject(1, 1);
        //dbConnection.AddTaskToProject(2, 1);
        //dbConnection.AddTaskToProject(3, 1);
        //dbConnection.AddTaskToProject(4, 1);

        //dbConnection.RemoveTaskFromProject(3);

    }
}
