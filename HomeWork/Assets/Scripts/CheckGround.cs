
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private bool isGround;
    private void OnEnable()
    {
        Character.OnIsGround += IsGround;
    }

    private void OnDisable()
    {
        Character.OnIsGround -= IsGround;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGround = false;
    }

    private bool IsGround()
    {
        return isGround;
    }
}
