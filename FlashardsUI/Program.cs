using FlashardsUI;
using FlashcardsLibrary;

var database = new DatabaseManager();
database.CreateDatabase();
database.CreateTables();

var menu = new Menu();
menu.MainMenu();