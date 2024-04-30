using FlashardsUI.Helpers;
using FlashcardsLibrary.Models;
using FlashcardsLibrary.Repositories;
using Spectre.Console;

namespace FlashardsUI;
internal class StacksController
{
    private readonly IStacksRepository _stacksRepository;
    public StacksController(IStacksRepository stacksRepository)
    {
        _stacksRepository = stacksRepository;
    }
    internal void GetAll()
    {
        IEnumerable<Stack> stacks = _stacksRepository.GetAll();

        List<StackDTO> stackDTOs = new();

        foreach (var s in stacks)
        {
            stackDTOs.Add(s.ToStackDTO());
        }

        TableVisualization.ShowStacksTable(stackDTOs);

        AnsiConsole.Write("Press any key to continue... ");
        Console.ReadKey();
    }

    internal void Post()
    {
        var name = UserInput.StringPrompt("Please enter the stack name (or press 0 to cancel):");

        while (_stacksRepository.StackNameExists(name.Trim()))
        {
            name = UserInput.StringPrompt($"Stack with the name {name} already exists! \n\nEnter stack name (or press 0 to cancel):");
        }

        if (name == "0")
        {
            return;
        }

        _stacksRepository.Add(new Stack
        {
            Name = name
        });

        AnsiConsole.Write($"\nNew stack {name} was succesfully added! Press any key to continue...");
        Console.ReadKey();
    }

    internal void Delete()
    {
        var stack = Get("Select a stack to delete (or press 0 to cancel):");

        if (stack.Id == 0)
        {
            return;
        }

        if (AnsiConsole.Confirm($"Are you sure you want to delete {stack.Name} stack and all associated flashcards?"))
        {
            _stacksRepository.Delete(stack);

            AnsiConsole.Write($"Stack {stack.Name} was succesfully deleted! Press any key to continue...");
            Console.ReadKey();
        }
    }

    internal void Update()
    {
        var stack = Get("Select a stack you want to update:");

        if (stack.Id == 0)
        {
            return;
        }

        var name = UserInput.StringPrompt($"Enter a new name for the stack {stack.Name} (or press 0 to cancel):");

        if (name.Trim() == "0")
        {
            return;
        }

        if (AnsiConsole.Confirm($"Are you sure you want to rename stack {stack.Name} to {name}?"))
        {
            stack.Name = name.Trim();
            _stacksRepository.Update(stack);

            AnsiConsole.Write($"\nStack was succesfully updated to {name}! Press any key to continue...");
            Console.ReadKey();
        }
    }

    internal Stack Get(string prompt)
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
