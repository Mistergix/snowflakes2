using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaCellData
{
    private int q, r, s;
    private float state, oldState;
    private bool isEdge;

    HexaCell hexaCell;
    private HexaCell hCell;

    public HexaCellData(int q, int r, float state, HexaCell hexaCell)
    {
        this.Q = q;
        this.R = r;
        this.State = state;
        OldState = state;
        S = -q - r;
        this.HexaCellInstance = hexaCell;
    }

    public HexaCellData(HexaCellData hCell) : this(hCell.Q, hCell.r, 0, hCell.HexaCellInstance)
    {
    }

    public List<Vector2Int> GetFalseNeightbours()
    {
        List<Vector2Int> neightbours = new List<Vector2Int>();
        List<Vector2Int> offset = new List<Vector2Int>()
            {
                new Vector2Int(1,0),
                new Vector2Int(1,-1),
                new Vector2Int(0,-1),
                new Vector2Int(-1,0),
                new Vector2Int(-1,1),
                new Vector2Int(0,1)
            };

        foreach (var o in offset)
        {
            neightbours.Add(new Vector2Int(Q + o.x, R + o.y));
        }

        return neightbours;
    }

    public int Q { get => q; set => q = value; }
    public int R { get => r; set => r = value; }

    internal void SetState(float t)
    {
        State = t;
    }

    public int S { get => s; set => s = value; }
    public bool IsEdge { get => isEdge; set => isEdge = value; }
    public float State { get => state; set => state = value; }
    public float OldState { get => oldState; set => oldState = value; }
    public HexaCell HexaCellInstance { get => hexaCell; set => hexaCell = value; }
}