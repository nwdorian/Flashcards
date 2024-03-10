using Dapper;
using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace FlashcardsLibrary;
public class DatabaseManager
{
    private string? connectionString;
    private string? connectionStringDb;

    public DatabaseManager()
    {
        connectionString = AppConfig.GetDbConnectionString();
        connectionString = AppConfig.GetFullConnectionString();
    }

    public void CreateDatabase()
    {
        try
        {
            using (var connection = new SqlConnection(connectionStringDb))
            {
                connection.Open();
                var createDatabaseSql = @"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'FlashcardsDb')
                                            CREATE DATABASE FlashcardsDb";
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
                var createStackSql = @"IF NOT EXISTS (SELECT name FROM sys.tables WHERE name = 'Stack')
                                            CREATE TABLE Stack(
                                            Id      INT PRIMARY KEY IDENTITY(1,1),
                                            Name    NVARCHAR(100) NOT NULL)";
                connection.Execute(createStackSql);

                var createFlashcardSql = @"IF NOT EXISTS (SELECT name FROM sys.tables WHERE name = 'Flashcard')
                                            CREATE TABLE Flashcard(
                                            Id          INT PRIMARY KEY IDENTITY(1,1),
                                            StackId     INT FOREIGN KEY REFERENCES Stack(Id) ON DELETE CASCADE,
                                            Question    NVARCHAR(100) NOT NULL,
                                            Answer      NVARCHAR(100) NOT NULL)";
                connection.Execute(createFlashcardSql);

                var createStudySessionSql = @"IF NOT EXISTS (SELECT name FROM sys.tables WHERE name = 'StudySession')
                                            CREATE TABLE StudySession(
                                            Id          INT PRIMARY KEY IDENTITY(1,1),
                                            StackId     INT FOREIGN KEY REFERENCES Stack(Id) ON DELETE CASCADE,
                                            Date        DATETIME NOT NULL,
                                            Score       INT NOT NULL)";
                connection.Execute(createStudySessionSql);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.Write($"Error! Details: {e.Message}\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
