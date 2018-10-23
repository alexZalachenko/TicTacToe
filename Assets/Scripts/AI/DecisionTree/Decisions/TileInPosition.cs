using UnityEngine;
using Util;

public class TileInPosition : Decision
{
    [SerializeField]
    BoardPosition m_position = null;
    
    public override TreeNode MakeDecision(BoardData p_boardData)
    {
        if (p_boardData.GetTiles()[m_position.Row, m_position.Column] != Tiles.Empty)
        {
            return m_trueNode;
        }
        else
        {
            return m_falseNode;
        }
    }
}
