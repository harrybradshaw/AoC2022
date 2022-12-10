namespace AdventOfCode2022.Day10;

public class CRT
{
    private readonly CPU _processor;
    private List<string> Output { get; }
    private string _workingLine = "";
    private int PixelPosition => (_processor.CompletedCycleCounter - 1) % 40;

    public CRT(CPU cpu)
    {
        _processor = cpu;
        Output = new List<string>();
    }

    public void Render()
    {
        _workingLine += IsSpriteHere()
            ? "#"
            : ".";
        if (_workingLine.Length == 40)
        {
            Output.Add(_workingLine);
            _workingLine = "";
        }
    }

    public void DisplayOutput()
    {
        Output.ForEach(Console.WriteLine);
    }

    private bool IsSpriteHere()
    {
        var spritePos = _processor.XRegister;
        return PixelPosition == spritePos || (spritePos - 1 == PixelPosition) || (spritePos + 1 == PixelPosition);
    }
}