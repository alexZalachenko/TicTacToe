using UnityEngine;
using System.Collections.Generic;

public class PlaceRandom : Action
{
    public override Move PerformAction(BoardData p_boardData)
    {
        List<Move> t_availableMoves = new List<Move>();
        p_boardData.GetMoves(t_availableMoves);

        Move t_randomMove = t_availableMoves[Random.Range(0, t_availableMoves.Count)];
        return new Move(t_randomMove.Row, t_randomMove.Column);
    }
}
