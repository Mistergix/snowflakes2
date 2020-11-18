using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisCity.Celine
{
	public class HexaGrid : MonoBehaviour
	{
        [SerializeField]
        private int rayon;

        [SerializeField]
        private HexaCell hexagon;

        private int nbCellsbyWidth;

        private Grid grid;

        private HashSet<Vector2Int> positions;

        private Dictionary<Vector2Int, HexaCell> cells;

        // Start is called before the first frame update
   	    void Start()
   		{
            cells = new Dictionary<Vector2Int, HexaCell>();
            grid = GetComponent<Grid>();
            nbCellsbyWidth = 2 * rayon + 1;

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
                HexaCell cell = Instantiate(hexagon);
                cells.Add(x, cell);
                cell.Init(x.x, x.y, 0,GetNeightbours(cell).Count!=6);
                cell.transform.position = grid.CellToWorld(new Vector3Int(x.x, x.y, 0));
                cell.gameObject.name = $"hexacell {x.x},{x.y},{GetNeightbours(cell).Count != 6}";
            }


    	}

        public List<HexaCell> GetNeightbours(HexaCell cell)
        {
            List<Vector2Int> falseNeightbours = cell.GetFalseNeightbours();
            List<HexaCell> hexacells = new List<HexaCell>();

            foreach(var x in falseNeightbours)
            {
                if (cells.TryGetValue(x, out HexaCell c))
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
}