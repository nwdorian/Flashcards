using FlashcardsLibrary.Models;

namespace FlashcardsLibrary.Repositories;
public interface IStacksRepository
{
    Stack? GetById(int id);
    IEnumerable<Stack> GetAll();
    void Add(Stack stack);
    void Update(Stack stack);
    void Delete(Stack stack);
    bool StackNameExists(string name);
}
