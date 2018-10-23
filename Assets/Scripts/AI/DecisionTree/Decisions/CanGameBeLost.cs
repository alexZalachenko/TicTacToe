using Util;

public class CanGameBeLost : Decision
{
    public override TreeNode MakeDecision(BoardData p_boardData)
    {
        if (p_boardData.GetMoveToWin(p_boardData.CurrentPlayer ^ (Players)1).Count > 0)
        {
            
            return m_trueNode;
        }
        else
        {
            return m_falseNode;
        }
    }
}
