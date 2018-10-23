using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private List<GenericUIView> m_toEnableWhenEnteringGame = new List<GenericUIView>();
    [SerializeField]
    private List<GenericUIView> m_toDisableWhenExitingGame = new List<GenericUIView>();
    [SerializeField]
    private GameController m_gameController = null;
    [SerializeField]
    private GenericUIView m_difficultyPanelView = null;
    [SerializeField]
    private GenericUIView m_optionsMenuView = null;
    [SerializeField]
    private GenericUIView m_quitPanelView = null;
    [SerializeField]
    private GenericUIView m_mainMenuView = null;
    private GenericUIView m_openedInnerMenu = null;

    private void Start()
    {
        TapOutsideToCloseArea t_tapArea = FindObjectOfType<TapOutsideToCloseArea>();
        t_tapArea.OnUIElementClosed += OnReturnFromInnerMenu;
        t_tapArea.gameObject.SetActive(false);
        OpenMainMenu();
    }

    private void Play()
    {
        foreach (GenericUIView t_gameObject in m_toEnableWhenEnteringGame)
        {
            t_gameObject.SetVisibility(true);
        }
        m_mainMenuView.SetVisibility(false);
        m_gameController.OnGameStart();
    }

    public void OnOptionSelected(int p_option)
    {
        switch (p_option)
        {
            case 0:
                m_openedInnerMenu = m_difficultyPanelView;
                m_difficultyPanelView.SetVisibility(true);
                break;
            case 1:
                m_openedInnerMenu = m_optionsMenuView;
                m_optionsMenuView.SetVisibility(true);
                break;

            case 2:
                m_openedInnerMenu = m_quitPanelView;
                m_quitPanelView.SetVisibility(true);
                break;

            case 3:
                Play();
                break;
        }
        m_mainMenuView.SetVisibility(false);
    }

    public void OpenMainMenu()
    {
        foreach (GenericUIView t_gameObject in m_toDisableWhenExitingGame)
        {
            t_gameObject.SetVisibility(false);
        }
        m_mainMenuView.SetVisibility(true);
    }

    public void OnReturnFromInnerMenu()
    {
        if (m_openedInnerMenu)
        {
            m_openedInnerMenu.SetVisibility(false);
            m_mainMenuView.SetVisibility(true);
            m_openedInnerMenu = null;
        }
    }
}
