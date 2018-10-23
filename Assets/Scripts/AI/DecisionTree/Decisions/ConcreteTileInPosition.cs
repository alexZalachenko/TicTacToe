using UnityEngine;
using Util;

public class ConcreteTileInPosition : Decision
{
    [SerializeField]
    BoardPosition m_position = null;
    [SerializeField]
    bool m_checkEnemyTile;

    public override TreeNode MakeDecision(BoardData p_boardData)
    {
        Tiles t_tileToCheck = p_boardData.CurrentPlayerTile;
        if (m_checkEnemyTile)
        {
            t_tileToCheck ^= (Tiles)1;
        }

        if (p_boardData.GetTiles()[m_position.Row, m_position.Column] == t_tileToCheck)
        {
            return m_trueNode;
        }
        else
        {
            return m_falseNode;
        }
    }
}
