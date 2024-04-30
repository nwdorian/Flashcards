using FlashardsUI.Helpers;
using FlashcardsLibrary.Models;
using FlashcardsLibrary.Repositories;
using Spectre.Console;

namespace FlashardsUI;
internal class FlashcardsController
{
    private readonly IFlashCardsRepository _flashcardsRepository;
    public FlashcardsController(IFlashCardsRepository flashcardsRepository)
    {
        _flashcardsRepository = flashcardsRepository;
    }
    public Stack? CurrentStack { get; set; }

    internal void GetAll()
    {
        var flashcards = _flashcardsRepository.GetAll(CurrentStack);

        List<FlashcardDTO> flashcardDTOs = new();

        foreach (var flashcard in flashcards)
        {
            flashcardDTOs.Add(flashcard.ToFlashcardDTO());
        }

        TableVisualization.ShowFlashcardsTable(flashcardDTOs, CurrentStack.Name);

        AnsiConsole.Write("Press any key to continue... ");
        Console.ReadKey();
    }

    internal void Post()
    {
        AnsiConsole.WriteLine($"Add new flashcard to the {CurrentStack.Name} stack");

        var question = UserInput.StringPrompt("Enter flashcard question:");

        while (_flashcardsRepository.FlashcardExists(question.Trim()))
        {
            question = UserInput.StringPrompt($"Flashcard with the question {question} already exists! \n\nEnter question (or press 0 to cancel):");
        }

        if (question.Trim() == "0")
        {
            return;
        }

        var answer = UserInput.StringPrompt("Enter flashcard answer:");

        if (!AnsiConsole.Confirm($"Are you sure you want to add a new flashcard to the {CurrentStack.Name} stack?"))
        {
            return;
        }

        _flashcardsRepository.Add(new Flashcard
        {
            StackId = CurrentStack.Id,
            Question = question,
            Answer = answer
        });

        AnsiConsole.Write($"\nNew flashcard was succesfully added! Press any key to continue...");
        Console.ReadKey();
    }

    internal void Delete()
    {
        var flashcard = Get("Select a flashcard to delete:");

        if (flashcard.Id == 0)
        {
            return;
        }

        if (!AnsiConsole.Confirm($"Are you sure you want to delete {flashcard.Question}?"))
        {
            return;
        }

        _flashcardsRepository.Delete(flashcard);

        AnsiConsole.Write($"Flashcard {flashcard.Question} was succesfully deleted! Press any key to continue...");
        Console.ReadKey();
    }

    internal void Update()
    {
        var flashcard = Get("Select a flashcard to update:");

        if (flashcard.Id == 0)
        {
            return;
        }

        var question = UserInput.StringPrompt("Enter flashcard question (or press 0 to cancel):");

        while (_flashcardsRepository.FlashcardExists(question.Trim()))
        {
            question = UserInput.StringPrompt($"Flashcard with the question {question} already exists! \n\nEnter question (or press 0 to cancel):");
        }

        if (question.Trim() == "0")
        {
            return;
        }

        var answer = UserInput.StringPrompt("Enter flashcard answer (or press 0 to cancel):");

        if (answer.Trim() == "0")
        {
            return;
        }

        if (!AnsiConsole.Confirm($"Are you sure you want to apply flashcard changes?"))
        {
            return;
        }

        flashcard.Question = question;
        flashcard.Answer = answer;

        _flashcardsRepository.Update(flashcard);

        AnsiConsole.Write($"Flashcard was succesfully updated! Press any key to continue...");
        Console.ReadKey();
    }

    internal Flashcard Get(string prompt)
    {
        IEnumerable<Flashcard> flashcards = _flashcardsRepository.GetAll(CurrentStack);

        return AnsiConsole.Prompt(
            new SelectionPrompt<Flashcard>()
            .Title(prompt)
            .AddChoices(flashcards)
            .AddChoices(new Flashcard { Id = 0, StackId = 0, Question = "Cancel and return to menu", Answer = "" })
            .UseConverter(f => f.Question)
            );
    }
}
