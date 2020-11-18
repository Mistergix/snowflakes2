using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexaGrid : MonoBehaviour
{
    [SerializeField]
    private int rayon;
    [SerializeField]
    private HexaCell prefabCell;
    [SerializeField]
    private float orthoRef = 8;
    [SerializeField]
    private int rayonRef = 10;
    private HashSet<Vector2Int> positionsCubic;

    

    private Dictionary<Vector2Int, HexaCell> cells;

    private Grid grid;


    public int NbCellsbyWidth { get; set; }
    public Dictionary<Vector2Int, HexaCell> Cells { get => cells; private set => cells = value; }

    public HexaCell GetCell(Vector2Int position)
    {
        if(cells.TryGetValue(position, out HexaCell hexaCell))
        {
            return hexaCell;
        }
        throw new UnityException("La position n'est pas enregistrée " + position);
    }

    // Start is called before the first frame update
    void Awake()
   	{
        /*
        var height = Camera.main.orthographicSize * 2.0f;
        var width = height * Screen.width / Screen.height;
        transform.localScale = new Vector3(width / 10f, 0.1f, height / 10f);*/

        Camera.main.orthographicSize = (float)rayon / rayonRef * orthoRef;

        grid = GetComponent<Grid>();

        Cells = new Dictionary<Vector2Int, HexaCell>();
        NbCellsbyWidth = 2 * rayon + 1;

        positionsCubic = new HashSet<Vector2Int>();
        for(int q = -rayon; q <= rayon; q++)
        {
            int r1 = Mathf.Max(-rayon, -q - rayon);
            int r2 = Mathf.Min(rayon, -q + rayon);
            for(int r = r1; r <= r2; r++)
            {
                positionsCubic.Add(new Vector2Int(q, r));
            }
        }

        foreach (var posCubic in positionsCubic)
        {
            HexaCell cell = Instantiate(prefabCell);
            cell.Init(posCubic.x, posCubic.y, 0);
            Cells.Add(posCubic, cell);

            int col = posCubic.x + (posCubic.y - (posCubic.y & 1)) / 2;
            int row = posCubic.y;

            cell.transform.position = grid.CellToWorld(new Vector3Int(col, row, 0));
        }

        foreach (HexaCell cell in Cells.Values)
        {
            cell.IsEdge = GetNeightbours(cell).Count != 6;
            cell.gameObject.name = $"Cell {cell.Q}, {cell.R}, {cell.IsEdge}";
        }

        UpdateGrid();
    }

    internal void UpdateGrid()
    {
        foreach (var item in Cells)
        {
            item.Value.UpdateState();
        }
    }

    public List<HexaCell> GetNeightbours(HexaCell cell)
    {
        List<Vector2Int> falseNeightbours = cell.GetFalseNeightbours();
        List<HexaCell> hexacells = new List<HexaCell>();

        foreach(var x in falseNeightbours)
        {
            if (Cells.TryGetValue(x, out HexaCell c))
            {
                hexacells.Add(c);
            }
        }

        return hexacells;
    }

	// Update is called once per frame
    void Update()
    {
        	
    }
}