using UnityEngine;
using Util;

public class TotalTilesCount : Decision
{
    [SerializeField]
    int m_amountToCompare = 0;

    public override TreeNode MakeDecision(BoardData p_boardData)
    {
        Tiles[,] t_tiles = p_boardData.GetTiles();
        int t_nonEmptyTiles = 0;
        for (int t_row = 0; t_row < t_tiles.GetLength(0); t_row++)
        {
            for (int t_column = 0; t_column < t_tiles.GetLength(1); t_column++)
            {
                if (t_tiles[t_row, t_column] != Tiles.Empty)
                {
                    ++t_nonEmptyTiles;
                }
            }
        }

        if (t_nonEmptyTiles != m_amountToCompare)
        {
            return m_falseNode;
        }
        else
        {
            return m_trueNode;
        }
    }
}
