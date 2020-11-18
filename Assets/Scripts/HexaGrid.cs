using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexaGrid : MonoBehaviour
{
    [SerializeField]
    private int rayon;
    private HashSet<Vector2Int> positions;

    private Dictionary<Vector2Int, HexaCell> cells;


    public int NbCellsbyWidth { get; set; }
    public Dictionary<Vector2Int, HexaCell> Cells { get => cells; private set => cells = value; }

    // Start is called before the first frame update
    void Start()
   	{
        var height = Camera.main.orthographicSize * 2.0f;
        var width = height * Screen.width / Screen.height;
        transform.localScale = new Vector3(width / 10f, 0.1f, height / 10f);

        Cells = new Dictionary<Vector2Int, HexaCell>();
        NbCellsbyWidth = 2 * rayon + 1;

        positions = new HashSet<Vector2Int>();
        for(int q = -rayon; q <= rayon; q++)
        {
            int r1 = Mathf.Max(-rayon, -q - rayon);
            int r2 = Mathf.Min(rayon, -q + rayon);
            for(int r = r1; r <= r2; r++)
            {
                positions.Add(new Vector2Int(q, r));
            }
        }

        foreach (var x in positions)
        {
            HexaCell cell = new HexaCell(x.x, x.y, 0);
            Cells.Add(x, cell);
        }

        foreach (HexaCell cell in Cells.Values)
        {
            cell.IsEdge = GetNeightbours(cell).Count != 6;
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