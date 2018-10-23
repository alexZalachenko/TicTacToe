using UnityEngine;
using System.Collections.Generic;

public class PlaceRandomFromList : Action
{
    public List<BoardPosition> m_moves = new List<BoardPosition>();

    public override Move PerformAction(BoardData p_boardData)
    {
        BoardPosition t_randomMove = m_moves[Random.Range(0, m_moves.Count)];
        return new Move(t_randomMove.Row, t_randomMove.Column);
    }
}
