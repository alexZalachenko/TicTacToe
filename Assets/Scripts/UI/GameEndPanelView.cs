using UnityEngine.UI;

class GameEndPanelView : GenericUIView
{
    private Text m_text = null;

    protected override void Awake()
    {
        base.Awake();
        m_text = GetComponentInChildren<Text>();
    }

    public override void SetVisibility(bool p_isVisible)
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

    public void SetText(string p_text)
    {
        m_text.text = p_text;
    }
}

