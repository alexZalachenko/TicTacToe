using System.Collections;
using UnityEngine;
using Util;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameEndPanelController m_gameEndPanelController = null;
    [SerializeField]
    private TurnController m_turnController = null;
    [SerializeField]
    private GameObject m_board = null;

    private void Start()
    {
        m_turnController.OnGameEnd += OnGameEnd;
    }
    
    public void RestartGame()
    {
        // To check if game is being played
        if (m_board.activeSelf)
        {
            OnGameStart();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnGameStart()
    {
        m_gameEndPanelController.OnGameStart();
        m_turnController.OnGameStart();
    }

    private void OnGameEnd(GameResults p_result)
    {
        StartCoroutine(ShowGameEndPanel(p_result));
    }

    private IEnumerator ShowGameEndPanel(GameResults p_result)
    {
        yield return new WaitForSeconds(1f);
        m_gameEndPanelController.OnGameEnd(p_result);
    }
}
