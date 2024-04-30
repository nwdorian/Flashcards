using Dapper;
using FlashcardsLibrary.Models;
using Microsoft.Data.SqlClient;

namespace FlashcardsLibrary.Repositories;
public class FlashcardsRepository : IFlashCardsRepository
{
    private readonly string? connectionString;
    public FlashcardsRepository()
    {
        connectionString = AppConfig.GetFullConnectionString();
    }
    public IEnumerable<Flashcard> GetAll(Stack stack)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var getAllSql = "SELECT * FROM Flashcard WHERE StackId = @StackId";

            return connection.Query<Flashcard>(getAllSql, new { StackId = stack.Id});
        }
    }

    public void Add(Flashcard flashcard)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var insertSql = "INSERT INTO Flashcard (StackId, Question, Answer) VALUES (@StackId, @Question, @Answer)";

            connection.Execute(insertSql, flashcard);
        }
    }

    public void Delete(Flashcard flashcard)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var deleteSql = "DELETE FROM Flashcard WHERE Id = @Id";

            connection.Execute(deleteSql, flashcard);
        }
    }

    public void Update(Flashcard flashcard)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var updateSql = "UPDATE Flashcard SET StackId = @StackId, Question = @Question, Answer = @Answer WHERE Id = @Id";

            connection.Execute(updateSql, flashcard);
        }
    }

    public bool FlashcardExists(string question)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var checkNameSql = "SELECT TOP 1 COUNT(*) FROM Flashcard WHERE Question = @Question";

            return connection.ExecuteScalar<bool>(checkNameSql, new { Question = question });
        }
    }
}
