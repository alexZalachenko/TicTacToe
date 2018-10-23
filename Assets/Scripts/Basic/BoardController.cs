using UnityEngine;
using Util;

public class BoardController : MonoBehaviour
{
    private BoardData m_boardData;
    [SerializeField]
    private BoardView m_boardView = null;
    [SerializeField]
    private ParticleSystem m_placingTileParticles = null;
    private Players m_startingPlayer = Players.Human;
    public Players StartingPlayer
    {
        get
        {
            return m_startingPlayer;
        }

        set
        {
            m_startingPlayer = value;
            m_boardData = new BoardData(StartingPlayer, m_boardData.HumanTile);
        }
    }

    private void Awake()
    {
        m_boardData = new BoardData(StartingPlayer);
    }

    private void Start()
    {
        RestartGame();
    }

    public BoardData GetBoard()
    {
        return m_boardData;
    }

    public void MakeMove(Move t_move)
    {
        // Change particles color depending on the player
        ParticleSystem.ColorOverLifetimeModule t_lifetimeColor = m_placingTileParticles.colorOverLifetime;
        Gradient t_gradient = m_placingTileParticles.colorOverLifetime.color.gradient;
        GradientColorKey[] t_colorKeys = t_gradient.colorKeys;
        for (int t_colorIndex = 0; t_colorIndex < t_gradient.colorKeys.Length; t_colorIndex++)
        {
            t_colorKeys[t_colorIndex].color = m_boardData.CurrentPlayerColor;
        }
        t_gradient.colorKeys = t_colorKeys;
        t_lifetimeColor.color = new ParticleSystem.MinMaxGradient(t_gradient);

        m_boardView.MakeMove(t_move, m_boardData.CurrentPlayerTile, m_boardData.CurrentPlayerColor);
        m_boardData.MakeMove(t_move);
    }

    public void RestartGame()
    {
        m_boardData = new BoardData(StartingPlayer, m_boardData.HumanTile);
        m_boardView.DepleteBoard();
    }

    public void SetHumanTile(Tiles p_newValue)
    {
        if (m_boardData == null)
        {
            m_boardData = new BoardData(StartingPlayer);
        }

        if (m_boardData.HumanTile != p_newValue)
        {
            m_boardData.SwapTiles();
            if (m_boardView.gameObject.activeSelf)
            {
                m_boardView.SwapTiles();
            }
        }
    }
}
