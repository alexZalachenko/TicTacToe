using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GenericUIView : MonoBehaviour
{
    protected Graphic[] m_graphics = null;
    private float m_elapsedTime = 0;
    [SerializeField]
    private float m_timeToComplete = 0.25f;
    [SerializeField]
    private bool m_startsDisabled = true;
    [SerializeField]
    private bool m_dontSearchInChildren = false;

    protected virtual void Awake()
    {
        m_graphics = m_dontSearchInChildren ? new Graphic[] { GetComponent<Graphic>() } : GetComponentsInChildren<Graphic>();
        if (m_startsDisabled)
        {
            SetAlpha(0);
        }
    }

    public virtual void SetVisibility(bool p_isVisible)
    {
        if (p_isVisible)
        {
            gameObject.SetActive(true);
            StartCoroutine(TweenAlpha(1, m_graphics));
        }
        else
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            StartCoroutine(TweenAlpha(0, m_graphics));
        }
    }

    public void ReverseVisibility()
    {
        StartCoroutine(TweenAlpha(((int)m_graphics[0].color.a) ^ 1, m_graphics));
    }

    private void SetAlpha(float p_value, Graphic[] p_graphics = null)
    {
        Graphic[] t_graphicsToAlpha = p_graphics != null ? p_graphics : m_graphics;

        foreach (Graphic t_image in t_graphicsToAlpha)
        {
            Color t_newColor = t_image.color;
            t_newColor.a = p_value;
            t_image.color = t_newColor;
        }
    }
   
    protected IEnumerator TweenAlpha(float p_alpha, Graphic[] p_graphics = null)
    {
        m_elapsedTime = 0;
        float t_originalAlpha = m_graphics[0].color.a;
        float t_newAlpha;
        while (m_elapsedTime < m_timeToComplete)
        {
            m_elapsedTime += Time.deltaTime;
            float t_interpolation = m_elapsedTime / m_timeToComplete;
            t_newAlpha = t_originalAlpha + (p_alpha - t_originalAlpha) * t_interpolation;
            SetAlpha(t_newAlpha, p_graphics);
            yield return null;
        }
        SetAlpha(p_alpha, p_graphics);
        OnTweenCompletion();
    }

    protected virtual void OnTweenCompletion()
    {
        if (m_graphics[0].color.a == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
