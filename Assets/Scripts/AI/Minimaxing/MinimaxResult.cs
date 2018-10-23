
public class MinimaxResult
{
    public int Score { get; private set; }
    public Move Move { get; private set; }

    public MinimaxResult(int p_score, Move p_move)
    {
        Score = p_score;
        Move = p_move;
    }
}
