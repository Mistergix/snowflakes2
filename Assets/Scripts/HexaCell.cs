using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexaCell : MonoBehaviour
	{
    HexaCellData cellData;

    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Text stateText;
    [SerializeField] private bool showState;

    private void Start()
    {
        stateText.transform.parent.gameObject.SetActive(showState);
    }

    public HexaCellData CellData { get => cellData; set => cellData = value; }

    public void SetState(float t)
    {
        CellData.SetState(t);
        
    }

    public void UpdateState()
    {
        CellData.OldState = CellData.State;
        renderer.color = Color.Lerp(new Color(66f / 255, 134f / 255, 244f / 255), new Color(1, 1, 1), CellData.State);
        gameObject.name = $"Cell {CellData.Q}, {CellData.R}, {CellData.IsEdge}, {CellData.State}";

        if (showState)
        {
            stateText.text = CellData.State + "";
        }

        
    }

    public void Init(int q, int r, float state)
        {
        CellData = new HexaCellData(q, r, state, this);
        }

        
	}
