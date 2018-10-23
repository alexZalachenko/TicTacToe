
public abstract class Action : TreeNode
{
	public override TreeNode NavigateTree(BoardData p_boardData)
    {
        return this;
    }

    public abstract Move PerformAction(BoardData p_boardData);
}
