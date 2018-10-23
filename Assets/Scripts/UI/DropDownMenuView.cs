using System.Collections;
using UnityEngine;

public class DropDownMenuView : MonoBehaviour
{
    [SerializeField]
    private float m_stateChangeDuration = 1.0f;
    private float m_elapsedTime = 0.0f;
    private float m_initialScale = 0.0f;
    private bool m_isDisabled = false;

    public void Open()
    {
        if (m_isDisabled)
        {
            return;
        }
        StartCoroutine(ChangeScale(1));
    }

    public void Close()
    {
        if (m_isDisabled)
        {
            return;
        }
        StartCoroutine(ChangeScale(0));
    }

    private IEnumerator ChangeScale(float p_target)
    {
        m_elapsedTime = 0;
        m_initialScale = transform.localScale.y;
        m_isDisabled = true;

        while (transform.localScale.y != p_target)
        {
            m_elapsedTime += Time.deltaTime;
            if (m_elapsedTime > m_stateChangeDuration)
            {
                m_elapsedTime = m_stateChangeDuration;
            }
            float t_currentScale = m_initialScale + (p_target - m_initialScale) * (m_elapsedTime / m_stateChangeDuration);
            transform.localScale = new Vector3(t_currentScale, t_currentScale, t_currentScale);
            yield return null;
        }

        m_isDisabled = false;
    }
}
