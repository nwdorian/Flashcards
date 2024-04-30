using FlashcardsLibrary.Models;

namespace FlashcardsLibrary.Repositories;
public interface IFlashCardsRepository
{
    IEnumerable<Flashcard> GetAll(Stack stack);
    void Add(Flashcard flashcard);
    void Update(Flashcard flashcard);
    void Delete(Flashcard flashcard);
    bool FlashcardExists(string name);
}
