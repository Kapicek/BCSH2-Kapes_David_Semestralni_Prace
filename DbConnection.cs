using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TaskManager.Entities;

namespace TaskManager
{
    class DbConnection
    {
        private string connectionString;

        public DbConnection()
        {
            connectionString = "Data Source=MyTasks.db;Version=3;";
        }

        public void CreateTables()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand createTablesCommand = new SQLiteCommand(connection))
                {
                    createTablesCommand.CommandText = "CREATE TABLE IF NOT EXISTS Tasks (Id INTEGER PRIMARY KEY, Name TEXT, Description TEXT, Priority TEXT, Status TEXT, ProjectId INTEGER, WorkerId INTEGER)";
                    createTablesCommand.ExecuteNonQuery();

                    createTablesCommand.CommandText = "CREATE TABLE IF NOT EXISTS Teams (Id INTEGER PRIMARY KEY, Name TEXT)";
                    createTablesCommand.ExecuteNonQuery();

                    createTablesCommand.CommandText = "CREATE TABLE IF NOT EXISTS Projects (Id INTEGER PRIMARY KEY, Name TEXT, TeamId INTEGER)";
                    createTablesCommand.ExecuteNonQuery();

                    createTablesCommand.CommandText = "CREATE TABLE IF NOT EXISTS Workers (Id INTEGER PRIMARY KEY, FirstName TEXT, LastName TEXT, TeamId INTEGER)";
                    createTablesCommand.ExecuteNonQuery();

                    createTablesCommand.CommandText = "CREATE TABLE IF NOT EXISTS ProjectTasks (ProjectId INTEGER, TaskId INTEGER)";
                    createTablesCommand.ExecuteNonQuery();

                    createTablesCommand.CommandText = "CREATE TABLE IF NOT EXISTS TeamWorkers (TeamId INTEGER, WorkerId INTEGER)";
                    createTablesCommand.ExecuteNonQuery();

                    createTablesCommand.CommandText = "CREATE TABLE IF NOT EXISTS TeamProjects (TeamId INTEGER, ProjectId INTEGER)";
                    createTablesCommand.ExecuteNonQuery();
                }
            }
        }

        public int InsertTeam(Team team)
        {
            int insertedId = -1; // Nastavíme výchozí hodnotu, pokud vložení selže.

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Vložíme tým do tabulky Teams
                string insertTeamQuery = "INSERT INTO Teams (Name) VALUES (@Name); SELECT last_insert_rowid()";
                using (SQLiteCommand teamCommand = new SQLiteCommand(insertTeamQuery, connection))
                {
                    teamCommand.Parameters.AddWithValue("@Name", team.Name);

                    insertedId = Convert.ToInt32(teamCommand.ExecuteScalar()); // Získáme ID nově vloženého týmu.
                }
            }

            return insertedId; // Vracíme ID nově vloženého týmu.
        }

        public int InsertProject(Project project)
        {
            int insertedId = -1;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Vložíme projekt do tabulky Projects
                string insertProjectQuery = "INSERT INTO Projects (Name, TeamId) VALUES (@Name, @TeamId); SELECT last_insert_rowid()";
                using (SQLiteCommand projectCommand = new SQLiteCommand(insertProjectQuery, connection))
                {
                    projectCommand.Parameters.AddWithValue("@Name", project.Name);
                    projectCommand.Parameters.AddWithValue("@TeamId", project.TeamId);

                    insertedId = Convert.ToInt32(projectCommand.ExecuteScalar()); // Získáme ID nově vloženého projektu.
                }
            }

            return insertedId;
        }


        public int InsertWorker(Worker worker)
        {
            int insertedId = -1; // Nastavíme výchozí hodnotu, pokud vložení selže.

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Workers (FirstName, LastName, TeamId) VALUES (@FirstName, @LastName, @TeamId); SELECT last_insert_rowid()";
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", worker.FirstName);
                    command.Parameters.AddWithValue("@LastName", worker.LastName);
                    command.Parameters.AddWithValue("@TeamId", worker.TeamId);

                    insertedId = Convert.ToInt32(command.ExecuteScalar()); // Získáme ID nově vloženého pracovníka.
                }
            }

            return insertedId; // Vracíme ID nově vloženého pracovníka.
        }


        public int InsertTaskItem(TaskItem taskItem)
        {
            int insertedId = -1; // Nastavíme výchozí hodnotu, pokud vložení selže.

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Tasks (Name, Description, Priority, Status, ProjectId, WorkerId) VALUES (@Name, @Description, @Priority, @Status, @ProjectId, @WorkerId); SELECT last_insert_rowid()";
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", taskItem.Name);
                    command.Parameters.AddWithValue("@Description", taskItem.Description);
                    command.Parameters.AddWithValue("@Priority", taskItem.Priority);
                    command.Parameters.AddWithValue("@Status", taskItem.Status);
                    command.Parameters.AddWithValue("@ProjectId", taskItem.ProjectId);
                    command.Parameters.AddWithValue("@WorkerId", taskItem.WorkerId);

                    insertedId = Convert.ToInt32(command.ExecuteScalar()); // Získáme ID nově vloženého úkolu.
                }
            }

            return insertedId; // Vracíme ID nově vloženého úkolu.
        }

        public void AddWorkerToTeam(int workerId, int teamId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence pracovníka a týmu
                if (!IsWorkerExists(workerId, connection) || !IsTeamExists(teamId, connection))
                {
                    Console.WriteLine("Přidání pracovníka se nepovedlo");
                    return;
                }

                // Získání aktuálního TeamId pracovníka
                string selectWorkerTeamQuery = "SELECT TeamId FROM Workers WHERE Id = @WorkerId";
                int currentTeamId = 0;

                using (SQLiteCommand selectWorkerTeamCommand = new SQLiteCommand(selectWorkerTeamQuery, connection))
                {
                    selectWorkerTeamCommand.Parameters.AddWithValue("@WorkerId", workerId);
                    var result = selectWorkerTeamCommand.ExecuteScalar();
                    if (result != null)
                    {
                        currentTeamId = Convert.ToInt32(result);
                    }
                }

                // Kontrola, zda pracovník již pracuje v nějakém týmu
                if (currentTeamId != 0)
                {
                    Console.WriteLine("Pracovník už pracuje v jiném týmu.");
                    return;
                }

                // Aktualizace TeamId pracovníka
                string updateWorkerQuery = "UPDATE Workers SET TeamId = @TeamId WHERE Id = @WorkerId";
                using (SQLiteCommand updateWorkerCommand = new SQLiteCommand(updateWorkerQuery, connection))
                {
                    updateWorkerCommand.Parameters.AddWithValue("@TeamId", teamId);
                    updateWorkerCommand.Parameters.AddWithValue("@WorkerId", workerId);
                    updateWorkerCommand.ExecuteNonQuery();
                }

                // Přidání pracovníka do tabulky TeamWorkers
                string insertTeamWorkerQuery = "INSERT INTO TeamWorkers (TeamId, WorkerId) VALUES (@TeamId, @WorkerId)";
                using (SQLiteCommand teamWorkerCommand = new SQLiteCommand(insertTeamWorkerQuery, connection))
                {
                    teamWorkerCommand.Parameters.AddWithValue("@TeamId", teamId);
                    teamWorkerCommand.Parameters.AddWithValue("@WorkerId", workerId);
                    teamWorkerCommand.ExecuteNonQuery();
                }
            }
        }


        public void RemoveWorkerFromTeam(int workerId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                if (!IsWorkerExists(workerId, connection))
                {
                    Console.WriteLine("Pracovník neexistuje");
                    return;
                }

                // Získání aktuálního TeamId pracovníka
                string selectWorkerTeamQuery = "SELECT TeamId FROM Workers WHERE Id = @WorkerId";
                int currentTeamId = 0;

                using (SQLiteCommand selectWorkerTeamCommand = new SQLiteCommand(selectWorkerTeamQuery, connection))
                {
                    selectWorkerTeamCommand.Parameters.AddWithValue("@WorkerId", workerId);
                    var result = selectWorkerTeamCommand.ExecuteScalar();
                    if (result != null)
                    {
                        currentTeamId = Convert.ToInt32(result);
                    }
                }

                if (currentTeamId != 0)
                {
                    // Odstranění pracovníka z tabulky TeamWorkers
                    string removeTeamWorkerQuery = "DELETE FROM TeamWorkers WHERE TeamId = @TeamId AND WorkerId = @WorkerId";
                    using (SQLiteCommand removeTeamWorkerCommand = new SQLiteCommand(removeTeamWorkerQuery, connection))
                    {
                        removeTeamWorkerCommand.Parameters.AddWithValue("@TeamId", currentTeamId);
                        removeTeamWorkerCommand.Parameters.AddWithValue("@WorkerId", workerId);
                        removeTeamWorkerCommand.ExecuteNonQuery();
                    }
                }

                // Aktualizace TeamId pracovníka na 0 (nezařazený)
                string updateWorkerQuery = "UPDATE Workers SET TeamId = 0 WHERE Id = @WorkerId";
                using (SQLiteCommand updateWorkerCommand = new SQLiteCommand(updateWorkerQuery, connection))
                {
                    updateWorkerCommand.Parameters.AddWithValue("@WorkerId", workerId);
                    updateWorkerCommand.ExecuteNonQuery();
                }
            }
        }

        public void AddProjectToTeam(int projectId, int teamId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence projektu a týmu
                if (!IsProjectExists(projectId, connection) || !IsTeamExists(teamId, connection))
                {
                    Console.WriteLine("Přidání projektu k týmu se nepovedlo");
                    return;
                }

                // Kontrola, zda projekt již není přiřazen k týmu
                if (IsProjectAssignedToTeam(projectId, connection))
                {
                    Console.WriteLine("Projekt je již přiřazen k týmu.");
                    return;
                }

                // Aktualizace TeamId projektu
                string updateProjectQuery = "UPDATE Projects SET TeamId = @TeamId WHERE Id = @ProjectId";
                using (SQLiteCommand updateProjectCommand = new SQLiteCommand(updateProjectQuery, connection))
                {
                    updateProjectCommand.Parameters.AddWithValue("@TeamId", teamId);
                    updateProjectCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    updateProjectCommand.ExecuteNonQuery();
                }

                // Přidání záznamu do tabulky TeamProjects
                string insertTeamProjectQuery = "INSERT INTO TeamProjects (TeamId, ProjectId) VALUES (@TeamId, @ProjectId)";
                using (SQLiteCommand teamProjectCommand = new SQLiteCommand(insertTeamProjectQuery, connection))
                {
                    teamProjectCommand.Parameters.AddWithValue("@TeamId", teamId);
                    teamProjectCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    teamProjectCommand.ExecuteNonQuery();
                }
            }
        }

        public void RemoveProjectFromTeam(int projectId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence projektu
                if (!IsProjectExists(projectId, connection))
                {
                    Console.WriteLine("Projekt neexistuje.");
                    return;
                }

                // Získání aktuálního TeamId projektu
                string selectProjectTeamQuery = "SELECT TeamId FROM Projects WHERE Id = @ProjectId";
                int currentTeamId = 0;

                using (SQLiteCommand selectProjectTeamCommand = new SQLiteCommand(selectProjectTeamQuery, connection))
                {
                    selectProjectTeamCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    var result = selectProjectTeamCommand.ExecuteScalar();
                    if (result != null)
                    {
                        currentTeamId = Convert.ToInt32(result);
                    }
                }

                // Kontrola, zda projekt je přiřazen k nějakému týmu
                if (currentTeamId == 0)
                {
                    Console.WriteLine("Projekt není přiřazen k žádnému týmu.");
                    return;
                }

                // Aktualizace TeamId projektu na 0 (nepřiřazený)
                string updateProjectQuery = "UPDATE Projects SET TeamId = 0 WHERE Id = @ProjectId";
                using (SQLiteCommand updateProjectCommand = new SQLiteCommand(updateProjectQuery, connection))
                {
                    updateProjectCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    updateProjectCommand.ExecuteNonQuery();
                }

                // Odstranění záznamu z tabulky TeamProjects
                string removeTeamProjectQuery = "DELETE FROM TeamProjects WHERE TeamId = @TeamId AND ProjectId = @ProjectId";
                using (SQLiteCommand removeTeamProjectCommand = new SQLiteCommand(removeTeamProjectQuery, connection))
                {
                    removeTeamProjectCommand.Parameters.AddWithValue("@TeamId", currentTeamId);
                    removeTeamProjectCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    removeTeamProjectCommand.ExecuteNonQuery();
                }
            }
        }

        public void AddTaskToProject(int taskId, int projectId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence úkolu a projektu
                if (!IsTaskExists(taskId, connection) || !IsProjectExists(projectId, connection) || IsTaskAssignedToProject(taskId, connection))
                {
                    Console.WriteLine("Přiřazení úkolu k projektu se nepovedlo.");
                    return;
                }

                // Načtení úkolu z databáze
                string selectTaskQuery = "SELECT * FROM Tasks WHERE Id = @TaskId";
                TaskItem taskToUpdate;

                using (SQLiteCommand selectTaskCommand = new SQLiteCommand(selectTaskQuery, connection))
                {
                    selectTaskCommand.Parameters.AddWithValue("@TaskId", taskId);
                    using (SQLiteDataReader reader = selectTaskCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Vytvoření instance TaskItem z načtených dat
                            taskToUpdate = new TaskItem
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Priority = reader.GetString(reader.GetOrdinal("Priority")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId"))
                            };
                        }
                        else
                        {
                            Console.WriteLine("Chyba při čtení úkolu.");
                            return;
                        }
                    }
                }

                // Aktualizace ProjectId úkolu
                taskToUpdate.ProjectId = projectId;

                // Aktualizace úkolu v databázi
                string updateTaskQuery = "UPDATE Tasks SET ProjectId = @ProjectId WHERE Id = @TaskId";
                using (SQLiteCommand updateTaskCommand = new SQLiteCommand(updateTaskQuery, connection))
                {
                    updateTaskCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    updateTaskCommand.Parameters.AddWithValue("@TaskId", taskId);
                    updateTaskCommand.ExecuteNonQuery();
                }

                // Přidání záznamu do tabulky ProjectTasks
                string insertProjectTaskQuery = "INSERT INTO ProjectTasks (ProjectId, TaskId) VALUES (@ProjectId, @TaskId)";
                using (SQLiteCommand projectTaskCommand = new SQLiteCommand(insertProjectTaskQuery, connection))
                {
                    projectTaskCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    projectTaskCommand.Parameters.AddWithValue("@TaskId", taskId);
                    projectTaskCommand.ExecuteNonQuery();
                }
            }
        }

        public void RemoveTaskFromProject(int taskId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence úkolu
                if (!IsTaskExists(taskId, connection))
                {
                    Console.WriteLine("Úkol neexistuje.");
                    return;
                }

                // Získání ProjectId úkolu
                string selectTaskProjectQuery = "SELECT ProjectId FROM Tasks WHERE Id = @TaskId";
                int projectId = 0;

                using (SQLiteCommand selectTaskProjectCommand = new SQLiteCommand(selectTaskProjectQuery, connection))
                {
                    selectTaskProjectCommand.Parameters.AddWithValue("@TaskId", taskId);
                    var result = selectTaskProjectCommand.ExecuteScalar();
                    if (result != null)
                    {
                        projectId = Convert.ToInt32(result);
                    }
                }

                // Kontrola, zda je úkol přiřazen k nějakému projektu
                if (projectId == 0)
                {
                    Console.WriteLine("Úkol není přiřazen k žádnému projektu.");
                    return;
                }

                // Odstranění záznamu z tabulky ProjectTasks
                string removeProjectTaskQuery = "DELETE FROM ProjectTasks WHERE ProjectId = @ProjectId AND TaskId = @TaskId";
                using (SQLiteCommand removeProjectTaskCommand = new SQLiteCommand(removeProjectTaskQuery, connection))
                {
                    removeProjectTaskCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    removeProjectTaskCommand.Parameters.AddWithValue("@TaskId", taskId);
                    removeProjectTaskCommand.ExecuteNonQuery();
                }

                // Aktualizace ProjectId úkolu na 0 (nezařazený k projektu)
                string updateTaskProjectIdQuery = "UPDATE Tasks SET ProjectId = 0 WHERE Id = @TaskId";
                using (SQLiteCommand updateTaskProjectIdCommand = new SQLiteCommand(updateTaskProjectIdQuery, connection))
                {
                    updateTaskProjectIdCommand.Parameters.AddWithValue("@TaskId", taskId);
                    updateTaskProjectIdCommand.ExecuteNonQuery();
                }
            }
        }

        public void AddTaskToWorker(int taskId, int workerId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence úkolu a pracovníka
                if (!IsTaskExists(taskId, connection) || !IsWorkerExists(workerId, connection) || !IsWorkerInTeam(workerId, taskId, connection))
                {
                    Console.WriteLine("Přiřazení úkolu pracovníkovi se nepovedlo.");
                    return;
                }

                // Aktualizace WorkerId úkolu
                string updateTaskWorkerQuery = "UPDATE Tasks SET WorkerId = @WorkerId WHERE Id = @TaskId";
                using (SQLiteCommand updateTaskWorkerCommand = new SQLiteCommand(updateTaskWorkerQuery, connection))
                {
                    updateTaskWorkerCommand.Parameters.AddWithValue("@WorkerId", workerId);
                    updateTaskWorkerCommand.Parameters.AddWithValue("@TaskId", taskId);
                    updateTaskWorkerCommand.ExecuteNonQuery();
                }
            }
        }

        public void RemoveWorkerFromTask(int taskId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence úkolu
                if (!IsTaskExists(taskId, connection))
                {
                    Console.WriteLine("Odstranění pracovníka z úkolu se nepovedlo.");
                    return;
                }

                // Odstranění WorkerId z úkolu
                string removeWorkerFromTaskQuery = "UPDATE Tasks SET WorkerId = 0 WHERE Id = @TaskId";
                using (SQLiteCommand removeWorkerFromTaskCommand = new SQLiteCommand(removeWorkerFromTaskQuery, connection))
                {
                    removeWorkerFromTaskCommand.Parameters.AddWithValue("@TaskId", taskId);
                    removeWorkerFromTaskCommand.ExecuteNonQuery();
                }
            }
        }


        private bool IsWorkerInTeam(int workerId, int taskId, SQLiteConnection connection)
        {
            // Získání TeamId pracovníka
            string selectWorkerTeamQuery = "SELECT TeamId FROM Workers WHERE Id = @WorkerId";
            int workerTeamId = 0;

            using (SQLiteCommand selectWorkerTeamCommand = new SQLiteCommand(selectWorkerTeamQuery, connection))
            {
                selectWorkerTeamCommand.Parameters.AddWithValue("@WorkerId", workerId);
                var result = selectWorkerTeamCommand.ExecuteScalar();
                if (result != null)
                {
                    workerTeamId = Convert.ToInt32(result);
                }
            }

            // Získání TeamId projektu, na kterém je úkol
            string selectProjectTeamQuery = "SELECT TeamId FROM TeamProjects WHERE ProjectId = (SELECT ProjectId FROM Tasks WHERE Id = @TaskId)";
            int projectTeamId = 0;

            using (SQLiteCommand selectProjectTeamCommand = new SQLiteCommand(selectProjectTeamQuery, connection))
            {
                selectProjectTeamCommand.Parameters.AddWithValue("@TaskId", taskId);
                var result = selectProjectTeamCommand.ExecuteScalar();
                if (result != null)
                {
                    projectTeamId = Convert.ToInt32(result);
                }
            }

            return workerTeamId == projectTeamId;
        }




        private bool IsProjectAssignedToTeam(int projectId, SQLiteConnection connection)
        {
            string selectTeamQuery = "SELECT TeamId FROM Projects WHERE Id = @ProjectId";
            using (SQLiteCommand selectTeamCommand = new SQLiteCommand(selectTeamQuery, connection))
            {
                selectTeamCommand.Parameters.AddWithValue("@ProjectId", projectId);
                var result = selectTeamCommand.ExecuteScalar();
                int teamId = result != null ? Convert.ToInt32(result) : 0;
                return teamId != 0;
            }
        }

        private bool IsWorkerExists(int workerId, SQLiteConnection connection)
        {
            // Zde provedete SELECT dotaz, abyste zjistili, zda pracovník s daným ID existuje.
            string selectWorkerQuery = "SELECT COUNT(*) FROM Workers WHERE Id = @WorkerId";
            using (SQLiteCommand selectWorkerCommand = new SQLiteCommand(selectWorkerQuery, connection))
            {
                selectWorkerCommand.Parameters.AddWithValue("@WorkerId", workerId);
                int workerCount = Convert.ToInt32(selectWorkerCommand.ExecuteScalar());

                return workerCount > 0;
            }
        }

        private bool IsProjectExists(int projectId, SQLiteConnection connection)
        {
            // Zde provedete SELECT dotaz, abyste zjistili, zda projekt s daným ID existuje.
            string selectProjectQuery = "SELECT COUNT(*) FROM Projects WHERE Id = @ProjectId";
            using (SQLiteCommand selectProjectCommand = new SQLiteCommand(selectProjectQuery, connection))
            {
                selectProjectCommand.Parameters.AddWithValue("@ProjectId", projectId);
                int projectCount = Convert.ToInt32(selectProjectCommand.ExecuteScalar());

                return projectCount > 0;
            }
        }

        private bool IsTeamExists(int teamId, SQLiteConnection connection)
        {
            // Zde provedete SELECT dotaz, abyste zjistili, zda tým s daným ID existuje.
            string selectTeamQuery = "SELECT COUNT(*) FROM Teams WHERE Id = @TeamId";
            using (SQLiteCommand selectTeamCommand = new SQLiteCommand(selectTeamQuery, connection))
            {
                selectTeamCommand.Parameters.AddWithValue("@TeamId", teamId);
                int teamCount = Convert.ToInt32(selectTeamCommand.ExecuteScalar());

                return teamCount > 0;
            }
        }
        private bool IsTaskExists(int taskId, SQLiteConnection connection)
        {
            // Zde provedeme SELECT dotaz, abychom zjistili, zda úkol s daným ID existuje.
            string selectTaskQuery = "SELECT COUNT(*) FROM Tasks WHERE Id = @TaskId";
            using (SQLiteCommand selectTaskCommand = new SQLiteCommand(selectTaskQuery, connection))
            {
                selectTaskCommand.Parameters.AddWithValue("@TaskId", taskId);
                int taskCount = Convert.ToInt32(selectTaskCommand.ExecuteScalar());

                return taskCount > 0;
            }
        }

        private bool IsTaskAssignedToProject(int taskId, SQLiteConnection connection)
        {
            // Zde provedeme SELECT dotaz, abychom zjistili, zda úkol je již přiřazen k nějakému projektu.
            string selectProjectTaskQuery = "SELECT COUNT(*) FROM ProjectTasks WHERE TaskId = @TaskId";
            using (SQLiteCommand selectProjectTaskCommand = new SQLiteCommand(selectProjectTaskQuery, connection))
            {
                selectProjectTaskCommand.Parameters.AddWithValue("@TaskId", taskId);
                int projectTaskCount = Convert.ToInt32(selectProjectTaskCommand.ExecuteScalar());

                return projectTaskCount > 0;
            }
        }

        public void RemoveWorker(int workerId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence pracovníka
                if (!IsWorkerExists(workerId, connection))
                {
                    Console.WriteLine("Pracovník neexistuje.");
                    return;
                }

                // Odstranění pracovníka z tabulky Workers
                string removeWorkerQuery = "DELETE FROM Workers WHERE Id = @WorkerId";
                using (SQLiteCommand removeWorkerCommand = new SQLiteCommand(removeWorkerQuery, connection))
                {
                    removeWorkerCommand.Parameters.AddWithValue("@WorkerId", workerId);
                    removeWorkerCommand.ExecuteNonQuery();
                }

                // Odstranění pracovníka z tabulky TeamWorkers
                string removeTeamWorkerQuery = "DELETE FROM TeamWorkers WHERE WorkerId = @WorkerId";
                using (SQLiteCommand removeTeamWorkerCommand = new SQLiteCommand(removeTeamWorkerQuery, connection))
                {
                    removeTeamWorkerCommand.Parameters.AddWithValue("@WorkerId", workerId);
                    removeTeamWorkerCommand.ExecuteNonQuery();
                }
            }
        }

        public void RemoveTeam(int teamId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence týmu
                if (!IsTeamExists(teamId, connection))
                {
                    Console.WriteLine("Tým neexistuje.");
                    return;
                }

                // Získání všech pracovníků, kteří jsou členy tohoto týmu
                List<int> teamMembers = new List<int>();
                string selectTeamMembersQuery = "SELECT WorkerId FROM TeamWorkers WHERE TeamId = @TeamId";
                using (SQLiteCommand selectTeamMembersCommand = new SQLiteCommand(selectTeamMembersQuery, connection))
                {
                    selectTeamMembersCommand.Parameters.AddWithValue("@TeamId", teamId);
                    using (SQLiteDataReader reader = selectTeamMembersCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int workerId = reader.GetInt32(reader.GetOrdinal("WorkerId"));
                            teamMembers.Add(workerId);
                        }
                    }
                }

                // Odstranění všech pracovníků z týmu
                foreach (int workerId in teamMembers)
                {
                    RemoveWorkerFromTeam(workerId);
                }

                // Odstranění týmu z tabulky Teams
                string removeTeamQuery = "DELETE FROM Teams WHERE Id = @TeamId";
                using (SQLiteCommand removeTeamCommand = new SQLiteCommand(removeTeamQuery, connection))
                {
                    removeTeamCommand.Parameters.AddWithValue("@TeamId", teamId);
                    removeTeamCommand.ExecuteNonQuery();
                }
            }
        }

        public void RemoveProject(int projectId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence projektu
                if (!IsProjectExists(projectId, connection))
                {
                    Console.WriteLine("Projekt neexistuje.");
                    return;
                }

                // Odstranění všech úkolů, které jsou přiřazeny k tomuto projektu
                List<int> projectTasks = new List<int>();
                string selectProjectTasksQuery = "SELECT TaskId FROM ProjectTasks WHERE ProjectId = @ProjectId";
                using (SQLiteCommand selectProjectTasksCommand = new SQLiteCommand(selectProjectTasksQuery, connection))
                {
                    selectProjectTasksCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    using (SQLiteDataReader reader = selectProjectTasksCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int taskId = reader.GetInt32(reader.GetOrdinal("TaskId"));
                            projectTasks.Add(taskId);
                        }
                    }
                }

                // Odstranění všech úkolů z projektu
                foreach (int taskId in projectTasks)
                {
                    RemoveTaskFromProject(taskId);
                }

                // Odstranění projektu z tabulky TeamProjects
                string removeTeamProjectsQuery = "DELETE FROM TeamProjects WHERE ProjectId = @ProjectId";
                using (SQLiteCommand removeTeamProjectsCommand = new SQLiteCommand(removeTeamProjectsQuery, connection))
                {
                    removeTeamProjectsCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    removeTeamProjectsCommand.ExecuteNonQuery();
                }

                // Odstranění projektu z tabulky Projects
                string removeProjectQuery = "DELETE FROM Projects WHERE Id = @ProjectId";
                using (SQLiteCommand removeProjectCommand = new SQLiteCommand(removeProjectQuery, connection))
                {
                    removeProjectCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    removeProjectCommand.ExecuteNonQuery();
                }
            }
        }


        public void RemoveTask(int taskId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Kontrola existence úkolu
                if (!IsTaskExists(taskId, connection))
                {
                    Console.WriteLine("Úkol neexistuje.");
                    return;
                }

                // Odstranění úkolu z všech projektů, pokud je někde přiřazen
                if (IsTaskAssignedToProject(taskId, connection))
                {
                    RemoveTaskFromProject(taskId);
                }

                // Odstranění úkolu z týmu, pokud je nějakému týmu přiřazen
                RemoveWorkerFromTask(taskId);

                // Odstranění úkolu z tabulky Tasks
                string removeTaskQuery = "DELETE FROM Tasks WHERE Id = @TaskId";
                using (SQLiteCommand removeTaskCommand = new SQLiteCommand(removeTaskQuery, connection))
                {
                    removeTaskCommand.Parameters.AddWithValue("@TaskId", taskId);
                    removeTaskCommand.ExecuteNonQuery();
                }
            }
        }

        public List<Worker> GetWorkers()
        {
            List<Worker> workers = new List<Worker>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectWorkersQuery = "SELECT * FROM Workers";

                using (SQLiteCommand selectWorkersCommand = new SQLiteCommand(selectWorkersQuery, connection))
                using (SQLiteDataReader reader = selectWorkersCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Worker worker = new Worker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            TeamId = reader.GetInt32(reader.GetOrdinal("TeamId"))
                        };

                        workers.Add(worker);
                    }
                }
            }

            return workers;
        }

        public List<Team> GetTeams()
        {
            List<Team> teams = new List<Team>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectTeamsQuery = "SELECT * FROM Teams";

                using (SQLiteCommand selectTeamsCommand = new SQLiteCommand(selectTeamsQuery, connection))
                using (SQLiteDataReader reader = selectTeamsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Team team = new Team
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        teams.Add(team);
                    }
                }
            }

            return teams;
        }

        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectProjectsQuery = "SELECT * FROM Projects";

                using (SQLiteCommand selectProjectsCommand = new SQLiteCommand(selectProjectsQuery, connection))
                using (SQLiteDataReader reader = selectProjectsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Project project = new Project
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            TeamId = reader.GetInt32(reader.GetOrdinal("TeamId"))
                        };

                        projects.Add(project);
                    }
                }
            }

            return projects;
        }

        public List<TaskItem> GetTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectTasksQuery = "SELECT * FROM Tasks";

                using (SQLiteCommand selectTasksCommand = new SQLiteCommand(selectTasksQuery, connection))
                using (SQLiteDataReader reader = selectTasksCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TaskItem task = new TaskItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Priority = reader.GetString(reader.GetOrdinal("Priority")),
                            Status = reader.GetString(reader.GetOrdinal("Status")),
                            ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId")),
                            WorkerId = reader.GetInt32(reader.GetOrdinal("WorkerId"))
                        };

                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }

        public void UpdateWorker(Worker worker)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Workers SET FirstName = @FirstName, LastName = @LastName, TeamId = @TeamId WHERE Id = @Id";
                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", worker.FirstName);
                    command.Parameters.AddWithValue("@LastName", worker.LastName);
                    command.Parameters.AddWithValue("@TeamId", worker.TeamId);
                    command.Parameters.AddWithValue("@Id", worker.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateTeam(Team team)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Aktualizujeme tým v tabulce Teams
                string updateTeamQuery = "UPDATE Teams SET Name = @Name WHERE Id = @Id";
                using (SQLiteCommand teamCommand = new SQLiteCommand(updateTeamQuery, connection))
                {
                    teamCommand.Parameters.AddWithValue("@Id", team.Id);
                    teamCommand.Parameters.AddWithValue("@Name", team.Name);

                    teamCommand.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProject(Project project)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Aktualizujeme projekt v tabulce Projects
                string updateProjectQuery = "UPDATE Projects SET Name = @Name, TeamId = @TeamId WHERE Id = @Id";
                using (SQLiteCommand projectCommand = new SQLiteCommand(updateProjectQuery, connection))
                {
                    projectCommand.Parameters.AddWithValue("@Id", project.Id);
                    projectCommand.Parameters.AddWithValue("@Name", project.Name);
                    projectCommand.Parameters.AddWithValue("@TeamId", project.TeamId);

                    projectCommand.ExecuteNonQuery(); // Provedeme aktualizaci v databázi.
                }
            }
        }

        public void UpdateTaskItem(TaskItem taskItem)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Aktualizujeme úkol v tabulce Tasks
                string updateTaskItemQuery = "UPDATE Tasks SET Name = @Name, Description = @Description, Priority = @Priority, Status = @Status, ProjectId = @ProjectId, WorkerId = @WorkerId WHERE Id = @Id";
                using (SQLiteCommand taskItemCommand = new SQLiteCommand(updateTaskItemQuery, connection))
                {
                    taskItemCommand.Parameters.AddWithValue("@Id", taskItem.Id);
                    taskItemCommand.Parameters.AddWithValue("@Name", taskItem.Name);
                    taskItemCommand.Parameters.AddWithValue("@Description", taskItem.Description);
                    taskItemCommand.Parameters.AddWithValue("@Priority", taskItem.Priority);
                    taskItemCommand.Parameters.AddWithValue("@Status", taskItem.Status);
                    taskItemCommand.Parameters.AddWithValue("@ProjectId", taskItem.ProjectId);
                    taskItemCommand.Parameters.AddWithValue("@WorkerId", taskItem.WorkerId);

                    taskItemCommand.ExecuteNonQuery(); // Provedeme aktualizaci v databázi.
                }
            }
        }

    }
}
