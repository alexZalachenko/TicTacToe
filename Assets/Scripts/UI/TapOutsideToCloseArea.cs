using System.Collections.Generic;
using UnityEngine;

public class TapOutsideToCloseArea : MonoBehaviour
{
    private Queue<GenericUIView> m_openedViews = new Queue<GenericUIView>();
    public System.Action OnUIElementClosed;

    public void OnTap()
    {
        m_openedViews.Peek().SetVisibility(false);
        if (OnUIElementClosed != null)
        {
            OnUIElementClosed.Invoke();
        }
    }

    public void OnEntityEnabled(GenericUIView p_entity)
    {
        gameObject.SetActive(true);
        m_openedViews.Enqueue(p_entity);
    }

    public void OnEntityDisabled()
    {
        m_openedViews.Dequeue();
        if (m_openedViews.Count == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
