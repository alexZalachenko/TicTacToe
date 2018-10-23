using UnityEngine;

public class DecisionTree : MonoBehaviour, IAlgorithm
{
    [SerializeField]
    private TreeNode m_root = null;

    Move IAlgorithm.GetBestMovement(BoardData p_board)
    {
        Action t_action = m_root.NavigateTree(p_board) as Action;
        return t_action.PerformAction(p_board);
    }
}
