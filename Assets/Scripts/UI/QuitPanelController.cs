using UnityEngine;

public class QuitPanelController : MonoBehaviour
{
    [SerializeField]
    private GameController m_gameController = null;
    [SerializeField]
    private MainMenuController m_mainMenuController = null;
    private GenericUIView m_quitPanelView = null;

    private void Awake()
    {
        m_quitPanelView = GetComponent<GenericUIView>();
    }

    public void QuitGame()
    {
        m_gameController.Quit();
    }

    public void DontQuitGame()
    {
        m_quitPanelView.SetVisibility(false);
        m_mainMenuController.OnReturnFromInnerMenu();
    }
}
