using UnityEngine;
using Util;

class GameEndPanelController : MonoBehaviour
{
    [SerializeField]
    private GameEndPanelView m_gameEndPanelView = null;
    [SerializeField]
    private GameObject m_particles = null;
    [SerializeField]
    private AudioSource[] m_gameEndSounds;
    private AudioSource m_currentPlaying = null;
    string[] m_gameEndTexts =
    {
        "Tie again!",
        "Yay ya beat 'em!",
        "Dis is lost bruh"
    };

    public void OnGameStart()
    {
        m_gameEndPanelView.SetVisibility(false);
        m_particles.SetActive(false);
    }

    public void OnGameEnd(GameResults p_result)
    {
        m_gameEndPanelView.SetVisibility(true);
        m_gameEndPanelView.SetText(m_gameEndTexts[(int)p_result]);
        m_currentPlaying = m_gameEndSounds[(int)p_result];
        m_currentPlaying.Play();
        m_particles.SetActive(true);
    }
}
