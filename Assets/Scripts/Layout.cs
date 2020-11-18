using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layout
{
    private float cellRadius;
    private Orientation orientation;
    private Vector2 origin;

    public Layout(Orientation orientation, Vector2 origin, float cellRadius)
    {
        this.cellRadius = cellRadius;
        this.orientation = orientation;
        this.origin = origin;
    }

    public Vector2 HexToPixel(HexaCell hexaCell)
    {
        Orientation o = orientation;
        float x = (o.F0 * hexaCell.Q + o.F1 * hexaCell.R) * cellRadius;
        float y = (o.F3 * hexaCell.Q + o.F4 * hexaCell.R) * cellRadius;

        return new Vector2(x, y);
    }

    public List<Vector2Int> Corners(HexaCell cell)
    {
        Vector2 center = HexToPixel(cell);
        List<Vector2Int> corners = new List<Vector2Int>();

        for (int i = 0; i < 6; i++)
        {
            Vector2 offset = CornerOffset(i);
            corners.Add(new Vector2Int((int)(center.x + offset.x), (int)(center.y + offset.y)));
        }

        return corners;
    }
 
    private Vector2 CornerOffset(int i)
    {
        float angle = Mathf.PI / 3 * (i + orientation.Angle * 1 / 60);
        return new Vector2(cellRadius * Mathf.Cos(angle), cellRadius * Mathf.Sin(angle));
    }

    public static Layout Pointy(Vector2 origin, float cellRadius)
    {
        return new Layout(Orientation.Pointy(), origin, cellRadius);
    }

    public static Layout Flat(Vector2 origin, float cellRadius)
    {
        return new Layout(Orientation.Flat(), origin, cellRadius);
    }
}