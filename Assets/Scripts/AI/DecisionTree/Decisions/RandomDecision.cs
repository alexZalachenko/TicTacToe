using UnityEngine;

public class RandomDecision : Decision
{
    public override TreeNode MakeDecision(BoardData p_boardData)
    {
        return Random.value > 0.5f ? m_trueNode : m_falseNode;
    }
}
