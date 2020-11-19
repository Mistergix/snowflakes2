using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexaGrid : MonoBehaviour
{
    HexaGridData gridData;
    private int rayon;
    [SerializeField]
    private HexaCell prefabCell;
    
    
    private Grid grid;


    private Dictionary<Vector2Int, HexaCell> hexaCells;

    private void Start()
    {
        HexaCells = new Dictionary<Vector2Int, HexaCell>();
    }

    public HexaGridData GridData { get => gridData; set => gridData = value; }
    public Dictionary<Vector2Int, HexaCell> HexaCells { get => hexaCells; set => hexaCells = value; }
    public int Rayon { get => rayon; set => rayon = value; }

    public HexaGridData CleanClone()
    {
        HexaGridData data = new HexaGridData(grid, Rayon);

        foreach (var posCubic in data.PositionsCubic)
        {
            HexaCellData hCell = GetCell(posCubic);
            data.Cells.Add(posCubic, new HexaCellData(hCell));
        }

        return data;
    }

    public HexaCell GetHexaCell(Vector2Int position)
    {
        if (HexaCells.TryGetValue(position, out HexaCell hexaCell))
        {
            return hexaCell;
        }
        throw new UnityException("La position n'est pas enregistrée " + position);
    }

    public HexaCellData GetCell(Vector2Int position)
    {
        return GridData.GetCell(position);
    }

    // Start is called before the first frame update
    public void ResetGrid(bool showState)
    {
        grid = GetComponent<Grid>();
        GridData = new HexaGridData(grid, Rayon);

        foreach (var item in HexaCells)
        {
            Destroy(item.Value.gameObject);
        }

        HexaCells = new Dictionary<Vector2Int, HexaCell>();

        foreach (var posCubic in GridData.PositionsCubic)
        {
            HexaCell cell = Instantiate(prefabCell);
            cell.Init(posCubic.x, posCubic.y, 0, showState);

            GridData.Cells.Add(posCubic, cell.CellData);
            HexaCells.Add(posCubic, cell);

            int col = posCubic.x + (posCubic.y - (posCubic.y & 1)) / 2;
            int row = posCubic.y;

            cell.transform.position = grid.CellToWorld(new Vector3Int(col, row, 0));
        }

        foreach (HexaCellData cell in GridData.Cells.Values)
        {
            cell.IsEdge = GetNeightbours(cell).Count != 6;
        }

        UpdateGrid();
    }

    internal void UpdateGrid()
    {
        foreach (var item in HexaCells)
        {
            item.Value.UpdateState();
        }
    }

    public List<HexaCellData> GetNeightbours(HexaCellData cell)
    {
        return gridData.GetNeighbours(cell);
    }
}