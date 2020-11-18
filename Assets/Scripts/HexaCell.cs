using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaCell : MonoBehaviour
	{
        private int q, r, s;
        private float state, oldState;
        private bool isEdge;

    public int Q { get => q; set => q = value; }
    public int R { get => r; set => r = value; }
    public int S { get => s; set => s = value; }
    public bool IsEdge { get => isEdge; set => isEdge = value; }

    [SerializeField] private SpriteRenderer renderer;

    public void SetState(float t)
    {
        state = t;
    }

    public void UpdateState()
    {
        oldState = state;
        renderer.color = Color.Lerp(new Color(66f / 255, 134f / 255, 244f / 255), new Color(1, 1, 1), state);
    }

    public void Init(int q, int r, float state)
        {
            this.Q = q;
            this.R = r;
            this.state = state;
            oldState = state;
            S = -q - r;
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
	    // Start is called before the first frame update
   		void Start()
   		{
  		    
    	}

		// Update is called once per frame
    	void Update()
    	{
        		
    	}
	}
