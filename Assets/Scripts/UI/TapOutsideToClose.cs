using UnityEngine;

[RequireComponent(typeof(GenericUIView))]
public class TapOutsideToClose : MonoBehaviour
{
    [SerializeField]
    private TapOutsideToCloseArea m_tapArea = null;
    private GenericUIView m_view = null;

    private void Awake()
    {
        m_view = GetComponent<GenericUIView>();
    }

    private void OnEnable()
    {
        m_tapArea.OnEntityEnabled(m_view);
    }

    private void OnDisable()
    {
        m_tapArea.OnEntityDisabled();
    }
}
