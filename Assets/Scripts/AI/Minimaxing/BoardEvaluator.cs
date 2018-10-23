using UnityEngine;
using Util;

class BoardEvaluator
{
    private enum BoardScores
    {
        CORNER = 5,
        CHECK = 20,
        EASYTRAP = 25,
        DOUBLETRAP = 30,
        TRIPLETRAP = 40,
        TERMINAL = 50
    }

    /*
     *      BoardsScores explanation:
     * 
     * BoardScores are checked from the highest score to the lowest, if a 
     * board fits in a BoardScores score, the that score is returned and 
     * there is no further checking.
     * What a BoardScores score consists of is explained below:
     * 
     * TERMINAL: board with two CHECK where the enemy will lose whatever move
     * he does.
     * X · O
     * · O ·
     * X · X
     * 
     * TRIPLETRAP: board where by placing one more tile there would be three CHECK
     * X · @   @ = By placing another X here there would be three CHECK
     * · · ·
     * X O ·
     * 
     * DOUBLETRAP: same as before, but by placing the said tile there would be two CHECK
     * · · @  @ = By placing another X here there would be three CHECK
     * · · ·
     * X O X
     * 
     * EASYTRAP: DOUBLETRAP that is at the same time a CHECK
     * · · ·
     * · O ·
     * X · X
     * 
     * CHECK: board where by placing one more tile the game would be a win
     * O · ·
     * X X ·
     * O · ·
     * 
     * CORNER: board where the tile is in a corner
     * X · ·
     * · · ·
     * · · ·
     */

    public MinimaxResult EvaluateBoard(BoardData p_board, Players p_player)
    {
        int t_score = 0;
        Tiles t_currentPlayerTile = p_board.HumanTile;

        if (Players.AI == p_player)
        {
            t_currentPlayerTile = p_board.HumanTile ^ (Tiles)1;
        }
        
        GameResults t_gameResult = p_board.GetGameResult();
        if (t_gameResult == GameResults.Tie)
        {
            t_score = 0;
        }
        else if (t_gameResult == GameResults.Unfinished)
        {
            t_score = ScoreUnfinishedBoard(p_board, t_currentPlayerTile);
        }
        else
        {
            if (t_gameResult == GameResults.Win)
            {
                t_score = Minimax.MAX_SCORE;
            }
            else
            {
                t_score = -Minimax.MAX_SCORE;
            }
        }

        if (p_player == Players.AI)
        {
            t_score *= -1;
        }
        return new MinimaxResult(t_score, null);
    }

    private int ScoreUnfinishedBoard(BoardData p_board, Tiles p_currentPlayerTile)
    {
        // If there is an enemy check then this board is shitty
        if (p_board.GetMoveToWin(p_board.CurrentPlayer).Count > 0)
        {
            return 0;
        }

        int t_currentPlayerChecks = p_board.GetMoveToWin(p_board.CurrentPlayer ^ (Players)1).Count;
        if (t_currentPlayerChecks >= 2)
        {
            return (int)BoardScores.TERMINAL;
        }
        else
        {
            int t_ghostChecks = p_board.CountGhostChecks(p_currentPlayerTile);
            if (t_ghostChecks == 3)
            {
                return (int)BoardScores.TRIPLETRAP;
            }
            else if (t_ghostChecks == 2)
            {
                if (t_currentPlayerChecks > 0)
                {
                    return (int)BoardScores.EASYTRAP;
                }
                else
	            {
                    return (int)BoardScores.DOUBLETRAP;
                }
            }
            else
            {
                if (t_currentPlayerChecks > 0)
                {
                    return (int)BoardScores.CHECK;
                }
                else if (p_board.IsCornerTile(p_currentPlayerTile))
                {
                    return (int)BoardScores.CORNER;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
