using UnityEngine;

public abstract class Decision : TreeNode
{
    [SerializeField]
    protected TreeNode m_trueNode = null;
    [SerializeField]
    protected TreeNode m_falseNode = null;

    public override TreeNode NavigateTree(BoardData p_boardData) 
    {
        return MakeDecision(p_boardData).NavigateTree(p_boardData);
    }

    public abstract TreeNode MakeDecision(BoardData p_boardData);
}