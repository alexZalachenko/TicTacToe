using UnityEngine;

public class DropDownMenuController : MonoBehaviour
{
    [SerializeField]
    private MainMenuController m_mainMenuController = null;
    [SerializeField]
    private GenericUIView m_difficultyPanel = null;
    [SerializeField]
    private GenericUIView m_optionsMenu = null;
    [SerializeField]
    private GenericUIView m_quitPanel = null;
    [SerializeField]
    private GenericUIView m_dropDownMenu = null;
    [SerializeField]
    private GameController m_gameController = null;
    private GenericUIView m_openedInnerMenu = null;

    public void ChangeState()
    {
        if (m_dropDownMenu.gameObject.activeSelf)
        {
            m_dropDownMenu.SetVisibility(false);
        }
        else
        {
            m_dropDownMenu.SetVisibility(true);
        }
    }

    public void OnOptionSelected(int p_option)
    {
        if (m_openedInnerMenu != null)
        {
            m_openedInnerMenu.SetVisibility(false);
            m_openedInnerMenu = null;
        }
        ChangeState();
        switch (p_option)
        {
            case 0:
                m_mainMenuController.OpenMainMenu();
                break;
            case 1:
                m_difficultyPanel.SetVisibility(true);
                m_openedInnerMenu = m_difficultyPanel;
                break;
            case 2:
                m_optionsMenu.SetVisibility(true);
                m_openedInnerMenu = m_optionsMenu;
                break;
            case 3:
                m_quitPanel.SetVisibility(true);
                m_openedInnerMenu = m_quitPanel;
                break;
            case 4:
                m_gameController.RestartGame();
                break;
        }
    }
}

