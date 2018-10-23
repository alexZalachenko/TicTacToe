using UnityEngine;
using Util;

public class OptionsMenuController : MonoBehaviour
{
    [SerializeField]
    private TurnController m_turnController = null;
    [SerializeField]
    private BoardController m_boardController = null;

    public void OnPlayFirstChange(int p_newValue)
    {
        m_turnController.SetStartingPlayer(p_newValue);
    }

    public void OnPlayerColorChange(int p_newValue)
    {
        Debug.Log(p_newValue);

    }

    public void OnPlayerTileChange(int p_newValue)
    {
        m_boardController.SetHumanTile((Tiles)p_newValue);
    }
}
