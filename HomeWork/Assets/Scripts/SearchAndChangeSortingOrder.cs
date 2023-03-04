using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAndChangeSortingOrder : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<IsometricOrder>() != null)
        {
            collision.GetComponent<IsometricOrder>().CalculateNewSortOrder(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IsometricOrder>() != null)
        {
            collision.GetComponent<IsometricOrder>().SetDefaultSortingOrder();
        }
    }
}
