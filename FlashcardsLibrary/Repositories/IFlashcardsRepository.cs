using FlashcardsLibrary.Models;

namespace FlashcardsLibrary.Repositories;
internal interface IFlashCardsRepository
{
    Flashcard GetById(int id);
    IEnumerable<Flashcard> GetAll();
    void Add(Flashcard flashcard);
    void Update(Flashcard flashcard);
    void Delete(Flashcard flashcard);
}
