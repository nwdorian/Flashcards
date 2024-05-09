using Dapper;
using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace FlashcardsLibrary;
public class DatabaseManager
{
    private string? connectionStringDb;
    private string? connectionString;

    public DatabaseManager()
    {
        connectionStringDb = AppConfig.GetDbConnectionString();
        connectionString = AppConfig.GetFullConnectionString();
    }

    public void CreateDatabase()
    {
        try
        {
            using (var connection = new SqlConnection(connectionStringDb))
            {
                connection.Open();
                var createDatabaseSql = """
                                        IF NOT EXISTS(SELECT *
                                                    FROM   sys.databases
                                                    WHERE  NAME = 'FlashcardsDb')
                                        CREATE DATABASE FlashcardsDb
                                        """;
                connection.Execute(createDatabaseSql);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.Write($"Error! Details: {e.Message}\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public void CreateTables()
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var createTablesSql = """
                                     IF NOT EXISTS (SELECT NAME
                                                   FROM   sys.tables
                                                   WHERE  NAME = 'Stack')
                                      CREATE TABLE Stack
                                        (
                                           Id   INT PRIMARY KEY IDENTITY(1, 1),
                                           Name NVARCHAR(100) NOT NULL
                                        );

                                    IF NOT EXISTS (SELECT NAME
                                                   FROM   sys.tables
                                                   WHERE  NAME = 'Flashcard')
                                      CREATE TABLE Flashcard
                                        (
                                           Id       INT PRIMARY KEY IDENTITY(1, 1),
                                           Stackid  INT FOREIGN KEY REFERENCES stack(id) ON DELETE CASCADE,
                                           Question NVARCHAR(100) NOT NULL,
                                           Answer   NVARCHAR(100) NOT NULL
                                        );

                                    IF NOT EXISTS (SELECT NAME
                                                   FROM   sys.tables
                                                   WHERE  NAME = 'StudySession')
                                      CREATE TABLE Studysession
                                        (
                                           Id      INT PRIMARY KEY IDENTITY(1, 1),
                                           Stackid INT FOREIGN KEY REFERENCES stack(id) ON DELETE CASCADE,
                                           Date    DATETIME NOT NULL,
                                           Score   INT NOT NULL
                                        )
                                    """;
                connection.Execute(createTablesSql);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.Write($"Error! Details: {e.Message}\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
