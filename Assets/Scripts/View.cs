using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private Model model;
    [SerializeField] private HexaGrid hexaGrid;
    [SerializeField] private float timeToUpdate = 0.2f;

    [SerializeField]
    private float orthoRef = 32;
    [SerializeField]
    private int rayonRef = 40;

    [SerializeField] private int speedFactor = 2;

    private void Start()
    {
        Camera.main.orthographicSize = (float)hexaGrid.GridData.Rayon / rayonRef * orthoRef;

        DOVirtual.DelayedCall(0, () =>
        {
            for (int i = 0; i < speedFactor; i++)
            {
                model.UpdateGrid();
            }
            hexaGrid.UpdateGrid();
        }).SetLoops(-1);
    }
}