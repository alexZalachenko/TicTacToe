using UnityEngine;
using UnityEngine.UI;
using Util;

public class DifficultyPanelController : MonoBehaviour
{
    [SerializeField]
    private Toggle m_minmax = null;
    [SerializeField]
    private Toggle m_decisionTree = null;
    [SerializeField]
    private TurnController m_turnController = null;

    private void Awake()
    {
        m_turnController.SetAlgorithm(m_minmax.isOn ? Algorithms.Minmax : Algorithms.DecisionTree);
    }

    public void OnChange()
    {
        if (m_minmax.isOn)
        {
            m_turnController.SetAlgorithm(Algorithms.Minmax);
        }
        else if (m_decisionTree.isOn)
        {
            m_turnController.SetAlgorithm(Algorithms.DecisionTree);
        }
    }

    public void SetVisibility(bool p_visible)
    {
        gameObject.SetActive(p_visible);
    }
}
