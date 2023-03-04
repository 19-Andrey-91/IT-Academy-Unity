using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class IsometricOrder : MonoBehaviour
{
    private const int IsometricRangePerYUnit = 256;

    private int defaultSortingOrder;

    private Renderer thisRenderer;

    private void Start()
    {
        thisRenderer = GetComponent<Renderer>();
        defaultSortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);
        thisRenderer.sortingOrder = defaultSortingOrder;
    }

    public void CalculateNewSortOrder(Transform character)
    {
        float coordinateDifferenceY = character.position.y - transform.position.y;
        thisRenderer.sortingOrder = (int)((coordinateDifferenceY) * IsometricRangePerYUnit);
    }

    public void SetDefaultSortingOrder()
    {
        thisRenderer.sortingOrder = defaultSortingOrder;
    }

}
