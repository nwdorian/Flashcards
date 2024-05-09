using FlashcardsLibrary.Models;

namespace FlashcardsLibrary.Repositories;
public interface IStudySessionsRepository
{
    IEnumerable<StudySession> GetAll();
    void Add(StudySession session);
}
