using Dapper;
using FlashcardsLibrary.Models;
using Microsoft.Data.SqlClient;

namespace FlashcardsLibrary.Repositories;
public class StacksRepository : IStacksRepository
{
    private readonly string? connectionString;

    public StacksRepository()
    {
        connectionString = AppConfig.GetFullConnectionString();
    }
    public IEnumerable<Stack> GetAll()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var getAllSql = "SELECT * FROM Stack";

            return connection.Query<Stack>(getAllSql);
        }
    }
    public void Add(Stack stack)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var insertSql = "INSERT INTO Stack (Name) VALUES (@Name)";

            connection.Execute(insertSql, stack);
        }
    }

    public void Delete(Stack stack)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var deleteSql = "DELETE FROM Stack WHERE Id = @Id";

            connection.Execute(deleteSql, stack);
        }
    }

    public void Update(Stack stack)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var updateSQL = "UPDATE Stack SET Name = @Name WHERE Id = @Id";

            connection.Execute(updateSQL, stack);
        }
    }

    public bool StackNameExists(string name)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var checkNameSql = "SELECT TOP 1 COUNT(*) FROM Stack WHERE Name = @Name";

            return connection.ExecuteScalar<bool>(checkNameSql, new { Name = name });
        }
    }
}
