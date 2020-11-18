using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private HexaGrid hexaGrid;
    [SerializeField] private float timeToUpdate = 0.2f;

    private void Start()
    {
        DOVirtual.DelayedCall(timeToUpdate, () =>
        {
            hexaGrid.UpdateGrid();
        }).SetLoops(-1);
    }
}