using Util;
using System.Collections.Generic;

public class Minimax : IAlgorithm
{
    const int MAX_DEPTH = 2;
    public const int MAX_SCORE = 100;
    const int INFINITE = MAX_SCORE + 1;
    BoardEvaluator m_boardEvaluator = new BoardEvaluator();

    Move IAlgorithm.GetBestMovement(BoardData p_board)
    {
        return MinimaxStep(p_board, p_board.CurrentPlayer, 0).Move;
    }

    private MinimaxResult MinimaxStep(BoardData p_board, Players p_player, int p_currentDepth)
    {
        GameResults t_gameResult = p_board.GetGameResult();
        if (t_gameResult != GameResults.Unfinished)
        {
            int t_score = t_gameResult == GameResults.Win ? MAX_SCORE : -MAX_SCORE;
            return new MinimaxResult(t_score, null);
        }
        else if (p_currentDepth > MAX_DEPTH)
        {
            return m_boardEvaluator.EvaluateBoard(p_board, p_player);
        }

        Move t_bestMove = null;
        int t_bestScore = 0;

        if (p_board.CurrentPlayer == Players.Human)
            t_bestScore = -INFINITE;
        else
            t_bestScore = INFINITE;

        List<Move> t_moves = new List<Move>();
        p_board.GetMoves(t_moves);

        foreach (Move t_move in t_moves)
        {
            BoardData t_newBoard = p_board.GetNewBoard(t_move);
            MinimaxResult t_result = MinimaxStep(t_newBoard, p_player, p_currentDepth + 1);

            if (p_board.CurrentPlayer == Players.Human)
            {
                if (t_bestScore < t_result.Score)
                {
                    t_bestScore = t_result.Score;
                    t_bestMove = t_move;
                }
            }
            else
            {
                if (t_result.Score < t_bestScore)
                {
                    t_bestScore = t_result.Score;
                    t_bestMove = t_move;
                }
            }
        }

        return new MinimaxResult(t_bestScore, t_bestMove);
    }
}
