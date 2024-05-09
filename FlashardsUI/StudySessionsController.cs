using FlashardsUI.Helpers;
using FlashcardsLibrary.Models;
using FlashcardsLibrary.Repositories;
using Spectre.Console;

namespace FlashardsUI;
internal class StudySessionsController
{
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IStacksRepository _stacksRepository;
    private readonly IFlashCardsRepository _flashCardsRepository;
    public StudySessionsController(IStudySessionsRepository studySessionsRepository, IStacksRepository stacksRepository, IFlashCardsRepository flashcardsRepository)
    {
        _studySessionsRepository = studySessionsRepository;
        _stacksRepository = stacksRepository;
        _flashCardsRepository = flashcardsRepository;
    }
    internal void GetAll()
    {
        var studySessions = _studySessionsRepository.GetAll();

        List<StudySessionDTO> studySessionDTOs = new();

        foreach (var s in studySessions)
        {
            studySessionDTOs.Add(s.ToStudySessionDTO());
        }

        TableVisualization.ShowStudySessionsTable(studySessionDTOs);

        AnsiConsole.Write("Press any key to continue...");
        Console.ReadKey();
    }

    internal void Post()
    {
        var stack = GetStack("Select which stack you want to study: ");

        if (stack.Id == 0)
        {
            return;
        }

        var flashcards = _flashCardsRepository.GetAll(stack);

        if(!flashcards.Any())
        {
            AnsiConsole.Markup($"The stack [red]{stack.Name}[/] doesn't contain any flashcards yet. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        int score = 0;
        int count = 1;

        foreach (var flashcard in flashcards)
        {
            Console.Clear();
            AnsiConsole.WriteLine($"Answer questions from {stack.Name} stack");
            AnsiConsole.WriteLine($"Question ({count}/{flashcards.Count()}): {flashcard.Question}");

            string answer = UserInput.StringPrompt("Enter answer: ");

            if (answer.ToLower().Trim() == flashcard.Answer.ToLower().Trim())
            {
                score++;
                AnsiConsole.Write("Your answer was correct! Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                AnsiConsole.MarkupLine($"Your answer [red]{answer}[/] was incorrect!");
                AnsiConsole.Markup($"Correct answer is [green]{flashcard.Answer}[/]. Press any key to continue...");
                Console.ReadKey();
            }
            count++;
        }
        var date = DateTime.Now;

        _studySessionsRepository.Add(new StudySession
        {
            StackId = stack.Id,
            Date = date,
            Score = score
        });

        Console.Clear();
        AnsiConsole.WriteLine($"You have finished the study session with a total score of {score}/{flashcards.Count()}!");
        AnsiConsole.Write("Press any key to continue...");
        Console.ReadLine();
    }

    internal Stack GetStack(string prompt)
    {
        IEnumerable<Stack> stacks = _stacksRepository.GetAll();

        return AnsiConsole.Prompt(
            new SelectionPrompt<Stack>()
            .Title(prompt)
            .AddChoices(stacks)
            .AddChoices(new Stack { Id = 0, Name = "Cancel and return to menu" })
            .UseConverter(stack => stack.Name!)
            );
    }
}
