namespace AdventOfCode2022.Day11;

public class MonkeyFactory
{
    public static List<Monkey> CreateMonkeys()
    {
        return new List<Monkey>
        {
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 83, 88, 96, 79, 86, 88, 70}),
                Operation = value => value * 5,
                TestDivisor = 11,
                TrueTarget = 2,
                FalseTarget = 3,
            },
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 59, 63, 98, 85, 68, 72}),
                Operation = value => value * 11,
                TestDivisor = 5,
                TrueTarget = 4,
                FalseTarget = 0,
            },
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 90, 79, 97, 52, 90, 94, 71, 70}),
                Operation = value => value + 2,
                TestDivisor = 19,
                TrueTarget = 5,
                FalseTarget = 6,
            },
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 97, 55, 62}),
                Operation = value => value + 5,
                TestDivisor = 13,
                TrueTarget = 2,
                FalseTarget = 6,
            },
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 74, 54, 94, 76}),
                Operation = value => value * value,
                TestDivisor = 7,
                TrueTarget = 0,
                FalseTarget = 3,
            },
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 58}),
                Operation = value => value + 4,
                TestDivisor = 17,
                TrueTarget = 7,
                FalseTarget = 1,
            },
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 66, 63}),
                Operation = value => value + 6,
                TestDivisor = 2,
                TrueTarget = 7,
                FalseTarget = 5,
            },
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 56, 56, 90, 96, 68}),
                Operation = value => value + 7,
                TestDivisor = 3,
                TrueTarget = 4,
                FalseTarget = 1,
            },
            
        };
    }
    
    public static List<Monkey> CreateTestMonkeys()
    {
        return new List<Monkey>
        {
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 79, 98}),
                Operation = value => value * 19,
                TestDivisor = 23,
                TrueTarget = 2,
                FalseTarget = 3,
            },
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 54, 65, 75, 74}),
                Operation = value => value + 6,
                TestDivisor = 19,
                TrueTarget = 2,
                FalseTarget = 0,
            },
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 79, 60, 97}),
                Operation = value => value * value,
                TestDivisor = 13,
                TrueTarget = 1,
                FalseTarget = 3,
            },
            new Monkey
            {
                Items = new Queue<long>(new[] {(long) 74}),
                Operation = value => value + 3,
                TestDivisor = 17,
                TrueTarget = 0,
                FalseTarget = 1,
            },
        };
    }
}