using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Model : MonoBehaviour
{
    [SerializeField]
    float alpha, beta, gamma;
    
    [SerializeField]
    HexaGrid grid;

    [SerializeField]
    HexaGrid hexagridPrefab;

	// Start is called before the first frame update
   	void Start()
   	{
        foreach (var cell in grid.Cells)
        {
            int q = cell.Value.Q;
            int r = cell.Value.R;
            if( q == 0 && r == 0)
            {
                cell.Value.SetState(1);
            }
            else
            {
                cell.Value.SetState(beta);
            }
            cell.Value.UpdateState();
        }
    }

    public void UpdateGrid()
    {
        HexaGrid rec = Instantiate(hexagridPrefab);
        HexaGrid nonrec = Instantiate(hexagridPrefab);

        foreach (var x in grid.Cells)
        {
            HexaCell cell = x.Value;
            if (cell.IsEdge) continue;
            if (rec.Cells.TryGetValue(x.Key,out HexaCell recCell)) {
                if (nonrec.Cells.TryGetValue(x.Key, out HexaCell nonRecCell))
                {
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

                    recCell.UpdateState();
                    nonRecCell.UpdateState();
                    if(recCell.State != 0)
                    {
                        recCell.State += gamma;
                    }
                }
            }
        }

        foreach (var x in grid.Cells)
        {
            HexaCell cell = x.Value;
            if (cell.IsEdge) continue;
            if (nonrec.Cells.TryGetValue(x.Key, out HexaCell nonRecCell))
            {
                cell.UpdateState();
                cell.State = alpha * (nonRecCell.State / 2) + alpha * GetNeightboursSum(nonRecCell, nonrec) + rec.GetCell(x.Key).State;
            }
        }

        Destroy(nonrec);
        Destroy(rec);
    }

    private float GetNeightboursSum(HexaCell cell, HexaGrid g)
    {
        float sum = 0;
        foreach (var item in g.GetNeightbours(cell))
        {
            sum += item.OldState;
        }

        return sum / 12;
    }

	// Update is called once per frame
    void Update()
    {
            
    }
}
