using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaGridData
{
    private Grid grid;
    private int rayon;

    public Dictionary<Vector2Int, HexaCellData> Cells { get; private set; }
    private HashSet<Vector2Int> positionsCubic;
    public int NbCellsbyWidth { get; set; }

    public int Rayon { get => rayon; private set => rayon = value; }
    public HashSet<Vector2Int> PositionsCubic { get => positionsCubic; set => positionsCubic = value; }

    public HexaGridData(Grid grid, int rayon)
    {
        this.grid = grid;
        this.rayon = rayon;

        Cells = new Dictionary<Vector2Int, HexaCellData>();

        NbCellsbyWidth = 2 * Rayon + 1;

        PositionsCubic = new HashSet<Vector2Int>();
        for (int q = -Rayon; q <= Rayon; q++)
        {
            int r1 = Mathf.Max(-Rayon, -q - Rayon);
            int r2 = Mathf.Min(Rayon, -q + Rayon);
            for (int r = r1; r <= r2; r++)
            {
                PositionsCubic.Add(new Vector2Int(q, r));
            }
        }
    }

    internal List<HexaCellData> GetNeighbours(HexaCellData cell)
    {
        List<Vector2Int> falseNeightbours = cell.GetFalseNeightbours();
        List<HexaCellData> hexacells = new List<HexaCellData>();

        foreach (var x in falseNeightbours)
        {
            if(Cells.TryGetValue(x, out HexaCellData c))
            {
                hexacells.Add(c);
            }
        }

        return hexacells;
    }

    internal HexaCellData GetCell(Vector2Int key)
    {
        if (Cells.TryGetValue(key, out HexaCellData hexaCell))
        {
            return hexaCell;
        }
        throw new UnityException("La position n'est pas enregistrée " + key);
    }
}