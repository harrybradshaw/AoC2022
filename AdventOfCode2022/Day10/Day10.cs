using AdventOfCode2022.Common;

namespace AdventOfCode2022.Day10;

public class Instruction
{
    public int CyclesRemaining { get; set; }
    public int Value { get; set; }
}

public class Day10
{
    private CPU _processor;
    private CRT _display;
    public Day10()
    {
        _processor = new CPU();
        _display = new CRT(_processor);
        FileReader
            .ReadAllLines("Day10Input.txt")
            .ForEach(ProcessLine);
        _display.DisplayOutput();
    }

    private void ProcessLine(string line)
    {
        var splitString = line.Split(" ");
        var instructionType = splitString[0];
        switch (instructionType)
        {
            case "addx":
                AddNewXInstruction(int.Parse(splitString[1]));
                break;
        }
        Increment();
        ProcessInstructionUntilCompletion();
    }

    private void Increment()
    {
        _processor.IncrementCounter();
        _display.Render();
    }

    private void ProcessInstructionUntilCompletion()
    {
        while (_processor.CurrentInstruction != null)
        {
            if (_processor.CurrentInstruction.CyclesRemaining == 0)
            {
                _processor.XRegister += _processor.CurrentInstruction.Value;
                _processor.CurrentInstruction = null;
                return;
            }
            Increment();
        }
    }

    private void AddNewXInstruction(int valueToAdd)
    {
        _processor.CurrentInstruction = new Instruction
        {
            Value = valueToAdd,
            CyclesRemaining = 2,
        };
    }
}