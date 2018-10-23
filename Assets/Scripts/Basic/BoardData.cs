using System.Collections.Generic;
using UnityEngine;
using Util;

public class BoardData
{
    private Color m_defaultAIColor = Color.red;
    private Color m_defaultHumanColor = Color.black;
    public Color CurrentPlayerColor { get; private set; }
    Tiles[,] m_boardTiles;
    public Tiles CurrentPlayerTile { get; private set; }
    public Players CurrentPlayer { get; private set; }
    // The tile that the player will use when placing a tile
    public Tiles HumanTile { get; private set; }
    private const Tiles m_defaultHumanTile = Tiles.Cross;
    private Tiles m_enemyTile;

    public BoardData(Players p_player, Tiles p_humanTile = m_defaultHumanTile)
    {
        InitializePlayersTiles(p_player, p_humanTile);
        m_boardTiles = new Tiles[3, 3]
        {
            { Tiles.Empty, Tiles.Empty, Tiles.Empty },
            { Tiles.Empty, Tiles.Empty, Tiles.Empty },
            { Tiles.Empty, Tiles.Empty, Tiles.Empty }
        };
    }

    public BoardData(Players p_player, Tiles[,] p_boardTiles, Tiles p_humanTile)
    {
        InitializePlayersTiles(p_player, p_humanTile);
        m_boardTiles = p_boardTiles;
    }

    private void InitializePlayersTiles(Players p_player, Tiles p_humanTile)
    {
        HumanTile = p_humanTile;
        CurrentPlayer = p_player;
        CurrentPlayerTile = HumanTile;
        CurrentPlayerColor = m_defaultHumanColor;
        if (Players.AI == p_player)
        {
            CurrentPlayerTile = (Tiles)1 ^ HumanTile;
            CurrentPlayerColor = m_defaultAIColor;
        }
        m_enemyTile = CurrentPlayerTile ^ (Tiles)1;
    }

    public void SwapTiles()
    {
        HumanTile = HumanTile ^ (Tiles)1;
        m_enemyTile = m_enemyTile ^ (Tiles)1;
        CurrentPlayerTile = m_enemyTile ^ (Tiles)1;
        
        for (int t_row = 0; t_row < m_boardTiles.GetLength(0); t_row++)
        {
            for (int t_column = 0; t_column < m_boardTiles.GetLength(1); t_column++)
            {
                if (m_boardTiles[t_row, t_column] != Tiles.Empty)
                {
                    m_boardTiles[t_row, t_column] = m_boardTiles[t_row, t_column] == Tiles.Circle ? Tiles.Cross : Tiles.Circle;
                }
            }
        }
    }

    public Tiles[,] GetTiles()
    {
        return m_boardTiles;
    }

    // Return all the empty tiles
    public List<Move> GetMoves(List<Move> p_moves)
    {
        for (int t_rows = 0; t_rows < m_boardTiles.GetLength(0); t_rows++)
        {
            for (int t_columns = 0; t_columns < m_boardTiles.GetLength(1); t_columns++)
            {
                if (m_boardTiles[t_rows, t_columns] == Tiles.Empty)
                    p_moves.Add(new Move(t_rows, t_columns));
            }
        }

        return p_moves;
    }

    public void MakeMove(Move p_move)
    {
        m_boardTiles[p_move.Row, p_move.Column] = CurrentPlayerTile;

        // Swap tiles
        Tiles t_auxTile = m_enemyTile;
        m_enemyTile = CurrentPlayerTile;
        CurrentPlayerTile = t_auxTile;
        CurrentPlayerColor = CurrentPlayerColor == m_defaultHumanColor ? m_defaultAIColor : m_defaultHumanColor;

        CurrentPlayer = CurrentPlayer ^ (Players)1; // To get the opposite player
    }

    public BoardData GetNewBoard(Move p_move)
    {
        Players t_newPlayer = CurrentPlayer ^ Players.AI; // Tricky way to get the opposite player

        Tiles[,] t_newBoardTiles = m_boardTiles.Clone() as Tiles[,];
        t_newBoardTiles[p_move.Row, p_move.Column] = CurrentPlayerTile;

        return new BoardData(t_newPlayer, t_newBoardTiles, HumanTile);
    }

    public GameResults GetGameResult()
    {
        // Check if any player has completed a line
        Tiles t_winningTile = Tiles.Empty;
        if (IsWinnerBoard(CurrentPlayerTile))
        {
            t_winningTile = CurrentPlayerTile;
        }
        else if (IsWinnerBoard(m_enemyTile))
        {
            t_winningTile = m_enemyTile;
        }
        if (t_winningTile != Tiles.Empty)
        {
            if (t_winningTile == HumanTile)
            {
                return GameResults.Win;
            }
            else
            {
                return GameResults.Lose;
            }
        }
        
        // Check if there are empty tiles
        for (int t_rows = 0; t_rows < m_boardTiles.GetLength(0); t_rows++)
        {
            for (int t_columns = 0; t_columns < m_boardTiles.GetLength(1); t_columns++)
            {
                if (m_boardTiles[t_rows, t_columns] == Tiles.Empty)
                {
                    return GameResults.Unfinished;
                }
            }
        }
        // No completed lines but no empty tiles in the board? Then it's over
        return GameResults.Tie;
    }

    public bool IsCornerTile(Tiles p_tile)
    {
        if (m_boardTiles[0, 0] == p_tile || m_boardTiles[2, 0] == p_tile ||
            m_boardTiles[0, 2] == p_tile || m_boardTiles[2, 2] == p_tile)
        {
            return true;
        }
        return false;
    }

    public bool IsWinnerBoard(Tiles p_tile)
    {
        // Top-left diagonal
        if (m_boardTiles[0, 0] == p_tile)
        {
            if (IsWinnerLine(0, 0, 1, 1, m_boardTiles[0, 0]))
            {
                return true;
            }
        }
        // Top-right diagonal
        if (m_boardTiles[0, 2] == p_tile)
        {
            if (IsWinnerLine(0, 2, 1, -1, p_tile))
            {
                return true;
            }
        }
        // Horizontal lines in first column
        for (int t_rows = 0; t_rows < m_boardTiles.GetLength(0); t_rows++)
        {
            if (m_boardTiles[t_rows, 0] == p_tile)
            {
                if (IsWinnerLine(t_rows, 0, 0, 1, p_tile))
                {
                    return true;
                }
            }

        }
        // Vertical lines in first row
        for (int t_columns = 0; t_columns < m_boardTiles.GetLength(1); t_columns++)
        {
            if (m_boardTiles[0, t_columns] == p_tile)
            {
                if (IsWinnerLine(0, t_columns, 1, 0, p_tile))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public List<Move> GetMoveToWin(Tiles p_tile)
    {
        List<Move> t_winningMoves = new List<Move>();
        
        // Top-left diagonal
        Move t_winnerMove = null;
        t_winnerMove = GetMoveToWinInLine(0, 0, 1, 1, p_tile);
        if (t_winnerMove != null)
        {
            t_winningMoves.Add(t_winnerMove);
        }
        // Top-right diagonal
        t_winnerMove = GetMoveToWinInLine(0, 2, 1, -1, p_tile);
        if (t_winnerMove != null)
        {
            t_winningMoves.Add(t_winnerMove);
        }
        // Horizontal lines in first column
        for (int t_rows = 0; t_rows < m_boardTiles.GetLength(0); t_rows++)
        {
            t_winnerMove = GetMoveToWinInLine(t_rows, 0, 0, 1, p_tile);
            if (t_winnerMove != null)
            {
                t_winningMoves.Add(t_winnerMove);
            }
        }
        // Vertical lines in first row
        for (int t_columns = 0; t_columns < m_boardTiles.GetLength(1); t_columns++)
        {
            t_winnerMove = GetMoveToWinInLine(0, t_columns, 1, 0, p_tile);
            if (t_winnerMove != null)
            {
                t_winningMoves.Add(t_winnerMove);
            }
        }

        return t_winningMoves;
    }

    public List<Move> GetMoveToWin(Players p_player)
    {
        Tiles t_playingTile = p_player == CurrentPlayer ? CurrentPlayerTile : m_enemyTile;
        return GetMoveToWin(t_playingTile);
    }

    public int CountGhostChecks(Tiles p_playerTile)
    {
        int t_maxChecks = 0;
        for (int t_row = 0; t_row < m_boardTiles.GetLength(0); t_row++)
        {
            for (int t_column = 0; t_column < m_boardTiles.GetLength(1); t_column++)
            {
                if (m_boardTiles[t_row, t_column] == Tiles.Empty)
                {
                    m_boardTiles[t_row, t_column] = p_playerTile;
                    int t_checks = GetMoveToWin(p_playerTile).Count;
                    if (t_checks > t_maxChecks)
                    {
                        t_maxChecks = t_checks;
                    }
                    m_boardTiles[t_row, t_column] = Tiles.Empty;
                }
            }
        }

        return t_maxChecks;
    }

    private bool IsWinnerLine(int p_startRow, int p_startColumn, int p_rowIncrement, int p_columnIncrement, Tiles p_tile)
    {
        if (m_boardTiles[p_startRow + p_rowIncrement, p_startColumn + p_columnIncrement] == p_tile &&
            m_boardTiles[p_startRow + p_rowIncrement * 2, p_startColumn + p_columnIncrement * 2] == p_tile)
        {
            return true;
        }
        return false;
    }
    
    private Move GetMoveToWinInLine(int p_startRow, int p_startColumn, int p_rowIncrement, int p_columnIncrement, Tiles p_tile)
    {
        int t_sameSymbolTiles = 0;
        int t_emptyRow = -1;
        int t_emptyColumn = -1;
        for (int t_step = 0; t_step < 3; t_step++)// 3 being the board width and height
        {
            Tiles t_tile = m_boardTiles[p_startRow + p_rowIncrement * t_step, p_startColumn + p_columnIncrement * t_step];
            if (t_tile == p_tile)
            {
                ++t_sameSymbolTiles;
            }
            else if (t_tile != Tiles.Empty)
            {
                return null;
            }
            else
            {
                t_emptyRow = p_startRow + p_rowIncrement * t_step;
                t_emptyColumn = p_startColumn + p_columnIncrement * t_step;
            }
        }

        if (t_sameSymbolTiles == 2)
        {
            return new Move(t_emptyRow, t_emptyColumn);
        }
        else
        {
            return null;
        }
    }
}
