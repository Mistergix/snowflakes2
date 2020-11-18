using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisCity.Celine
{
	public class HexaCell : MonoBehaviour
	{
        private int q, r, s;
        private float state, oldState;
        private bool isEdge;

        public void Init(int q, int r, float state, bool isEdge)
        {
            this.q = q;
            this.r = r;
            this.state = state;
            this.isEdge = isEdge;
            oldState = state;
            s = -q - r;
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
                neightbours.Add(new Vector2Int(q + o.x, r + o.y));
            }

            return neightbours;
        }
	    // Start is called before the first frame update
   		void Start()
   		{
  		    
    	}

		// Update is called once per frame
    	void Update()
    	{
        		
    	}
	}
}