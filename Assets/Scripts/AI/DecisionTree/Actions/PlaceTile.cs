using UnityEngine;

public class PlaceTile : Action
{
    [SerializeField]
    BoardPosition m_position = null;

    public override Move PerformAction(BoardData p_boardData)
    {
        return new Move(m_position.Row, m_position.Column);
    }
}
