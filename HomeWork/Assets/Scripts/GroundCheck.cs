using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGround { get; private set; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            IsGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            IsGround = false;
        }
    }
}
