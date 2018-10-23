using Util;

public class PlaceBlockingTile : Action
{
    public override Move PerformAction(BoardData p_boardData)
    {
        return p_boardData.GetMoveToWin(p_boardData.CurrentPlayer ^ (Players)1)[0];
    }
}