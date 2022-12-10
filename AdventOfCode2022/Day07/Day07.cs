using AdventOfCode2022.Common;

namespace AdventOfCode2022.Day07;

public class Day07
{
    private TreeNode _treeNode;
    private List<TreeNode> dirs;

    public Day07()
    {
        _treeNode = new TreeNode("/")
        {
            Parent = null,
        };
        dirs = new List<TreeNode>();
        FileReader
            .ReadAllLines("Day7Input.txt")
            .ForEach(ParseInput);
        GetTopLevel();
        var totalSize = _treeNode.GetTotalDirectorySize();
        var target = 30000000 - (70000000 - totalSize);
        var x = dirs.Select(d => d.GetTotalDirectorySize())
            .Where(d => d >= target)
            .Order()
            .First();
        Console.WriteLine(x);
    }

    private TreeNode GetTopLevel()
    {
        while (_treeNode.Parent != null)
        {
            _treeNode = _treeNode.Parent;
        }

        return _treeNode;
    }

    private void ParseInput(string line)
    {
        var splitLine = line.Split(" ");

        if (line.StartsWith('$'))
        {
            if (splitLine[1] == "cd")
            {
                var inst = splitLine[2].Split("/");
                inst.ToList().ForEach(NavigateTree);
            }

            return;
        }

        if (splitLine[0] == "dir")
        {
            TryCreateNewDirectory(_treeNode, splitLine[1]);
            return;
        }
        
        TryCreateNewFile(_treeNode, splitLine[1], int.Parse(splitLine[0]));
    }

    private void NavigateTree(string instruction)
    {
        _treeNode = instruction switch
        {
            "" => GetTopLevel(),
            ".." => _treeNode.TryGetParent(),
            _ => _treeNode.GetChild(instruction)
        };
    }

    private void TryCreateNewDirectory(TreeNode treeNode, string dirName)
    {
        if (!treeNode.ChildrenNames.Contains(dirName))
        {
            var addedNode = treeNode.AddChild(dirName);
            dirs.Add(addedNode);
        }
    }

    private void TryCreateNewFile(TreeNode treeNode, string fileName, int fileSize)
    {
        if (!treeNode.FileNames.Contains(fileName))
        {
            treeNode.AddFile(fileName, fileSize);
        }
    }
}