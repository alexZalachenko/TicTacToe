using UnityEngine;
using UnityEngine.UI;
using Util;

public class BoardView : GenericUIView
{
    [SerializeField]
    private Transform m_tilesParent = null;
    [SerializeField]
    private ParticleSystem m_placingTileParticles = null;

    public override void SetVisibility(bool p_isVisible)
    {
        if (p_isVisible)
        {
            gameObject.SetActive(true);
            StartCoroutine(TweenAlpha(1, new Graphic[] { m_graphics[0] }));
        }
        else
        {
            base.SetVisibility(p_isVisible);
        }
    }

    public void DepleteBoard()
    {
        foreach (Transform t_boardSlot in m_tilesParent)
        {
            t_boardSlot.GetComponent<TileView>().RemoveTile();
        }
    }

    public void SwapTiles()
    {
        for (int t_childrenIndex = 0; t_childrenIndex < m_tilesParent.childCount; t_childrenIndex++)
        {
            TileView t_tile = m_tilesParent.GetChild(t_childrenIndex).GetComponent<TileView>();
            if (t_tile.TileType == Tiles.Circle)
            {
                t_tile.SetCross(t_tile.GetColor());
            }
            else if (t_tile.TileType == Tiles.Cross)
            {
                t_tile.SetCircle(t_tile.GetColor());
            }
        }
    }

    public void MakeMove(Move t_move, Tiles p_tileType, Color p_color)
    {
        TileView t_tile = m_tilesParent.GetChild(t_move.Row * 3 + t_move.Column).GetComponent<TileView>();
        if (p_tileType == Tiles.Cross)
        {
            t_tile.SetCross(p_color);
        }
        else
        {
            t_tile.SetCircle(p_color);
        }
        Vector3 t_particlesPosition = m_placingTileParticles.transform.position;
        t_particlesPosition.x = t_tile.transform.position.x;
        t_particlesPosition.y = t_tile.transform.position.y;
        m_placingTileParticles.transform.position = t_particlesPosition;
        m_placingTileParticles.Play();
    }
}
