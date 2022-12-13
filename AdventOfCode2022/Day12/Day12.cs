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

        //var starts = new List<Coord> {startS};
        var starts = FindStartingCoords(grid);
        var ans = PerformSearch(starts, grid);
        Console.WriteLine(ans);
    }
    
    private static int PerformSearch(List<Coord> starts, List<List<char>> grid)
    {
        var searchDriver = new SearchDriver(grid);
        starts.Select(start => new SearchNode(searchDriver, start, 0))
            .ToList()
            .ForEach(node => searchDriver.SearchNodes.Enqueue(node));
        return searchDriver.ProcessNodes();
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
    public List<List<int?>> MinGrid { get; }
    public Queue<SearchNode> SearchNodes { get; set; }
    public SearchDriver(List<List<char>> grid)
    {
        Grid = grid;
        MinGrid = grid.Select(line => line.Select(_ => (int?) null).ToList()).ToList();
        SearchNodes = new Queue<SearchNode>();
    }

    public int ProcessNodes()
    {
        while (SearchNodes.Count > 0)
        {
            var res = SearchNodes
                .Dequeue()
                .FindOrQueueMoves();
            if (res.HasValue) return res.Value;
        }

        throw new Exception();
    }
}

public class SearchNode
{
    private SearchDriver Driver { get; }
    private Coord Location { get; }
    private int MoveCount { get; }

    public SearchNode(SearchDriver driver, Coord location, int moveCount)
    {
        driver.MinGrid[location.X][location.Y] = moveCount;
        Driver = driver;
        Location = location;
        MoveCount = moveCount;
    }

    public int? FindOrQueueMoves()
    {
        if (Driver.Grid[Location.X][Location.Y] == 'E')
        {
            return MoveCount;
        }

        var validMoves = Coord.Directions
            .Select(c => new Coord{X = Location.X + c.X, Y = Location.Y + c.Y})
            .Where(CheckMoveValid)
            .ToList();

        validMoves.ForEach(newLocation =>
        {
            var sn = new SearchNode(Driver, newLocation, MoveCount + 1);
            Driver.SearchNodes.Enqueue(sn);
        });

        return null;
    }

    private bool CheckMoveValid(Coord newMove)
    {
        return CheckSquareInBounds(newMove) &&
               CheckSquareContents(newMove) &&
               CheckSquareNotBetterVisited(newMove);
    }

    private bool CheckSquareNotBetterVisited(Coord newMove)
    {
        return Driver.MinGrid[newMove.X][newMove.Y] == null || MoveCount + 1 < Driver.MinGrid[newMove.X][newMove.Y];
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
        return newMove.X < Driver.Grid.Count && newMove.X >= 0 && newMove.Y < Driver.Grid[0].Count && newMove.Y >= 0;
    }
}

public class Coord
{
    public int X { get; init; }
    public int Y { get; init; }

    public static readonly List<Coord> Directions = new()
    {
        new Coord {X = -1, Y = 0,},
        new Coord {X = 1, Y = 0,},
        new Coord {X = 0, Y = -1,},
        new Coord {X = 0, Y = 1,},
    };
}
