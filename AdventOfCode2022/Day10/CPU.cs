namespace AdventOfCode2022.Day10;

public class CPU
{
    public int XRegister { get; set; } = 1;
    public int CompletedCycleCounter { get; private set; }
    public Instruction? CurrentInstruction { get; set; }

    private int ValueSum { get; set; }

    public void IncrementCounter()
    {
        CompletedCycleCounter += 1;
        if (CurrentInstruction != null)
        {
            CurrentInstruction.CyclesRemaining -= 1;
        }
        
        var adjustedCycleCounter = CompletedCycleCounter - 20;
        if (adjustedCycleCounter % 40 == 0)
        {
            ValueSum += (XRegister * CompletedCycleCounter);
            Console.WriteLine(ValueSum);
        }
    }
}