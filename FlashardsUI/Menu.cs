﻿using FlashcardsLibrary.Models;
using FlashcardsLibrary.Repositories;
using Spectre.Console;
using static FlashardsUI.Enums;

namespace FlashardsUI;
internal class Menu
{
    StacksController stacksController = new(new StacksRepository());
    FlashcardsController flashcardsController = new(new  FlashcardsRepository());

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
                    flashcardsController.CurrentStack = stacksController.Get("Select a stack of flashcards to manage:");
                    if (flashcardsController.CurrentStack.Id == 0)
                    {
                        return;
                    }
                    FlashcardsMenu(flashcardsController.CurrentStack);
                    break;
                case MainMenuOptions.StudySessions:
                    break;
                case MainMenuOptions.CloseApplication:
                    if (AnsiConsole.Confirm("Are you sure you want to exit?"))
                    {
                        AnsiConsole.WriteLine("\nGoodbye!");
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

    internal void FlashcardsMenu(Stack stack)
    {
        var exit = false;

        while (!exit)
        {
            Console.Clear();

            var selection = UserInput.EnumPrompt<FlashcardsMenuOptions>($"Manage flashcards from the {stack.Name} stack\nSelect from the options");

            switch (selection)
            {
                case FlashcardsMenuOptions.ChangeStack:
                    var newStack = stacksController.Get("Select a stack of flashcards to manage:");
                    if (newStack.Id == 0)
                    {
                        continue;
                    }
                    stack = newStack;
                    break;
                case FlashcardsMenuOptions.ViewAllFlashcards:
                    flashcardsController.GetAll();
                    break;
                case FlashcardsMenuOptions.AddFlashcard:
                    flashcardsController.Post();
                    break;
                case FlashcardsMenuOptions.DeleteFlashcard:
                    flashcardsController.Delete();
                    break;
                case FlashcardsMenuOptions.UpdateFlashcard:
                    flashcardsController.Update();
                    break;
                case FlashcardsMenuOptions.MainMenu:
                    exit = true;
                    break;
            }
        }
    }
}
