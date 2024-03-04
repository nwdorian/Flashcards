using Spectre.Console;
using static FlashardsUI.Enums;

namespace FlashardsUI;
internal class Menu
{
    internal void MainMenu()
    {
        bool repeat = true;
        while (repeat)
        {
            Console.Clear();
            repeat = false;

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("Welcome to the [green]Flashcards[/] application\nWhat would you like to do?")
                .PageSize(10)
                .AddChoices(MainMenuOptions.ManageStacks,
                            MainMenuOptions.ManageFlashcards,
                            MainMenuOptions.StudySessions,
                            MainMenuOptions.CloseApplication));

            switch (selection)
            {
                case MainMenuOptions.ManageStacks:
                    StacksMenu();
                    MainMenu();
                    break;
                case MainMenuOptions.ManageFlashcards:
                    FlashcardsMenu();
                    MainMenu();
                    break;
                case MainMenuOptions.StudySessions:
                    break;
                case MainMenuOptions.CloseApplication:
                    if (AnsiConsole.Confirm("Are you sure you want to exit?"))
                    {
                        Console.WriteLine("\nGoodbye!");
                    }
                    else
                    {
                        repeat = true;
                    }
                    break;
            }
        }
    }

    internal void StacksMenu()
    {
        bool repeat = true;
        while (repeat)
        {
            Console.Clear();

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<StacksMenuOptions>()
                .Title("Manage stacks of flashcards\nSelect from the options")
                .PageSize(10)
                .AddChoices(StacksMenuOptions.ViewAllStacks,
                            StacksMenuOptions.AddStack,
                            StacksMenuOptions.DeleteStack,
                            StacksMenuOptions.UpdateStack,
                            StacksMenuOptions.MainMenu));

            switch (selection)
            {
                case StacksMenuOptions.ViewAllStacks:
                    break;
                case StacksMenuOptions.AddStack:
                    break;
                case StacksMenuOptions.DeleteStack:
                    break;
                case StacksMenuOptions.UpdateStack:
                    break;
                case StacksMenuOptions.MainMenu:
                    repeat = false;
                    break;
            }
        }
    }

    internal void FlashcardsMenu()
    {
        bool repeat = true;
        while (repeat)
        {
            Console.Clear();

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<FlashcardsMenuOptions>()
                .Title("Manage stacks of flashcards\nSelect from the options")
                .PageSize(10)
                .AddChoices(FlashcardsMenuOptions.ViewAllFlashcards,
                            FlashcardsMenuOptions.AddFlashcard,
                            FlashcardsMenuOptions.DeleteFlashcard,
                            FlashcardsMenuOptions.UpdateFlashcard,
                            FlashcardsMenuOptions.MainMenu));

            switch (selection)
            {
                case FlashcardsMenuOptions.ViewAllFlashcards:
                    break;
                case FlashcardsMenuOptions.AddFlashcard:
                    break;
                case FlashcardsMenuOptions.DeleteFlashcard:
                    break;
                case FlashcardsMenuOptions.UpdateFlashcard:
                    break;
                case FlashcardsMenuOptions.MainMenu:
                    repeat = false;
                    break;
            }
        }
    }
}
