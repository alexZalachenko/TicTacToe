using System;
using System.Collections;
using UnityEngine;
using Util;

public class TurnController : MonoBehaviour
{
    private enum StartingPlayerMode
    {
        Human,
        AI,
        Alternate
    }
    [SerializeField]
    private float m_AIDecisionTime = 0.75f;
    [SerializeField]
    private DecisionTree m_decisionTree = null;
    private Minimax m_minimax = new Minimax();
    private IAlgorithm m_currentAI = null;
    [SerializeField]
    private BoardController m_boardController = null;
    [SerializeField]
    private StartingPlayerMode m_startingPlayerMode = StartingPlayerMode.Alternate;
    private Players m_lastStartingPlayer = Players.AI;
    private bool m_isHumanTurn = false;
    public Action<GameResults> OnGameEnd;
    [SerializeField]
    private GameController m_gameController = null;

    private void Awake()
    {
        m_currentAI = m_minimax;
    }

    public void OnGameStart()
    {
        SetupTurn();

        m_boardController.RestartGame();
        if (!m_isHumanTurn)
        {
            m_isHumanTurn = !m_isHumanTurn;
            StartCoroutine(OnTurnFinished());
        }
    }

    public void OnTileClick(TileView p_theTile)
    {
        if (m_isHumanTurn && p_theTile.TileType == Tiles.Empty)
        {
            MakeMove(new Move(p_theTile.Row, p_theTile.Column));
        }
    }

    public void SetAlgorithm(Algorithms p_algorithm)
    {
        switch (p_algorithm)
        {
            case Algorithms.Minmax:
                m_currentAI = m_minimax;
                break;
            case Algorithms.DecisionTree:
                m_currentAI = m_decisionTree;
                break;
        }
    }

    public void SetStartingPlayer(int p_newValue)
    {
        if ((StartingPlayerMode)p_newValue != m_startingPlayerMode)
        {
            m_startingPlayerMode = (StartingPlayerMode)p_newValue;
            SetupTurn();
            m_gameController.RestartGame();
        }
    }

    private void SetupTurn()
    {
        if (m_startingPlayerMode == StartingPlayerMode.Alternate)
        {
            if (m_lastStartingPlayer == Players.AI)
            {
                m_lastStartingPlayer = Players.Human;
                m_isHumanTurn = true;
            }
            else
            {
                m_lastStartingPlayer = Players.AI;
                m_isHumanTurn = false;
            }
        }
        else if (m_startingPlayerMode == StartingPlayerMode.Human)
        {
            m_lastStartingPlayer = Players.Human;
            m_isHumanTurn = true;
        }
        else
        {
            m_lastStartingPlayer = Players.AI;
            m_isHumanTurn = false;
        }
        m_boardController.StartingPlayer = m_lastStartingPlayer;
    }

    private void OnEnemyTurn()
    {
        BoardData t_board = m_boardController.GetBoard();
        MakeMove(m_currentAI.GetBestMovement(t_board));
    }

    private IEnumerator OnTurnFinished()
    {
        m_isHumanTurn = !m_isHumanTurn;

        if (!m_isHumanTurn)
        {
            // Wait a bit before enemy's turn
            yield return new WaitForSeconds(m_AIDecisionTime);
            OnEnemyTurn();
        }
    }

    private void MakeMove(Move p_move)
    {
        m_boardController.MakeMove(p_move);

        GameResults t_gameResult = m_boardController.GetBoard().GetGameResult();
        if (t_gameResult != GameResults.Unfinished)
        {
            OnGameEnd(t_gameResult);
        }
        else
        {
            StartCoroutine(OnTurnFinished());
        }
    }
}
