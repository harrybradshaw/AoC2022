using AdventOfCode2022.Common;

namespace AdventOfCode2022.Day02;

public class Day02
{
    public Day02()
    {
        var lines = FileReader.ReadAllLines("Day2Input.txt")
            .Select(line =>
            {
                var splitLine = line.Split(" ");
                return new GameRound
                {
                    Player1 = ParseP1Input(splitLine[0]),
                    Player2 = ParseP2Input(splitLine[1]),
                };
            });
        var score = lines.Aggregate(0, (acc, gr) => acc + gr.RoundScore);
        Console.WriteLine(score);
    }

    private PlayerInput ParseP1Input(string playerInput)
    {
        var firstChar = playerInput[0];
        return (PlayerInput) (firstChar - 65);
    }
    
    private PlayerInput ParseP2Input(string playerInput)
    {
        var firstChar = playerInput[0];
        return (PlayerInput) (firstChar - 88);
    }
    
}

public class GameRound
{
    public PlayerInput Player1 { get; set; }
    public PlayerInput Player2 { get; set; }

    public int RoundScore => CalculateScoreFromEnding();

    private int CalculateScoreFromInputs()
    {
        var diff = Math.Abs(Player2 - Player1);
        var scores = new [] {(int) Player1, (int) Player2};
        return diff switch
        {
            0 => 3 + (int) Player2 + 1,
            1 => ((int) Player2) == scores.Max()
                ? 6 + (1 + (int) Player2)
                : 1 + (int) Player2,
            _ => ((int) Player2) == scores.Min()
                ? 6 + (1 + (int) Player2)
                : 1 + (int) Player2,
        };
    }

    private int CalculateScoreFromEnding()
    {
        var ending = (GameEnding) Player2;
        var played = CalculatePlayedMove(Player1, ending);
        return (3 * (int) ending) + (1 + (int) played);
    }

    private static PlayerInput CalculatePlayedMove(PlayerInput playerInput, GameEnding gameEnding)
    {
        return gameEnding switch
        {
            GameEnding.Draw => playerInput,
            GameEnding.Win => CalculateWinningMove(playerInput),
            GameEnding.Lose => CalculateLosingMove(playerInput),
            _ => throw new ArgumentOutOfRangeException(nameof(gameEnding))
        };
    }

    private static PlayerInput CalculateWinningMove(PlayerInput playerInput)
    {
        return playerInput switch
        {
            PlayerInput.Rock => PlayerInput.Paper,
            PlayerInput.Paper => PlayerInput.Scissors,
            PlayerInput.Scissors => PlayerInput.Rock,
            _ => throw new ArgumentOutOfRangeException(nameof(playerInput), playerInput, null)
        };
    }
    
    private static PlayerInput CalculateLosingMove(PlayerInput playerInput)
    {
        return playerInput switch
        {
            PlayerInput.Rock => PlayerInput.Scissors,
            PlayerInput.Paper => PlayerInput.Rock,
            PlayerInput.Scissors => PlayerInput.Paper,
            _ => throw new ArgumentOutOfRangeException(nameof(playerInput), playerInput, null)
        };
    }
}

public enum PlayerInput
{
    Rock,
    Paper,
    Scissors,
}

public enum GameEnding
{
    Lose,
    Draw,
    Win,
}