﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Model : MonoBehaviour
{
    float beta, gamma;
    
    [SerializeField]
    HexaGrid grid;

    public float Beta { get => beta; set => beta = value; }
    public float Gamma { get => gamma; set => gamma = value; }

    // Start is called before the first frame update
    public void ResetModel()
    {
        foreach (var cell in grid.GridData.Cells)
        {
            int q = cell.Value.Q;
            int r = cell.Value.R;
            if (q == 0 && r == 0)
            {
                cell.Value.SetState(1);
            }
            else
            {
                cell.Value.SetState(Beta);
            }

            grid.GetHexaCell(cell.Key).UpdateState();
        }
    }

    public void UpdateGrid()
    {
        HexaGridData rec = grid.CleanClone();
        HexaGridData nonrec = grid.CleanClone();

        foreach (var x in grid.GridData.Cells)
        {
            HexaCellData cell = x.Value;
            if (cell.IsEdge) continue;

            HexaCellData recCell = rec.GetCell(x.Key);
            HexaCellData nonRecCell = nonrec.GetCell(x.Key);

           
            bool recept = false;
            if (cell.OldState >= 1) recept = true;
            else
            {
                foreach (var item in grid.GetNeightbours(cell))
                {
                    if(item.OldState >= 1)
                    {
                        recept = true;
                        break;
                    }
                           
                }

            }
            if (recept)
            {
                recCell.State = cell.State;
                nonRecCell.State = 0;
            }
            else
            {
                recCell.State = 0;
                nonRecCell.State = cell.State;
            }

            //recCell.HexaCellInstance.UpdateState();
            //nonRecCell.HexaCellInstance.UpdateState();

            recCell.OldState = recCell.State;
            nonRecCell.OldState = nonRecCell.State;

            if(recCell.State != 0)
            {
                recCell.State += Gamma;
            }
                
            
        }

        foreach (var x in grid.GridData.Cells)
        {
            HexaCellData cell = x.Value;
            if (cell.IsEdge) continue;

            HexaCellData nonRecCell = nonrec.GetCell(x.Key);
            cell.OldState = cell.State;
            cell.State = (nonRecCell.State / 2) + GetNeightboursSum(nonRecCell, nonrec) + rec.GetCell(x.Key).State;
        }
    }

    private float GetNeightboursSum(HexaCellData cell, HexaGridData g)
    {
        float sum = 0;
        foreach (var item in g.GetNeighbours(cell))
        {
            sum += item.OldState;
        }

        return sum / 12;
    }
}
