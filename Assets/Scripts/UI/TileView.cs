using UnityEngine;
using UnityEngine.UI;
using Util;

public class TileView : GenericUIView
{
    private Image m_image;
    [SerializeField]
    private Sprite m_circleSprite;
    [SerializeField]
    private Sprite m_crossSprite;
    public Tiles TileType { get; set; }
    public int Column { get; private set; }
    public int Row { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        m_image = GetComponent<Image>();
        TileType = Tiles.Empty;
        for (int t_index = 0; t_index < m_image.transform.parent.childCount; t_index++)
        {
            if (m_image.transform.parent.GetChild(t_index) == m_image.transform)
            {
                Column = t_index % 3;
                Row = t_index / 3;
                break;
            }
        }
    }

    protected override void OnTweenCompletion()
    {
        // Leave it empty so the gameObject is not deactivated
    }

    public void RemoveTile()
    {
        SetVisibility(false);
        TileType = Tiles.Empty;
    }

    public void SetCircle(Color p_color)
    {
        m_image.sprite = m_circleSprite;
        SetTile(p_color);
        TileType = Tiles.Circle;
    }

    public void SetCross(Color p_color)
    {
        m_image.sprite = m_crossSprite;
        SetTile(p_color);
        TileType = Tiles.Cross;
    }

    private void SetTile(Color p_color)
    {
        m_image.color = p_color - new Color(0, 0, 0, 1);
        SetVisibility(true);
    }

    public Color GetColor()
    {
        return m_image.color;
    }
}
