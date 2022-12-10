namespace AdventOfCode2022.Day07;

public class TreeNode
{
    public string Name { get;}
    public List<TreeNode> Children { get; set; }
    public HashSet<string> ChildrenNames { get; set; }
    public HashSet<string> FileNames { get; set; }
    public TreeNode? Parent { get; set; }
    private int TotalFileSize { get; set; }

    public TreeNode(string name)
    {
        Name = name;
        Children = new List<TreeNode>();
        ChildrenNames = new HashSet<string>();
        FileNames = new HashSet<string>();
    }

    public TreeNode AddChild(string name)
    {
        var newChild = new TreeNode(name)
        {
            Parent = this
        };
        Children.Add(newChild);
        ChildrenNames.Add(name);
        return newChild;
    }

    public TreeNode GetChild(string name)
    {
        return Children.First(tn => tn.Name == name);
    }

    public void AddFile(string fileName, int fileSize)
    {
        FileNames.Add(fileName);
        TotalFileSize += fileSize;
    }

    public int GetTotalDirectorySize()
    {
        return TotalFileSize + Children.Aggregate(0, (acc, c) => acc + c.GetTotalDirectorySize());
    }

    public TreeNode TryGetParent()
    {
        if (Parent != null)
        {
            return Parent;
        }

        throw new NullReferenceException();
    }
}