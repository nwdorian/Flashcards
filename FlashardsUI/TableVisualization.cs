using FlashcardsLibrary.Models;
using Spectre.Console;

namespace FlashardsUI;
internal class TableVisualization
{
    internal static void ShowStacksTable(List<StackDTO> tableDataDTO)
    {
        var table = new Table();

        table.Title = new TableTitle("Stacks Table", "bold");

        table.AddColumn("Name");

        foreach (var stackDTO in tableDataDTO)
        {
            table.AddRow(stackDTO.Name!);
        }

        AnsiConsole.Write(table);
    }
}
