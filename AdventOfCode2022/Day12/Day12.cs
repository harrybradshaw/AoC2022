using AdventOfCode2022.Common;

namespace AdventOfCode2022.Day12;

public class Day12
{
    public Day12()
    {
        var grid = FileReader
            .ReadAllLines("Day12Test.txt")
            .Select(line => line.ToCharArray().ToList())
            .ToList();

        var startS = FindChar(grid, 'S');
        grid[startS.X][startS.Y] = 'a';

        var starts = FindStartingCoords(grid);
        var ans = PerformSearch(starts, grid);
        Console.WriteLine(ans);
    }
    
    private static int PerformSearch(List<Coord> starts, List<List<char>> grid)
    {
        var searchDriver = new SearchDriver(grid);
        var minMoveLength = starts.Min(start =>
        {
            searchDriver.MinGrid[start.X][start.Y] = 0;
            var sn = new SearchNode(searchDriver, start,0);
            return sn.FindAndExecuteMoves();
        });
        return minMoveLength;
    }
    
    private static List<Coord> FindStartingCoords(List<List<char>> grid)
    {
        return grid.SelectMany((line, i) =>
            {
                return line.Select((x, j) =>
                {
                    if (x == 'a')
                    {
                        return new Coord
                        {
                            X = i,
                            Y = j,
                        };
                    }

                    return null;
                });
            }).Where(s => s != null)
            .ToList()!;
    }
    
    private static Coord FindChar(List<List<char>> grid, char c)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[0].Count; j++)
            {
                if (grid[i][j] == c)
                {
                    return new Coord
                    {
                        X = i,
                        Y = j,
                    };
                } 
            }
        }

        throw new Exception();
    }
}

public class SearchDriver
{
    public readonly List<List<char>> Grid;
    public List<List<int?>> MinGrid { get; set; }
    public SearchDriver(List<List<char>> grid)
    {
        Grid = grid;
        MinGrid = grid.Select(line =>
        {
            return line.Select(_ => (int?) null).ToList();
        }).ToList();
    }
}

public class SearchNode
{
    private SearchDriver Driver { get; set; }
    private Coord Location { get; set; }
    private int MoveCount { get; set; }

    public SearchNode(
        SearchDriver driver,
        Coord location,
        int moveCount)
    {
        Driver = driver;
        Location = location;
        MoveCount = moveCount;
    }

    public int FindAndExecuteMoves()
    {
        if (CheckSquareForFound())
        {
            return MoveCount;
        }
        var possMoves = new List<Coord>
        {
            Location.Add(Coord.North()),
            Location.Add(Coord.East()),
            Location.Add(Coord.South()),
            Location.Add(Coord.West()),
        };
        var validMoves = possMoves.Where(CheckMoveValid).ToList();
        if (!validMoves.Any())
        {
            return 1000000000;
        }

        return validMoves.Min(move =>
        {
            Driver.MinGrid[move.X][move.Y] = MoveCount + 1;
            var sn = new SearchNode(Driver, move, MoveCount + 1);
            return sn.FindAndExecuteMoves();
        });
    }

    private bool CheckSquareForFound()
    {
        return (Driver.Grid[Location.X][Location.Y] == 'E');
    }

    private bool CheckMoveValid(Coord newMove)
    {
        return CheckSquareInBounds(newMove) &&
               CheckSquareContents(newMove) &&
               CheckSquareNotVisit(newMove);
    }

    private bool CheckSquareNotVisit(Coord newMove)
    {
        return Driver.MinGrid[newMove.X][newMove.Y] == null ||
               MoveCount + 1 < Driver.MinGrid[newMove.X][newMove.Y];
    }
    private bool CheckSquareContents(Coord newMove)
    {
        if (Driver.Grid[newMove.X][newMove.Y] == 'E')
        {
            return 'z' - 1 <= Driver.Grid[Location.X][Location.Y];
        }
        return Driver.Grid[newMove.X][newMove.Y] - 1 <= Driver.Grid[Location.X][Location.Y];
    }

    private bool CheckSquareInBounds(Coord newMove)
    {
        return newMove.X < Driver.Grid.Count
               && newMove.X >= 0
                && newMove.Y < Driver.Grid[0].Count && newMove.Y >= 0;
    }
}

public class Coord
{
    public int X { get; init; }
    public int Y { get; init; }

    public Coord Add(Coord toAdd)
    {
        return new Coord
        {
            X = this.X + toAdd.X,
            Y = this.Y + toAdd.Y,
        };
    }

    public static Coord North()
    {
        return new Coord
        {
            X = -1,
            Y = 0,
        };
    }
    
    public static Coord East()
    {
        return new Coord
        {
            X = 0,
            Y = 1,
        };
    }
    
    public static Coord South()
    {
        return new Coord
        {
            X = 1,
            Y = 0,
        };
    }
    
    public static Coord West()
    {
        return new Coord
        {
            X = 0,
            Y = -1,
        };
    }
}
