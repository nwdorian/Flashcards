using FlashcardsLibrary.Repositories;
using Spectre.Console;
using static FlashardsUI.Enums;

namespace FlashardsUI;
internal class Menu
{
    StacksController stacksController = new(new StacksRepository());

    internal void MainMenu()
    {
        var exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();

            var selection = UserInput.EnumPrompt<MainMenuOptions>("Welcome to flashcards application!\nWhat would you like to do?");

            switch (selection)
            {
                case MainMenuOptions.ManageStacks:
                    StacksMenu();
                    break;
                case MainMenuOptions.ManageFlashcards:
                    FlashcardsMenu();
                    break;
                case MainMenuOptions.StudySessions:
                    break;
                case MainMenuOptions.CloseApplication:
                    if (AnsiConsole.Confirm("Are you sure you want to exit?"))
                    {
                        Console.WriteLine("\nGoodbye!");
                        exit = true;
                    }
                    else
                    {
                        exit = false;
                    }
                    break;
            }
        }
    }

    internal void StacksMenu()
    {
        var exit = false;

        while (!exit)
        {
            Console.Clear();

            var selection = UserInput.EnumPrompt<StacksMenuOptions>("Manage stacks of flashcards\nSelect from the options");

            switch (selection)
            {
                case StacksMenuOptions.ViewAllStacks:
                    stacksController.GetAll();
                    break;
                case StacksMenuOptions.AddStack:
                    stacksController.Post();
                    break;
                case StacksMenuOptions.DeleteStack:
                    stacksController.Delete();
                    break;
                case StacksMenuOptions.UpdateStack:
                    stacksController.Update();
                    break;
                case StacksMenuOptions.MainMenu:
                    exit = true;
                    break;
            }
        }
    }

    internal void FlashcardsMenu()
    {
        var exit = false;

        while (!exit)
        {
            Console.Clear();

            var selection = UserInput.EnumPrompt<FlashcardsMenuOptions>("Manage flashcards\nSelect from the options");

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
                    exit = true;
                    break;
            }
        }
    }
}
