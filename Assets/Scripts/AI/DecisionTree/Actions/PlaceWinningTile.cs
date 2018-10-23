
public class PlaceWinningTile : Action
{
    public override Move PerformAction(BoardData p_boardData)
    {
        return p_boardData.GetMoveToWin(p_boardData.CurrentPlayer)[0];
    }
}